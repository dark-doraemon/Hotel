using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinKhachHangDangKyPhong
    {
        public string MaKH { get; set; }
        public string TenKH { get; set;}
        public string DiaChiKH { get; set; }
        public string GioiTinh { get; set; }
        public string SDTKhachHang { get; set; }
        public string DateIn { get; set; }
        public string DateOut { get; set; }   
        public string TenPhong { get; set; }    
        public ThongTinKhachHangDangKyPhong() { }

        public ThongTinKhachHangDangKyPhong(string maKH, string tenKH, string diaChiKH, string gioiTinh, string sDTKhachHang, string detein, string dateout, string tenphong)
        {
            MaKH = maKH;
            TenKH = tenKH;
            DiaChiKH = diaChiKH;
            GioiTinh = gioiTinh;
            SDTKhachHang = sDTKhachHang;
            this.DateIn = detein;
            this.DateOut = dateout;
            this.TenPhong = tenphong;
        }
    }
}
