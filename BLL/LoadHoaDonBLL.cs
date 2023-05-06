using DAL;
using DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoadHoaDonBLL
    {
        public  List<HoaDon> CheckLoadHoaDon()
        {
            return DataAccess.LoadHoaDonDAL();
        }
    }
}
