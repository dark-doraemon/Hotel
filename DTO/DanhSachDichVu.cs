using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DanhSachDichVu
    {
        public string TenDichVu { get; set; }
        public int GiaTien { get; set; }
        public int SoLuong { get; set; } 
        public int ThanhTien { get; set; }


        DanhSachDichVu() { }
        public DanhSachDichVu(string tendv, int giatien,int soluong = 0,int thanhtien = 0)
        {
            this.TenDichVu = tendv;
            this.GiaTien   = giatien;
            this.SoLuong = soluong;
            this.ThanhTien = thanhtien;
        }

        
    }

    public class LoadDV
    {
        public List<DanhSachDichVu> list = new List<DanhSachDichVu>();
        public void add()
        {
            list.Add(new DanhSachDichVu("Coca", 10000));
            list.Add(new DanhSachDichVu("Pepsi", 10000));
            list.Add(new DanhSachDichVu("Sting", 10000));
            list.Add(new DanhSachDichVu("Bia", 12000));
            list.Add(new DanhSachDichVu("Mì xào", 30000));
            list.Add(new DanhSachDichVu("Cơm chiên", 30000));
            list.Add(new DanhSachDichVu("Cá viên chiên", 30000));

        }
    }
}
