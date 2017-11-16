using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuciMobilv4.Models
{
    public class VM_DetailPencucian
    {
        public int IdDetail{ get; set; }
        public int IdTransaksi { get; set; }
        public int IdJenisPencucian { get; set; }
        public string NamaPencucian { get; set; }

        public decimal kecil { get; set; }
        public decimal sedang { get; set; }
        public decimal besar { get; set; }
        public decimal gratis { get; set; }
        public bool IsSelected { get; set; }
    }
}