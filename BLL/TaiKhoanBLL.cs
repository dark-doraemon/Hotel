using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Diagnostics;
using DAL;
namespace BLL
{
    public class TaiKhoanBLL
    {

        TaiKhoanAccess tkAccess = new TaiKhoanAccess();
        public string CheckLoginBLL(TaiKhoan tk)
        {
            //kiểm tra nghiệp vụ, check những lỗi cơ bản để không phải truy cập CSDL
            if (tk.username == "")
            {
                return "error_user";
            }
            if (tk.password == "")
            {
                return "error_pass";
            }

            //nếu mà tài khoản không lỗi(lỗi ở đây là không nhập)
            //thì chuyển dữ liệu xuống DAL để check trong CSDL
            string info = tkAccess.CheckLoginDAL(tk);
            return info;
        }


    }
}
