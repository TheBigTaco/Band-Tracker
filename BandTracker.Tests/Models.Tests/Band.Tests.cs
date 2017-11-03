using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    private Band blue = new Band("Blue Man Group");
    private Band heathen = new Band("Twenty One Pilots");
    private Venue place = new Venue("place");
    private Venue there = new Venue("there");
    public void Dispose()
    {
      Band.ClearAll();
    }
    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    [TestMethod]
    public void Equals_AreTheSame_True()
    {
      Band blue2 = new Band("Blue Man Group");
      Assert.AreEqual(blue, blue2);
    }
    [TestMethod]
    public void Save_AddToDatabase_EntryAdded()
    {
      blue.Save();

      int result = Band.GetAll().Count;

      Assert.AreEqual(1, result);
    }
    [TestMethod]
    public void Find_FindsInDatabase_EntryFound()
    {
      blue.Save();
      heathen.Save();

      Band result = Band.Find(heathen.Id);

      Assert.AreEqual(heathen, result);
    }
    [TestMethod]
    public void AddBandToVenue_AddsBandsToVenue_BandAddedToVenue()
    {
      blue.Save();
      heathen.Save();
      place.Save();
      Band.AddBandToVenue(blue.Id, place.Id);
      Band.AddBandToVenue(heathen.Id, place.Id);
      List<Band> result = Venue.GetBands(place.Id);
      List<Band> testList = new List<Band>{blue, heathen};
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void AddBandToVenue_AddsVenueToBands_VenueAddedToBand()
    {
      blue.Save();
      there.Save();
      place.Save();
      Band.AddBandToVenue(blue.Id, there.Id);
      Band.AddBandToVenue(blue.Id, place.Id);
      List<Venue> result = Band.GetVenues(blue.Id);
      List<Venue> testList = new List<Venue>{there, place};
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
