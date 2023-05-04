using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoadBangGiaDVBLL
    {
        public void CheckLoadBangGiaDichVu(ref List<BangGiaDichVu> list)
        {
            DataAccess.LoadDichVuToList(ref list);
        }
        public DataTable CheckLoadBangGiaDichVu2()
        {
           return DataAccess.LoadDichVuToList2();
        }
    }
}
