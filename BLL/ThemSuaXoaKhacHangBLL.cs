using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ThemSuaXoaKhacHangBLL
    {
        public string CheckThemSuaXoaKhacHangBLL(string makh, string tenkh, string diachi,string gioitinh, string sdt)
        {
            for(int i = 0; i< sdt.Length; i++)
            {
                if (!(sdt[i] >= '0' && sdt[i] <= '9'))
                {
                    return "code_error_sdt";
                }
            }
            return DataAccess.SuaThongTinKhachHangDAL(makh, tenkh, diachi,gioitinh, sdt);
        }
    }
}
