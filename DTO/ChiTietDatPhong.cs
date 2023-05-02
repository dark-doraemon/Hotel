using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDatPhong
    {
        public string MaDatPhong { get; set; }
        public string MaPhong { get; set; }
        public string MaKH { get; set; }
        public string MaDichVu { get; set; }    
        public string SoPhong { get; set; } 
        public  DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }

        public ChiTietDatPhong() { }
        public ChiTietDatPhong(string madatphong ="",string maphong = "",string makh = "",string madichvu = "",string sophong = "",DateTime datein = default(DateTime), DateTime dateout = default(DateTime))
        {
            this.MaDatPhong = madatphong;
            this.MaPhong = maphong;
            this.MaKH = makh;
            this.MaDichVu = madichvu;
            this.SoPhong = sophong;
            this.DateIn = datein;
            this.DateOut = dateout;
        }
    }
}
