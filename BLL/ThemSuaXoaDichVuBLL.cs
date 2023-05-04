using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ThemSuaXoaDichVuBLL
    {
        public string CheckSuaDichVuBLL(string madv, string tendv, string giadv)
        {
            for (int i = 0; i < giadv.Length; i++)
            {
                if (!(giadv[i] >= '0' && giadv[i] <= '9'))
                {
                    return "code_error_giadv";
                }
            }
            if (tendv == "" || giadv == "")
            {
                return "code_string";
            }
            return DataAccess.SuaDichVuDAL(madv,tendv, giadv);
        }

        public string CheckThemDichVuBLL(string tendv, string giadichvu)
        {

            for(int i = 0; i < giadichvu.Length; i++)
            {
                if (!(giadichvu[i] >='0' && giadichvu[i] <='9'))
                {
                    return "code_error_giadv";
                }
            }
            if (tendv == "" || giadichvu == "")
            {
                return "code_string";
            }
            return DataAccess.ThemDichVuDaL(tendv,giadichvu);
        }

        public string CheckXoaDichVuBLL(string madv)
        {
            return DataAccess.XoaDichVuBLL(madv);
        }
    }
}
