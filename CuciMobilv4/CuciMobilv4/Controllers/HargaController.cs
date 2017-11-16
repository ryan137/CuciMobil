using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuciMobilv4;

namespace CuciMobilv4.Controllers
{
    public class HargaController : Controller
    {
        private ProjCarWashEntities2 db = new ProjCarWashEntities2();

        // GET: Hargas
        public ActionResult Index()
        {
            return View(db.tblHargas.ToList());
        }

        // GET: Hargas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHarga tblHarga = db.tblHargas.Find(id);
            if (tblHarga == null)
            {
                return HttpNotFound();
            }
            return View(tblHarga);
        }

        // GET: Hargas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hargas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdJenisPencucian,NamaJenisPencucian,kecil,sedang,besar")] tblHarga tblHarga)
        {
            if (ModelState.IsValid)
            {
                db.tblHargas.Add(tblHarga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblHarga);
        }

        // GET: Hargas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHarga tblHarga = db.tblHargas.Find(id);
            if (tblHarga == null)
            {
                return HttpNotFound();
            }
            return View(tblHarga);
        }

        // POST: Hargas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdJenisPencucian,NamaJenisPencucian,kecil,sedang,besar")] tblHarga tblHarga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblHarga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblHarga);
        }

        // GET: Hargas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHarga tblHarga = db.tblHargas.Find(id);
            if (tblHarga == null)
            {
                return HttpNotFound();
            }
            return View(tblHarga);
        }

        // POST: Hargas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblHarga tblHarga = db.tblHargas.Find(id);
            db.tblHargas.Remove(tblHarga);
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
