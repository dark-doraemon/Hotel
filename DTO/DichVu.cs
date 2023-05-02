using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DichVu
    {
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }   
        public int SoLuong { get; set; } 
        public int GiaDichVu { get; set; }   
        public DichVu() { }
        public DichVu(string maDichVu, string tenDichVu, int soLuong, int giaDichVu)
        {
            MaDichVu = maDichVu;
            TenDichVu = tenDichVu;
            SoLuong = soLuong;
            GiaDichVu = giaDichVu;
        }
    }
}
