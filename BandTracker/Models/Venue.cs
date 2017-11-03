using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace VenueTracker.Models
{
  public class Venue
  {
    public int Id {get; private set;}
    public string Name {get;}

    public Venue(string name, int id = 0)
    {
      Id = id;
      Name = name;
    }

    public override bool Equals(object other)
    {
      if (!(other is Venue))
      {
        return false;
      }
      else
      {
        Venue otherVenue = (Venue)other;
        return this.Title == otherVenue.Title;
      }
    }
    public override int GetHashCode()
    {
      return this.Title.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (name) VALUES (@name);";
      cmd.Parameters.Add(new MySqlParameter("@name", Name));
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Venue> GetAll()
    {
      List<Venue> output = new List<Venue> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        output.Add(new Venue(name, id));
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static Venue Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE id = @id;";
      cmd.Parameters.Add(new MySqlParameter("@id", searchId));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string name = "";

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Venue output = new Venue(name, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }
  }
}
