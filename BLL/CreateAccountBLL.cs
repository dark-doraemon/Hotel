using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CreateAccountBLL
    {
        CreateAccountAccess createAccess = new CreateAccountAccess();//tạo 1 đối tượng ở DAL
        public string CheckCreateAccount(NhanVien nv, TaiKhoan tk)
        {
            //kiểm tra nghiệp vụ, nhưng lỗi sai cơ bản để không phải truy cập CSDL
            if (nv.TenNhanVien == "") return "code_ten_hien_thi";

            if (nv.SDTNhanVien.Contains(' ') || nv.SDTNhanVien == "") return "code_phone_number";

            for(int i = 0;i < nv.SDTNhanVien.Length;i++)
            {
                if (!(nv.SDTNhanVien[i] >='0' && nv.SDTNhanVien[i] <= '9'))
                {
                    return "code_phone_number";
                }
            }

            if (tk.username.Contains(' ') || tk.username == "") return "code_user_name";
            if (tk.password != tk.confirm ) return "code_password";


            //BLL.CreateAccountBLL tk = new CreateAccountBLL();
            //string info = createAccess.CheckCreateAccountDAL(nv, tk);
            //return info;
            string info = createAccess.CheckCreateAccountDAL(nv, tk);
            return info;
        }
    }
}
