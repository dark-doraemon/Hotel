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
        public string GiaTien { get; set; }
        DanhSachDichVu() { }
        public DanhSachDichVu(string tendv, string giatien)
        {
            this.TenDichVu = tendv;
            this.GiaTien   = giatien;
        }

    }

    public class LoadDV
    {
        public List<DanhSachDichVu> list = new List<DanhSachDichVu>();
        public void add()
        {
            list.Add(new DanhSachDichVu("Coca", "10000"));
            list.Add(new DanhSachDichVu("Pepsi", "10000"));
            list.Add(new DanhSachDichVu("Sting", "10000"));
            list.Add(new DanhSachDichVu("Bia", "12000"));
            list.Add(new DanhSachDichVu("Mì xào", "30000"));
            list.Add(new DanhSachDichVu("Cơm chiên", "30000"));
            list.Add(new DanhSachDichVu("Cá viên chiên", "30000"));
            list.Add(new DanhSachDichVu("Cá viên chiên", "30000"));
            list.Add(new DanhSachDichVu("Cá viên chiên", "30000"));
            list.Add(new DanhSachDichVu("Cá viên chiên", "30000"));
            list.Add(new DanhSachDichVu("Cá viên chiên", "30000"));
            list.Add(new DanhSachDichVu("Cá viên chiên", "30000"));
            list.Add(new DanhSachDichVu("Cá viên chiên", "30000"));

        }
    }
}
