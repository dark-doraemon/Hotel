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
        public string TenPhong { get; set; }
        public double GiaPhong { get; set; }
        public string TenDichVu { get; set; }
        public double GiaGiaDichVu { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }   
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }

        public string TenNhanVien { get; set; }

        public string MaDatPhong { get; set; }

        public ThongTinCuaPhong() { }
        public ThongTinCuaPhong(string maphong,string tenphong,double giaphong,string tendichvu, double giagiadichvu,int soluong,double thanhtien,DateTime datein,DateTime dateout,string tennhanvien,string madatphong)
        {
            this.MaPhong = maphong;
            this.TenPhong = tenphong; 
            this.GiaPhong = giaphong;
            this.TenDichVu = tendichvu;
            this.GiaGiaDichVu = giagiadichvu;
            this.SoLuong = soluong;
            this.ThanhTien = thanhtien; 
            this.DateIn = datein;
            this.DateOut = dateout;
            this.TenNhanVien = tennhanvien; 
            this.MaDatPhong = madatphong;
        }
    }
}
