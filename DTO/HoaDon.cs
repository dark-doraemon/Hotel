using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDon
    {
        public string MaHoaDon { get; set; }
        public string MaKh { get; set; }
        public string TenKH { get; set; }
        public double TongHoaDon { get;set; }
        public  DateTime NgayInHoaDon { get; set; }
        public HoaDon() { }
        public HoaDon(string maHoaDon, string maKh,string tenkh, double tongHoaDon, DateTime ngayInHoaDon)
        {
            MaHoaDon = maHoaDon;
            MaKh = maKh;
            TenKH = tenkh;
            TongHoaDon = tongHoaDon;
            NgayInHoaDon = ngayInHoaDon;
        }
    }
}
