using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BLL
{
    public class LoadKhachHangBLL
    {
        public void CheckLoadKhachHangBLL( ref List<ThongTinKhachHangDangKyPhong> khachhang)
        {
            DataAccess.LoadKhachHangToList(ref khachhang);
        }
    }
}
