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
            DatPhong windowdatphong = new DatPhong();
            windowdatphong.txt_TenPhong.Text = txb_NameRoom.Text;
            windowdatphong.ShowDialog();
        }
        #endregion

        
    }
}
