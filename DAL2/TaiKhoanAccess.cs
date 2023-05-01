using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using DAL;

namespace DAL
{
    public class TaiKhoanAccess:DataAccess
    {

        //createAcccountAccess từ BLL chuyển xuống DAL để check trong DataAccess
        public string CheckLoginDAL(TaiKhoan tk)
        {
            
            string info = DataAccess.CheckLoginFinalDAL(tk);
            return info;
            
        }
    }
}
