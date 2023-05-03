        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Phong
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public double GiaPhong { get; set; }
        public string TrangThaiPhong { get; set; }
        public string DonDep { get; set; }
        public string TenKH { get; set; }
        public Phong() { }
        public Phong(string maPhong, string tenPhong, string loaiPhong, double giaPhong, string trangThaiPhong, string donDep,string kh)
        {
            MaPhong = maPhong;
            TenPhong = tenPhong;
            LoaiPhong = loaiPhong;
            GiaPhong = giaPhong;
            TrangThaiPhong = trangThaiPhong;
            DonDep = donDep;
            TenKH = kh;
        }

        
    }
}
