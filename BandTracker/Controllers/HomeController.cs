using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        List<Band> bands = Band.GetAll();
        return View(bands);
      }
      [HttpGet("/band/add")]
      public ActionResult AddBand()
      {
        return View("Add", "band");
      }
      [HttpPost("/band/new")]
      public ActionResult NewBand()
      {
        Band newBand = new Band(Request.Form["name"]);
        newBand.Save();

        return Redirect("/");
      }
      [HttpGet("/venue")]
      public ActionResult Venues()
      {
        List<Venue> venues = Venue.GetAll();
        return View(venues);
      }
      [HttpGet("/venue/add")]
      public ActionResult AddVenue()
      {
        return View("Add", "venue");
      }
      [HttpPost("/venue/new")]
      public ActionResult NewVenue()
      {
        Venue newVenue = new Venue(Request.Form["name"]);
        newVenue.Save();

        return Redirect("/venue");
      }
      [HttpGet("/band/{id}")]
      public ActionResult BandDetails(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band currentBand = Band.Find(id);
        List<Venue> bandVenues = currentBand.GetVenues();
        model.Add("band", currentBand);
        model.Add("venue", bandVenues);

        return View(model);
      }
      [HttpGet("/venue/{id}")]
      public ActionResult VenueDetails(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue currentVenue = Venue.Find(id);
        List<Band> venueBands = currentVenue.GetBands();
        model.Add("band", venueBands);
        model.Add("venue", currentVenue);

        return View(model);
      }
      [HttpGet("/band/{id}/venue")]
      public ActionResult ConnectBandToVenue(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band currentBand = Band.Find(id);
        List<Venue> venues = Venue.GetAll();
        model.Add("band", currentBand);
        model.Add("venue", venues);
        return View(model);
      }
      [HttpPost("/band/new/venue")]
      public ActionResult NewBandToVenueCon()
      {
        int bandId = int.Parse(Request.Form["band-id"]);
        int venueId = int.Parse(Request.Form["venues"]);
        Band.AddBandToVenue(bandId, venueId);
        return Redirect("/band/"+ bandId);
      }
      [HttpGet("/venue/{id}/band")]
      public ActionResult ConnectVenueToBand(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue currentVenue = Venue.Find(id);
        List<Band> bands = Band.GetAll();
        model.Add("band", bands);
        model.Add("venue", currentVenue);
        return View(model);
      }
      [HttpPost("/venue/new/band")]
      public ActionResult NewVenueToBandCon()
      {
        int bandId = int.Parse(Request.Form["bands"]);
        int venueId = int.Parse(Request.Form["venue-id"]);
        Band.AddBandToVenue(bandId, venueId);
        return Redirect("/venue/"+ venueId);
      }
      [HttpGet("/venue/{id}/delete")]
      public ActionResult DeleteThisVenue(int id)
      {
        Venue.DeleteVenue(id);
        return Redirect("/venue");
      }
      [HttpGet("/venue/{id}/update")]
      public ActionResult UpdateThisVenue(int id)
      {
        return View(id);
      }
      [HttpPost("/venue/{id}/updated")]
      public ActionResult VenueUpdated(int id)
      {
        Venue selectedVenue = Venue.Find(id);
        selectedVenue.UpdateVenue(Request.Form["new-name"]);

        return Redirect("/venue/"+id);
      }
    }
}
