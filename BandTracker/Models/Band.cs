using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    public int Id {get; private set;}
    public string Name {get;}

    public Band(string name, int id = 0)
    {
      Id = id;
      Name = name;
    }

    public override bool Equals(object other)
    {
      if (!(other is Band))
      {
        return false;
      }
      else
      {
        Band otherBand = (Band)other;
        return this.Name == otherBand.Name;
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands (name) VALUES (@name);";
      cmd.Parameters.Add(new MySqlParameter("@name", Name));
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Band> GetAll()
    {
      List<Band> output = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        output.Add(new Band(name, id));
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static Band Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands WHERE id = @id;";
      cmd.Parameters.Add(new MySqlParameter("@id", searchId));
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string name = "";

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Band output = new Band(name, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static void AddBandToVenue(int bandId, int venueId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @venueId);";

      cmd.Parameters.Add(new MySqlParameter("@bandId", bandId));
      cmd.Parameters.Add(new MySqlParameter("@venueId", venueId));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Venue> GetVenues()
    {
      List<Venue> output = new List<Venue>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT venues.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) WHERE band_id = @bandId;";
      cmd.Parameters.Add(new MySqlParameter("@bandId", this.Id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string venueName = rdr.GetString(1);
        int venueId = rdr.GetInt32(0);
        Venue newVenue = new Venue(venueName, venueId);
        output.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands; DELETE FROM venues;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
