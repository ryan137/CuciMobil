using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuciMobilv4.Models
{
    public class VM_Trans
    {
        public int IdTransaksi { get; set; }
        public int IdCustomer { get; set; }
        public int IdJenisPencucian { get; set; }
        public string NoKendaraan { get; set; }

        public DateTime TanggalTransaksi { get; set; }

        public int totalharga { get; set; }
        public string ukuran { get; set; }
        public string StatusPembayaran { get; set; }
        public List<VM_DetailPencucian> DetailTrans { get; set; }
    }
}