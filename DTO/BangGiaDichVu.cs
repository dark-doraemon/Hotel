using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BangGiaDichVu
    {
        public string MaGiaDichVu { get; set; }
        public string TenGiaDichVu { get;set; }
        public double GiaGiaDichVu { get; set; }

        public BangGiaDichVu() { }
        public BangGiaDichVu(string maGiaDichVu, string tenGiaDichVu, double giaGiaDichVu)
        {
            MaGiaDichVu = maGiaDichVu;
            TenGiaDichVu = tenGiaDichVu;
            GiaGiaDichVu = giaGiaDichVu;
        }
    }
}
