using CarFuel.Models;
using CarFuel.Services;
using CarFuel.Services.Bases;
using CarFuel.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarFuel.Web.Controllers {
  public class CarsController : AppControllerBase {
    private readonly ICarService _carService;

    public CarsController(ICarService carService, IUserService userService) : base(userService) {
      _carService = carService;
    }

    // GET: Cars
    public ActionResult Index() {
      if (User.Identity.IsAuthenticated) {
        var cars = _carService.All();

        ViewBag.AppUser = _userService.CurrentUser;

        return View("IndexForMember", cars);
      }
      else {
        return View("IndexForAnonymous");
      }
    }

    // GET: AddCar
    public ActionResult Add() {
      return View();
    }

    [HttpPost]
    public ActionResult Add(Car item) {
      ModelState.Remove("Owner");
      if (ModelState.IsValid) {

        User u = _userService.Find(new Guid(User.Identity.GetUserId()));
        item.Owner = u;

        _carService.Add(item);
        _carService.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(item);
    }

    public ActionResult AddFillUp(Guid id) {
      // do something
      //var c = db.Cars.Find(id);
      //if (c == null) {
      //  return HttpNotFound();
      //}

      //ViewBag.CarName = c.Name;
      var name = (from c in _carService.All()
                  where c.Id == id
                  select c.Name).SingleOrDefault();
      ViewBag.CarName = name;
      return View();
    }

    [HttpPost]
    public ActionResult AddFillUp(Guid id, [Bind(Exclude = "Id")]FillUp item) {
      // Not validate "Id" same [Bind(Exclude = "Id")] on receive parameter of method.
      //ModelState.Remove("Id");
      // do something
      if (ModelState.IsValid) {
        var c = _carService.Find(id);
        // load relation "one" model.
        //db.Entry(item).Reference(x => x.NextFillUp).Load();
        // load relation "many" model.
        //db.Entry(c).Collection(x => x.FillUps).Load(); // Manual Load case not set "public virtual of parameter collection group."
        c.AddFillUp(item.Odometer, item.Liters);
        _carService.SaveChanges();

        return RedirectToAction("Index");
      }
      return View(item);
    }
  }
}