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
    public class DetailPencucian : Controller
    {
        private ProjCarWashEntities2 db = new ProjCarWashEntities2();

        // GET: DetailPencucian
        public ActionResult Index()
        {
            var tblDetailPencucians = db.tblDetailPencucians.Include(t => t.tblHarga).Include(t => t.tblTransaksi);
            return View(tblDetailPencucians.ToList());
        }

        // GET: DetailPencucian/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDetailPencucian tblDetailPencucian = db.tblDetailPencucians.Find(id);
            if (tblDetailPencucian == null)
            {
                return HttpNotFound();
            }
            return View(tblDetailPencucian);
        }

        // GET: DetailPencucian/Create
        public ActionResult Create()
        {
            ViewBag.IdJenisPencucian = new SelectList(db.tblHargas, "IdJenisPencucian", "NamaJenisPencucian");
            ViewBag.IdTransaksi = new SelectList(db.tblTransaksis, "IdTransaksi", "NoKendaraan");
            return View();
        }

        // POST: DetailPencucian/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetail,IdTransaksi,IdJenisPencucian")] tblDetailPencucian tblDetailPencucian)
        {
            if (ModelState.IsValid)
            {
                db.tblDetailPencucians.Add(tblDetailPencucian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdJenisPencucian = new SelectList(db.tblHargas, "IdJenisPencucian", "NamaJenisPencucian", tblDetailPencucian.IdJenisPencucian);
            ViewBag.IdTransaksi = new SelectList(db.tblTransaksis, "IdTransaksi", "NoKendaraan", tblDetailPencucian.IdTransaksi);
            return View(tblDetailPencucian);
        }

        // GET: DetailPencucian/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDetailPencucian tblDetailPencucian = db.tblDetailPencucians.Find(id);
            if (tblDetailPencucian == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdJenisPencucian = new SelectList(db.tblHargas, "IdJenisPencucian", "NamaJenisPencucian", tblDetailPencucian.IdJenisPencucian);
            ViewBag.IdTransaksi = new SelectList(db.tblTransaksis, "IdTransaksi", "NoKendaraan", tblDetailPencucian.IdTransaksi);
            return View(tblDetailPencucian);
        }

        // POST: DetailPencucian/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetail,IdTransaksi,IdJenisPencucian")] tblDetailPencucian tblDetailPencucian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDetailPencucian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdJenisPencucian = new SelectList(db.tblHargas, "IdJenisPencucian", "NamaJenisPencucian", tblDetailPencucian.IdJenisPencucian);
            ViewBag.IdTransaksi = new SelectList(db.tblTransaksis, "IdTransaksi", "NoKendaraan", tblDetailPencucian.IdTransaksi);
            return View(tblDetailPencucian);
        }

        // GET: DetailPencucian/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDetailPencucian tblDetailPencucian = db.tblDetailPencucians.Find(id);
            if (tblDetailPencucian == null)
            {
                return HttpNotFound();
            }
            return View(tblDetailPencucian);
        }

        // POST: DetailPencucian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDetailPencucian tblDetailPencucian = db.tblDetailPencucians.Find(id);
            db.tblDetailPencucians.Remove(tblDetailPencucian);
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
