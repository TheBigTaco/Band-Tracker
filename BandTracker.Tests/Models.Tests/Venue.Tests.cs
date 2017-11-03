using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    private Venue blue = new Venue("Madison Square Garden");
    private Venue heathen = new Venue("Some Dingy Bar");
    public void Dispose()
    {
      Band.ClearAll();
    }
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    [TestMethod]
    public void Equals_AreTheSame_True()
    {
      Venue blue2 = new Venue("Madison Square Garden");
      Assert.AreEqual(blue, blue2);
    }
    [TestMethod]
    public void Save_AddToDatabase_EntryAdded()
    {
      blue.Save();

      int result = Venue.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Find_FindsInDatabase_EntryFound()
    {
      blue.Save();
      heathen.Save();

      Venue result = Venue.Find(heathen.Id);

      Assert.AreEqual(heathen, result);
    }
    [TestMethod]
    public void DeleteVenue_DeleteVenue_VenueDeleted()
    {
      blue.Save();
      Venue.DeleteVenue(blue.Id);
      int result = Venue.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void UpdateVenue_UpdateVenue_VenueUpdated()
    {
      blue.Save();
      blue.UpdateVenue("over there", blue.Id);
      Venue test = new Venue("over there");

      Assert.AreEqual(test, Venue.GetAll()[0]);
    }
  }
}
