using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Artistesta.Models;

namespace Artistesta.Controllers
{
    public class PUBLISHARTsController : Controller
    {
        private ArtistestaEntities db = new ArtistestaEntities();

        // GET: PUBLISHARTs
        public ActionResult Index()
        {
            var pUBLISHART = db.PUBLISHART.Include(p => p.USER);
            return View(pUBLISHART.ToList());
        }

        // GET: PUBLISHARTs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHART pUBLISHART = db.PUBLISHART.Find(id);
            if (pUBLISHART == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHART);
        }

        // GET: PUBLISHARTs/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.USERID = new SelectList(db.USER, "USERID", "USERNAME");
            return View();
        }

        // POST: PUBLISHARTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PUBLISHART pUBLISHART)
        {
            try { 
                db.PUBLISHART.Add(pUBLISHART);
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            catch(DbEntityValidationException ex)
            {
                Debug.WriteLine(ex);
                return View();
            }
        }

        // GET: PUBLISHARTs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHART pUBLISHART = db.PUBLISHART.Find(id);
            if (pUBLISHART == null)
            {
                return HttpNotFound();
            }
            ViewBag.USERID = new SelectList(db.USER, "USERID", "USERNAME", pUBLISHART.USERID);
            return View(pUBLISHART);
        }

        // POST: PUBLISHARTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ARTID,USERID,INSPIRATION,ARTWORK,TITLE,TIME,CATEGORY,ENCOURAGES,DISCOURAGES,FAVORITES,ISPINNED,FORSALE,MINIMUMBID,AUCTIONID,CURHIGHESTBID,CURBIDDERID")] PUBLISHART pUBLISHART)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pUBLISHART).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USERID = new SelectList(db.USER, "USERID", "USERNAME", pUBLISHART.USERID);
            return View(pUBLISHART);
        }

        // GET: PUBLISHARTs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHART pUBLISHART = db.PUBLISHART.Find(id);
            if (pUBLISHART == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHART);
        }

        // POST: PUBLISHARTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PUBLISHART pUBLISHART = db.PUBLISHART.Find(id);
            db.PUBLISHART.Remove(pUBLISHART);
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
