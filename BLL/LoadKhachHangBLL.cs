using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;

namespace BLL
{
    public class LoadKhachHangBLL
    {
        public void CheckLoadKhachHangBLL( ref List<ThongTinKhachHangDangKyPhong> khachhang)
        {
            DataAccess.LoadKhachHangToList(ref khachhang);
        }

        public DataTable CheckLoadKhachHangBLL2()
        {
            return DataAccess.LoadKhachHangToList2();
        }
    }
}
