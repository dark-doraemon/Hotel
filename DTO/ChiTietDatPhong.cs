using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDatPhong
    {
        public ChiTietDatPhong() { }
        public string MaDatPhong { get; set; }
        public string MaPhong { get; set; }
        public string MaKH { get; set; }
        public string MaDichVu { get; set; }    
        public string SoPhong { get; set; } 
        public string DateIn { get; set; }
        public string DateOut { get; set; }
    }
}
