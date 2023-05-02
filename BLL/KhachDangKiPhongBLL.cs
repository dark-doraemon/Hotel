using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class KhachDangKiPhongBLL
    {
        public string CheckKhachDangKiPhongBLL(ChiTietDatPhong ChiTietDatPhong,KhachHang Kh,List<DichVu> dv)
        {

            if (Kh.TenKH == "") return "code_ten_KH";
            if (Kh.DiaChi == "") return "code_diachi_KH";
            if (Kh.SDTKhachHang == "") return "code_SDT_KH";

            if (ChiTietDatPhong.DateIn == null) return "code_date_in";
            if (ChiTietDatPhong.DateOut == null) return "code_date_out";

            string info = DataAccess.insertChiTietDatPhong(ChiTietDatPhong,Kh,dv);
            return info;
        }
    }
}
