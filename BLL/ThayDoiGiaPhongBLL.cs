using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ThayDoiGiaPhongBLL
    {
        public  string CheckThayDoiGia(string giaphongdon,string loaiphong)
        {
            for(int i = 0;i < giaphongdon.Length;i++)
            {
                if(!(giaphongdon[i] >= '0' && giaphongdon[i] <='9'))
                {
                    return "code_error_gia_phong";
                }
            }

            return DataAccess.ThayDoiGiaDaL(Convert.ToDouble( giaphongdon), loaiphong);
        }
    }
}
