using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Collections.ObjectModel;

namespace BLL
{
    public class LoadKhachHangBLL
    {
        public List<ThongTinKhachHangDangKyPhong> CheckLoadKhachHangBLL()
        {
            return DataAccess.LoadKhachHangToList();
        }

        public DataTable CheckLoadKhachHangBLL2()
        {
            return DataAccess.LoadKhachHangToList2();
        }
    }
}
