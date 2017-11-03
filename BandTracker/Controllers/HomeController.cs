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
    }
}
