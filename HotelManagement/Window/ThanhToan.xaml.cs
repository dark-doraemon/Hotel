using BLL;
using DTO;
using GUI.Window.UserControls;
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
    /// Interaction logic for ThanhToan.xaml
    /// </summary>
    public partial class ThanhToan : System.Windows.Window
    {

        List<ThongTinCuaPhong> thongtin = new List<ThongTinCuaPhong> ();
        public void LayThongTinCuaPhong(ref List<ThongTinCuaPhong> thongtin,string maphong)
        {
            LoadThongTinPhongBLL load = new LoadThongTinPhongBLL();
            load.CheckLoadThongTinPhongBLL(ref thongtin,maphong);
        }

        public  void GanGiaTri()
        {
            txb_DateIn.Text = thongtin[0].DateIn.ToString("dd/MM/yyyy");
            txb_DateOut.Text = thongtin[0].DateOut.ToString("dd/MM/yyyy");
            dtg_DVDaChon.ItemsSource = thongtin;
            dtg_DVDaChon.Height = thongtin.Count * 30;
            
        }
        public ThanhToan(string maphong, string tenphong, string tenkhachhang, string giaphong)
        {
            InitializeComponent();

            LayThongTinCuaPhong(ref thongtin, maphong);
            txb_TenPhong.Text = tenphong;
            txb_TenKhachHang.Text = tenkhachhang;
            GanGiaTri();

            //tiền phòng
            int ngayvao = thongtin[0].DateIn.Day;
            int ngayra = thongtin[0].DateOut.Day;
            double tongtienphong = Convert.ToDouble(giaphong) * (ngayra - ngayvao);
            txb_TienPhong.Text = string.Format("{0:n0}", tongtienphong);

            //tiền dịch vụ
            double tongtiendichvu = 0;
            thongtin.ForEach(i=> tongtiendichvu += i.GiaDichVu);
            string str_tongtiendichvu = string.Format("{0:n0}", tongtiendichvu);
            txb_TienDichVu.Text = str_tongtiendichvu;

            //tổng tiền
            txb_TongTien.Text = string.Format("{0:n0}", tongtiendichvu + tongtienphong);

        }


        #region thanh kéo
        private void gridHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region đóng ứng dụng
        private void red_exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btn_Dong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ThanhToan_Click(object sender, RoutedEventArgs e)
        {
            HoaDon d = new HoaDon();
            d.ShowDialog();
        }
    }
}
