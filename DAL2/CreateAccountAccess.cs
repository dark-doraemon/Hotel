using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CreateAccountAccess:DataAccess
    {
        //TaiKhoanAccess từ BLL chuyển xuống DAL để check trong DataAccess

        public string CheckCreateAccountDAL(NhanVien nv, TaiKhoan tk)
        {
            string info = DataAccess.CheckCreateAccountFinalDAL(nv, tk);
            return info;
        }
    }
}
