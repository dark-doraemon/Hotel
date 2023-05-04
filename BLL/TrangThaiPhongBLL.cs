using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TrangThaiPhongBLL
    {
        public void CheckTrangThaiPhong(string maphong)
        {
            DataAccess.ChuyenTrangThai(maphong);
        }
    }
}
