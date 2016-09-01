using CarFuel.DataAccess.Contexts;
using CarFuel.Models;
using CarFuel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarFuel.Web.Controllers {
  public class HomeController : Controller {
    public ActionResult Index() {
      using (var db = new CarFuelDb()) {
        var fillUps = from f in db.FillUps
                      select f;
        return View(fillUps.ToList());
      }
    }

    public ActionResult About() {
      using (var db = new CarFuelDb()) {
        var f1 = new FillUp(1000, 40.0);
        var f2 = new FillUp(2000, 50.0);
        var f3 = new FillUp(2500, 20.0);

        f1.NextFillUp = f2;
        f2.NextFillUp = f3;

        db.FillUps.Add(f1);
        db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}