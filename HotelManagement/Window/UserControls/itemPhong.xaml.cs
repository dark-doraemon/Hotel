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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelManagement.GUI;

namespace GUI.Window.UserControls
{
    /// <summary>
    /// Interaction logic for itemPhong.xaml
    /// </summary>
    public partial class itemPhong : UserControl
    {
        public itemPhong()
        {
            InitializeComponent();
        }


        #region sự kiện mỗi khi bấm vào phòng
        private void Room_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(txb_TrangThaiPhong.Text == "Đã thuê")
            {
                ThanhToan thanhtoan = new ThanhToan(txb_MaPhong.Text, txb_NameRoom.Text,txb_TenKhachHang.Text);
                thanhtoan.ShowDialog();
            }
            else
            {
                DatPhong windowdatphong = new DatPhong(txb_MaPhong.Text, txb_NameRoom.Text);
                windowdatphong.txb_TienPhong.Text = txb_GiaPhong.Text;
                windowdatphong.txt_TenPhong.Text = txb_NameRoom.Text;
                windowdatphong.ShowDialog();
            }
            

        }
        #endregion

        
    }
}
