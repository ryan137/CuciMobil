using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuciMobilv4;
using CuciMobilv4.Models;

namespace CuciMobilv4.Controllers
{
    public class TransaksiController : Controller
    {
        private ProjCarWashEntities2 db = new ProjCarWashEntities2();

        // GET: Transaksi
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            var tblTransaksis = db.tblTransaksis.Include(t => t.tblCustomer);
            return View(tblTransaksis.ToList());
        }

        // GET: Transaksi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTransaksi tblTransaksi = db.tblTransaksis.Find(id);
            if (tblTransaksi == null)
            {
                return HttpNotFound();
            }
            return View(tblTransaksi);
        }

        // GET: Transaksi/Create
        public ActionResult Create(int id , string status)
        {
            var NamaCustomer = db.tblCustomers.Find(id).NamaCustomer;
            var NoHp = db.tblCustomers.Find(id).NoHp;
            var CheckStatus = db.tblTransaksis.Where(s => s.IdCustomer == id).Select(s => s.IdTransaksi).Count();
            var listpencucian = new List<VM_DetailPencucian>(); //Membuat list dari class
            var listjenispencucian = db.tblHargas.Select(a => a); //Dari Database
            var test = new VM_DetailPencucian();

            if(status == "Bayar")
            {
                foreach (var item in listjenispencucian)
                {
                    var jenis = new VM_DetailPencucian(); //Membuat single list untuk dimasukkan ke listpencucian(yang dari class)
                    jenis.IdJenisPencucian = item.IdJenisPencucian;
                    jenis.NamaPencucian = item.NamaJenisPencucian;
                    jenis.kecil = item.kecil;
                    jenis.sedang = item.sedang;
                    jenis.besar = item.besar;
                    listpencucian.Add(jenis);
                }
            }
            else
            {
                foreach (var item in listjenispencucian)
                {
                    var jenis = new VM_DetailPencucian(); //Membuat single list untuk dimasukkan ke listpencucian(yang dari class)
                    jenis.IdJenisPencucian = item.IdJenisPencucian;
                    jenis.NamaPencucian = item.NamaJenisPencucian;
                    jenis.kecil = 0;
                    jenis.sedang = 0;
                    jenis.besar = 0;
                    listpencucian.Add(jenis);
                }
            }

            

            var model = new VM_Trans();
            model.StatusPembayaran = status;
            model.DetailTrans = listpencucian; //Memasukkan list dari class Detail ke list diclass Trans lalu dikirim ke view

            ViewBag.NamaCustomer = NamaCustomer;
            ViewBag.NoHp = NoHp;
            ViewBag.IdCustomer = id;
            return View(model);
        } // End Create

        // POST: Transaksi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VM_Trans trans)
        {
            db.SaveChanges();
            var check = trans.DetailTrans.Where(x => x.IsSelected == true).Count();
            
            if(check < 1)
            {
                ModelState.AddModelError("IdJenisPencucian", "Pilih jenis pencucian!");
            }

            if (ModelState.IsValid)
            {
                decimal total = 0;
                decimal hargacuci = 0;
                    foreach (var dataTrans in trans.DetailTrans)
                    {
                        if (dataTrans.IsSelected == true)
                        {
                            if(trans.StatusPembayaran == "Bayar")
                            {
                                if (trans.ukuran == "Kecil")
                                {
                                    hargacuci = (from harga in db.tblHargas
                                                 where harga.IdJenisPencucian == dataTrans.IdJenisPencucian
                                                 select harga.kecil).SingleOrDefault();
                                    total += hargacuci;


                                }
                                else if (trans.ukuran == "Sedang")
                                {
                                    hargacuci = (from harga in db.tblHargas
                                                 where harga.IdJenisPencucian == dataTrans.IdJenisPencucian
                                                 select harga.sedang).SingleOrDefault();
                                    total += hargacuci;
                                }
                                else
                                {
                                    hargacuci = (from harga in db.tblHargas
                                                 where harga.IdJenisPencucian == dataTrans.IdJenisPencucian
                                                 select harga.besar).SingleOrDefault();
                                    total += hargacuci;
                                }
                            }
                            else
                            {
                                total = 0;
                            }
                            
                            var datajenis = new tblDetailPencucian
                            {
                                IdTransaksi = dataTrans.IdTransaksi,
                                IdJenisPencucian = dataTrans.IdJenisPencucian,
                                Ukuran = trans.ukuran
                            };
                            db.tblDetailPencucians.Add(datajenis);
                        }
                    } // End foreach
                var datacustomer = new tblTransaksi
                {
                    IdCustomer = trans.IdCustomer,
                    NoKendaraan = trans.NoKendaraan,
                    TanggalTransaksi = DateTime.Now,
                    TotalHarga = total,
                    StatusPembayaran = trans.StatusPembayaran
                };
                db.tblTransaksis.Add(datacustomer);

                db.SaveChanges();
                return RedirectToAction("Index");

            } // End Model Valid
            return View();
        } // End Create Post

        // GET: Transaksi/Edit/5
        public ActionResult Edit(int? id)
        {
            var NamaCustomer = (from trans in db.tblTransaksis
                                join data in db.tblCustomers
                                on trans.IdCustomer equals data.IdCustomer
                                where trans.IdTransaksi == id
                                select data.NamaCustomer).SingleOrDefault();
            var NoHp = (from trans in db.tblTransaksis
                                join data in db.tblCustomers
                                on trans.IdCustomer equals data.IdCustomer
                                where trans.IdTransaksi == id
                                select data.NoHp).SingleOrDefault();
            
            var listpencucian = new List<VM_DetailPencucian>(); //Membuat list dari class
            var listjenispencucian = db.tblHargas.Select(a => a); //Dari Database
            

            foreach (var item in listjenispencucian)
            {
                var jenis = new VM_DetailPencucian(); //Membuat single list untuk dimasukkan ke listpencucian(yang dari class)
                var count = db.tblDetailPencucians.Where(s => s.IdTransaksi == id && s.IdJenisPencucian == item.IdJenisPencucian).Count();// jenis.IdJenisPencucian).Count();
                jenis.IdJenisPencucian = item.IdJenisPencucian;
                jenis.NamaPencucian = item.NamaJenisPencucian;
                if(count < 1)
                {
                    jenis.IsSelected = false;
                }
                else
                {
                    jenis.IsSelected = true;
                }
                listpencucian.Add(jenis);
            }

            var model = new VM_Trans();
            model.StatusPembayaran = db.tblTransaksis.Find(id).StatusPembayaran;
            model.ukuran = db.tblDetailPencucians.Where(s => s.IdTransaksi == id).Select(s => s.Ukuran).FirstOrDefault();
            model.NoKendaraan = db.tblTransaksis.Find(id).NoKendaraan;
            model.TanggalTransaksi = db.tblTransaksis.Find(id).TanggalTransaksi;
            model.DetailTrans = listpencucian; //Memasukkan list dari class Detail ke list diclass Trans lalu dikirim ke view
            ViewBag.NamaCustomer = NamaCustomer;
            ViewBag.NoHp = NoHp;
            return View(model);
        }

        // POST: Transaksi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VM_Trans trans)
        {

            db.SaveChanges();
            var check = trans.DetailTrans.Where(x => x.IsSelected == true).Count();

            if (check < 1)
            {
                ModelState.AddModelError("IdJenisPencucian", "Pilih jenis pencucian!");
            }
            
            if (ModelState.IsValid)
            {

                decimal total = 0;
                decimal hargacuci = 0;
                foreach (var dataTrans in trans.DetailTrans)
                {
                    if (dataTrans.IsSelected == true)
                    {
                        if (trans.StatusPembayaran == "Bayar")
                        {
                            if (trans.ukuran == "Kecil")
                            {
                                hargacuci = (from harga in db.tblHargas
                                             where harga.IdJenisPencucian == dataTrans.IdJenisPencucian
                                             select harga.kecil).SingleOrDefault();
                                total += hargacuci;


                            }
                            else if (trans.ukuran == "Sedang")
                            {
                                hargacuci = (from harga in db.tblHargas
                                             where harga.IdJenisPencucian == dataTrans.IdJenisPencucian
                                             select harga.sedang).SingleOrDefault();
                                total += hargacuci;
                            }
                            else
                            {
                                hargacuci = (from harga in db.tblHargas
                                             where harga.IdJenisPencucian == dataTrans.IdJenisPencucian
                                             select harga.besar).SingleOrDefault();
                                total += hargacuci;
                            }
                        }
                        else
                        {
                            total = 0;
                        }

                        var datajenis = new tblDetailPencucian
                        {
                            IdTransaksi = dataTrans.IdTransaksi,
                            IdJenisPencucian = dataTrans.IdJenisPencucian,
                            Ukuran = trans.ukuran
                        };
                        db.Entry(db.tblDetailPencucians).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                } // End foreach

                var datacustomer = new tblTransaksi
                {
                    NoKendaraan = trans.NoKendaraan,
                };
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
                return View();
        }

        // GET: Transaksi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTransaksi tblTransaksi = db.tblTransaksis.Find(id);
            if (tblTransaksi == null)
            {
                return HttpNotFound();
            }
            return View(tblTransaksi);
        }

        // POST: Transaksi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblTransaksi tblTransaksi = db.tblTransaksis.Find(id);
            db.tblTransaksis.Remove(tblTransaksi);
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
