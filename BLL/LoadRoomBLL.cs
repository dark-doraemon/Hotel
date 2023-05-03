using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoadRoomBLL
    {
        public void CheckLoadRoomBLL(ref ObservableCollection<Phong> phongdon, ref ObservableCollection<Phong> phongdoi, ref ObservableCollection<Phong> phonggiadinh)
        {
            DataAccess.LoadRoomToList(ref phongdon,ref phongdoi, ref phonggiadinh);
        }
    }
}
