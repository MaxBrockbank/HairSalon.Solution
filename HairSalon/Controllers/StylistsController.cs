using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    private readonly HairSalonContext _db;
    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Stylist> model = _db.Stylists.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details (int id)
    {
      var thisStylist=_db.Stylists
        .Include(stylist=>stylist.Clients)
        .ThenInclude(join=>join.Client)
        .FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    public ActionResult Edit(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost]
    public ActionResult Edit(Stylist stylist, int ClientId)
    {
      _db.Entry(stylist).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      _db.Stylists.Remove(thisStylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddClient(int id)
    {
      var thisStylist=_db.Stylists.FirstOrDefault(stylists=>stylists.StylistId == id);
      ViewBag.ClientId = new SelectList((from s in _db.Clients select new {ClientId=s.ClientId, FullName=s.ClientFirstName + " " + s.ClientLastName}), "ClientId", "FullName", null);
      return View(thisStylist);
    } 

    [HttpPost]
    public ActionResult AddClient(Stylist stylist, int ClientId)
    {
      if (ClientId!=0)
      {
        _db.ClientStylist.Add(new ClientStylist(){ClientId=ClientId, StylistId=stylist.StylistId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteClient(int joinId)
    {
      var joinEntry = _db.ClientStylist.FirstOrDefault(entry=>entry.ClientStylistId == joinId);
      _db.ClientStylist.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}