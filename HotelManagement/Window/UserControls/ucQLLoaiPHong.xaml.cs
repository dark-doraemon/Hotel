using BLL;
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

namespace GUI.Window.UserControls
{
    /// <summary>
    /// Interaction logic for ucQLLoaiPHong.xaml
    /// </summary>
    public partial class ucQLLoaiPHong : UserControl
    {
        public ucQLLoaiPHong()
        {
            InitializeComponent();
        }

        private void btn_PhongDon_Click(object sender, RoutedEventArgs e)
        {
            string giaphongdon = tb_ThayDoiGiaPhongDon.Text;
            ThayDoiGiaPhongBLL thaydoi = new ThayDoiGiaPhongBLL();
            string res = thaydoi.CheckThayDoiGia(giaphongdon, "Phòng đơn");
            if(res == "code_error_gia_phong")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else { MessageBox.Show("Thay đổi giá thành công"); }
        }
        private void btn_PhongDoi_Click(object sender, RoutedEventArgs e)
        {
            string giaphongdoi = tb_ThayDoiGiaPhongDoi.Text;
            ThayDoiGiaPhongBLL thaydoi = new ThayDoiGiaPhongBLL();
            string res = thaydoi.CheckThayDoiGia(giaphongdoi, "Phòng đôi");
            if (res == "code_error_gia_phong")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else { MessageBox.Show("Thay đổi giá thành công"); }
        }
        private void btn_PhongGiaDinh_Click(object sender, RoutedEventArgs e)
        {
            string giaphonggiadinh = tb_ThayDoiGiaPhongGiaDinh.Text;
            ThayDoiGiaPhongBLL thaydoi = new ThayDoiGiaPhongBLL();
            string res = thaydoi.CheckThayDoiGia(giaphonggiadinh, "Phòng gia đình");
            if (res == "code_error_gia_phong")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else { MessageBox.Show("Thay đổi giá thành công"); }
        }

       
    }
}
