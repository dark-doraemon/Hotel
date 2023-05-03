using BLL;
using DTO;
using GUI.Window.UserControls;
using System;
using System.Collections.Generic;
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
        public void SetThongTinCuaPhong(ref List<ThongTinCuaPhong> thongtin)
        {
            LoadThongTinPhongBLL load = new LoadThongTinPhongBLL();
            load.CheckLoadThongTinPhongBLL(ref thongtin);
        }
        public ThanhToan(string maphong,string tenphong,string tenkhachhang)
        {
            InitializeComponent();
            txb_TenPhong.Text = tenphong;
            txb_TenKhachHang.Text = tenkhachhang;
            SetThongTinCuaPhong(ref thongtin);
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
    }
}
