using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class XoaHoaDonBLL
    {
        public void CheckXoaHoaDon(string maHoaDon)
        {
            DataAccess.XoaHoaDonDAL(maHoaDon);
        }
    }
}
