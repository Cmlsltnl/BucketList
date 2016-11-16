﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BucketList.Models;

namespace BucketList.Controllers
{
    public class EntertainmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Entertainments
        public ActionResult Index()
        {
            var entertainments = db.entertainments.Include(e => e.EntertainmentsType);
            return View(entertainments.ToList());
        }

        // GET: Entertainments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entertainment entertainment = db.entertainments.Find(id);
            if (entertainment == null)
            {
                return HttpNotFound();
            }
            return View(entertainment);
        }

        // GET: Entertainments/Create
        public ActionResult Create()
        {
            ViewBag.EntertainmentTypeId = new SelectList(db.EntertainmentTypes, "EntertainmentTypeId", "EntertainmentsType");
            return View();
        }

        // POST: Entertainments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntertainmentId,Title,Description,Link,Location,EntertainmentTypeId")] Entertainment entertainment)
        {
            if (ModelState.IsValid)
            {
                db.entertainments.Add(entertainment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntertainmentTypeId = new SelectList(db.EntertainmentTypes, "EntertainmentTypeId", "EntertainmentsType", entertainment.EntertainmentTypeId);
            return View(entertainment);
        }

        // GET: Entertainments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entertainment entertainment = db.entertainments.Find(id);
            if (entertainment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntertainmentTypeId = new SelectList(db.EntertainmentTypes, "EntertainmentTypeId", "EntertainmentsType", entertainment.EntertainmentTypeId);
            return View(entertainment);
        }

        // POST: Entertainments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntertainmentId,Title,Description,Link,Location,EntertainmentTypeId")] Entertainment entertainment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entertainment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntertainmentTypeId = new SelectList(db.EntertainmentTypes, "EntertainmentTypeId", "EntertainmentsType", entertainment.EntertainmentTypeId);
            return View(entertainment);
        }

        // GET: Entertainments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entertainment entertainment = db.entertainments.Find(id);
            if (entertainment == null)
            {
                return HttpNotFound();
            }
            return View(entertainment);
        }

        // POST: Entertainments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entertainment entertainment = db.entertainments.Find(id);
            db.entertainments.Remove(entertainment);
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
