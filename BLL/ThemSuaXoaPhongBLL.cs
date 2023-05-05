using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ThemSuaXoaPhongBLL
    {
        public string CheckThemPhong(string tenphong, string giaphong, string loaiphong)
        {
            for(int i = 0;i<giaphong.Length;i++)
            {
                if (!(giaphong[i] >='0' && giaphong[i] <='9'))
                {
                    return "code_error_giaphong";
                }
            }
            return DataAccess.CheckThemPhongDAL(tenphong,giaphong, loaiphong);
        }

        public void CheckXoaPhong(string maphong)
        {
            DataAccess.XoaPhongDAL(maphong);
        }
    }
}
