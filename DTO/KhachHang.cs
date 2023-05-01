using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhachHang
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }
        public string SDTKhachHang { get; set; }

        public KhachHang() { }

        public KhachHang(string makh,string tenkh,string diachi,string gioitinh,string sdtkhachhang)
        {
            this.MaKH = makh;
            this.TenKH = tenkh; 
            this.DiaChi= diachi;
            this.GioiTinh = gioitinh;
            this.SDTKhachHang = sdtkhachhang;
        }
    }
}
