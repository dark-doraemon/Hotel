using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.Window
{
    /// <summary>
    /// Interaction logic for HoaDon.xaml
    /// </summary>
    public partial class HoaDon : System.Windows.Window
    {

        public HoaDon(List<ThongTinCuaPhong> thongtin,string tenkhachhang,DateTime ngayinhoadon)
        {
            InitializeComponent();
            txb_TenKhachHang.Text = tenkhachhang;

            txb_SoPhong.Text = thongtin[0].TenPhong;

            txb_NgayInHoaDon.Text = ngayinhoadon.ToString();

            txb_NhanVienInHoaDon.Text = thongtin[0].TenNhanVien;
            lsv_HoaDon.ItemsSource = thongtin;

            txb_GiaPhong.Text = string.Format("{0:n0}", (Convert.ToDouble(thongtin[0].GiaPhong)));

            int songay = thongtin[0].DateOut.Subtract(thongtin[0].DateIn).Days;
            txb_SoNgay.Text = songay.ToString();

            txb_ThanhTien.Text = string.Format("{0:n0}", (Convert.ToDouble(thongtin[0].GiaPhong) * songay));

            double tongtiendv = 0;
            for(int i = 0;i < thongtin.Count;i++)
            {
                Debug.WriteLine(thongtin[i].ThanhTien);
                tongtiendv += thongtin[i].ThanhTien;
            }

            txb_TongTien.Text = string.Format("{0:n0}", ((Convert.ToDouble(thongtin[0].GiaPhong) * songay) + tongtiendv));
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();    
            if(printDialog.ShowDialog() == true )
            {
                printDialog.PrintVisual(print, "Hóa đơn");
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
