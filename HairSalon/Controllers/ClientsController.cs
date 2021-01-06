using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;
    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Clients.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.StylistId = new SelectList((from s in _db.Stylists select new {StylistId=s.StylistId, FullName=s.StylistFirstName + " " + s.StylistLastName}), "StylistId", "FullName", null);
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client, int StylistId)
    {
      _db.Clients.Add(client);
      if (StylistId !=0 )
      {
        _db.ClientStylist.Add(new ClientStylist(){StylistId=StylistId, ClientId=client.ClientId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisClient=_db.Clients
        .Include(client=>client.Stylists)
        .ThenInclude(join=>join.Stylist)
        .FirstOrDefault(client=>client.ClientId==id);
      
      return View(thisClient);
    }

    public ActionResult Edit(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      ViewBag.StylistId = new SelectList((from s in _db.Stylists select new {StylistId=s.StylistId, FullName=s.StylistFirstName + " " + s.StylistLastName}), "StylistId", "FullName", null);
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult Edit(Client client, int StylistId)
    {
      if(StylistId != 0)
      {
        _db.ClientStylist.Add(new ClientStylist(){StylistId=StylistId, ClientId=client.ClientId});
      }
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddStylist(int id)
    {
      var thisClient = _db.Clients.FirstOrDefault(clients=>clients.ClientId==id);
      ViewBag.StylistId = new SelectList((from s in _db.Stylists select new {StylistId=s.StylistId, FullName=s.StylistFirstName + " " + s.StylistLastName}), "StylistId", "FullName", null);
      return View(thisClient);
    }
    
    [HttpPost]
    public ActionResult AddStylist(Client client, int StylistId)
    {
      if(StylistId!=0)
      {
        _db.ClientStylist.Add(new ClientStylist(){StylistId = StylistId, ClientId = client.ClientId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");

    }


    public ActionResult Delete(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      return View(thisClient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      _db.Clients.Remove(thisClient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteStylist(int joinId)
    {
      var joinEntry = _db.ClientStylist.FirstOrDefault(entry=>entry.ClientStylistId == joinId);
      _db.ClientStylist.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}