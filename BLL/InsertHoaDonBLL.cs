using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InsertHoaDonBLL
    {
        public void CheckInsertHoaDon(string madatphong,double tongtien,DateTime ngayinhoadon)
        {
            DataAccess.InsertHoaDonDAL(madatphong,tongtien,ngayinhoadon);
        }
    }
}
