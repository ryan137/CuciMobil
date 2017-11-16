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
    public class CustomersController : Controller
    {
        private ProjCarWashEntities2 db = new ProjCarWashEntities2();

        // GET: Customers
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            int i = 0;
            string TextStatus, WarnaStatus;
            List<int> listKunjungan = new List<int>();
            var listWarna = new List<string>();
            var listText = new List<string>();
            foreach (var data in db.tblCustomers)
            {
                var CheckStatus = db.tblTransaksis.Where(s => s.IdCustomer == data.IdCustomer).Select(s => s.IdTransaksi).Count();
                listKunjungan.Add(CheckStatus);
            }
            var customerlist = db.tblCustomers.ToList();
            foreach (var item in listKunjungan)
            {
                if (listKunjungan[i] != 0 && listKunjungan[i] < 7)
                {
                    if (listKunjungan[i] % 6 == 0)
                    {
                        TextStatus = "Free";
                        WarnaStatus = "btn btn-success";
                        listText.Add(TextStatus);
                        listWarna.Add(WarnaStatus);
                    }
                    else
                    {
                        TextStatus = "Bayar";
                        WarnaStatus = "btn btn-primary";
                        listText.Add(TextStatus);
                        listWarna.Add(WarnaStatus);
                    }
                }
                else if(listKunjungan[i] > 6)
                {
                    if (listKunjungan[i] % 7 == 6)
                    {
                        TextStatus = "Free";
                        WarnaStatus = "btn btn-success";
                        listText.Add(TextStatus);
                        listWarna.Add(WarnaStatus);
                    }
                    else
                    {
                        TextStatus = "Bayar";
                        WarnaStatus = "btn btn-primary";
                        listText.Add(TextStatus);
                        listWarna.Add(WarnaStatus);
                    }
                }
                else
                {
                    TextStatus = "Bayar";
                    WarnaStatus = "btn btn-primary";
                    listText.Add(TextStatus);
                    listWarna.Add(WarnaStatus);
                }
                
                i++;
            }
            ViewBag.Customer = customerlist;
            ViewBag.listText = listText;
            ViewBag.listWarna = listWarna;
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            var tblTransaksi = db.tblTransaksis.Where(s => s.IdCustomer == id);
            ViewBag.Customer = tblCustomer;
            ViewBag.Transaksi = tblTransaksi;
            return View();
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCustomer,NamaCustomer,NoHp,alamat")] tblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                db.tblCustomers.Add(tblCustomer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCustomer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCustomer,NamaCustomer,NoHp,alamat")] tblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCustomer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            db.tblCustomers.Remove(tblCustomer);
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
