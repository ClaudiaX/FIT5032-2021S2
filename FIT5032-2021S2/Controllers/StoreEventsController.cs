using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_2021S2.Models;

namespace FIT5032_2021S2.Controllers
{
    public class StoreEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StoreEvents
        public ActionResult Index()
        {
            var storeEvents = db.storeEvents.Include(s => s.EventType).Include(s => s.Store);
            return View(storeEvents.ToList());
        }

        // GET: StoreEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreEvent storeEvent = db.storeEvents.Find(id);
            if (storeEvent == null)
            {
                return HttpNotFound();
            }
            return View(storeEvent);
        }

        // GET: StoreEvents/Create
        public ActionResult Create()
        {
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name");
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name");
            return View();
        }

        // POST: StoreEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreId,EventTypeId,StartTime,EndTime")] StoreEvent storeEvent)
        {
            if (ModelState.IsValid)
            {
                db.storeEvents.Add(storeEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name", storeEvent.EventTypeId);
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", storeEvent.StoreId);
            return View(storeEvent);
        }

        // GET: StoreEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreEvent storeEvent = db.storeEvents.Find(id);
            if (storeEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name", storeEvent.EventTypeId);
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", storeEvent.StoreId);
            return View(storeEvent);
        }

        // POST: StoreEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreId,EventTypeId,StartTime,EndTime")] StoreEvent storeEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name", storeEvent.EventTypeId);
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", storeEvent.StoreId);
            return View(storeEvent);
        }

        // GET: StoreEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreEvent storeEvent = db.storeEvents.Find(id);
            if (storeEvent == null)
            {
                return HttpNotFound();
            }
            return View(storeEvent);
        }

        // POST: StoreEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreEvent storeEvent = db.storeEvents.Find(id);
            db.storeEvents.Remove(storeEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
