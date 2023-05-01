using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoadRoomBLL
    {
        public void CheckLoadRoomBLL(ref List<Phong> phongdon, ref List<Phong> phongdoi, ref List<Phong> phonggiadinh)
        {
            DataAccess.LoadRoomToList(ref phongdon,ref phongdoi, ref phonggiadinh);
        }
    }
}
