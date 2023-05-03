using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoadThongTinPhongBLL
    {
        public void CheckLoadThongTinPhongBLL(ref List<ThongTinCuaPhong> thongtin,string maphong)
        {
            DataAccess.LoadThongTinPhongToList(ref thongtin,maphong);
        }
    }
}
