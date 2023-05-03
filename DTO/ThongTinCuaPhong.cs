using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinCuaPhong
    {
        public string MaPhong { get; set; } 
        public string TenDichVu { get; set; }
        public int SoLuong { get; set; }
        public double GiaDichVu { get; set; }   
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }

        public ThongTinCuaPhong() { }
        public ThongTinCuaPhong(string maphong,string tendichvu,int soluong,double giadichvu,DateTime datein,DateTime dateout)
        {
            this.MaPhong = maphong;
            this.TenDichVu = tendichvu;
            this.SoLuong = soluong;
            this.GiaDichVu = giadichvu;
            this.DateIn = datein;
            this.DateOut = dateout;
        }
    }
}
