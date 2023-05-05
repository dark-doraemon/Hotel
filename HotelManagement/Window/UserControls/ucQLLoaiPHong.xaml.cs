using BLL;
using System;
using System.Collections.Generic;
using System.Data;
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

            LoadLoaiPhongBLL loadphongdon = new LoadLoaiPhongBLL();
            dtg_PhongDon.ItemsSource = loadphongdon.ChecKLoadLoaiPhong("Phòng đơn").DefaultView;


            LoadLoaiPhongBLL loadphongdoi = new LoadLoaiPhongBLL();
            dtg_PhongDoi.ItemsSource = loadphongdoi.ChecKLoadLoaiPhong("Phòng đôi").DefaultView;

            LoadLoaiPhongBLL loadphonggiadinh = new LoadLoaiPhongBLL();
            dtg_PhongGiaDinh.ItemsSource = loadphonggiadinh.ChecKLoadLoaiPhong("Phòng gia đình").DefaultView;
        }

        #region thay đổi giá phòng đơn
        private void btn_PhongDon_Click(object sender, RoutedEventArgs e)
        {
            string giaphongdon = tb_ThayDoiGiaPhongDon.Text;
            ThayDoiGiaPhongBLL thaydoi = new ThayDoiGiaPhongBLL();
            string res = thaydoi.CheckThayDoiGia(giaphongdon, "Phòng đơn");
            if(res == "code_error_gia_phong")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else 
            {
                LoadLoaiPhongBLL loadphongdon = new LoadLoaiPhongBLL();
                dtg_PhongDon.ItemsSource = loadphongdon.ChecKLoadLoaiPhong("Phòng đơn").DefaultView;
                MessageBox.Show("Thay đổi giá thành công"); 
            }
        }
        #endregion

        #region thay đổi giá phòng đôi
        private void btn_PhongDoi_Click(object sender, RoutedEventArgs e)
        {
            string giaphongdoi = tb_ThayDoiGiaPhongDoi.Text;
            ThayDoiGiaPhongBLL thaydoi = new ThayDoiGiaPhongBLL();
            string res = thaydoi.CheckThayDoiGia(giaphongdoi, "Phòng đôi");
            if (res == "code_error_gia_phong")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else 
            { 
                MessageBox.Show("Thay đổi giá thành công");
                LoadLoaiPhongBLL loadphongdoi = new LoadLoaiPhongBLL();
                dtg_PhongDoi.ItemsSource = loadphongdoi.ChecKLoadLoaiPhong("Phòng đôi").DefaultView;
            }
        }
        #endregion

        #region thay đôi giá phòng gia đình
        private void btn_PhongGiaDinh_Click(object sender, RoutedEventArgs e)
        {
            string giaphonggiadinh = tb_ThayDoiGiaPhongGiaDinh.Text;
            ThayDoiGiaPhongBLL thaydoi = new ThayDoiGiaPhongBLL();
            string res = thaydoi.CheckThayDoiGia(giaphonggiadinh, "Phòng gia đình");
            if (res == "code_error_gia_phong")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else 
            { 
                MessageBox.Show("Thay đổi giá thành công");
                LoadLoaiPhongBLL loadphonggiadinh = new LoadLoaiPhongBLL();
                dtg_PhongGiaDinh.ItemsSource = loadphonggiadinh.ChecKLoadLoaiPhong("Phòng gia đình").DefaultView;
            }
        }

        #endregion

        #region thêm phòng
        private void btn_ThemPhong_Click(object sender, RoutedEventArgs e)
        {
            string tenphong =txt_TenPhong.Text;
            string giaphong = txt_GiaPhong.Text;
            string? loaiphong = ((ComboBoxItem)cbo_LoaiPhong.SelectedItem).Content.ToString();
            ThemSuaXoaPhongBLL themphong = new ThemSuaXoaPhongBLL();
            string res = themphong.CheckThemPhong(tenphong, giaphong, loaiphong);
            if (res == "code_error_giaphong") MessageBox.Show("Giá không hợp lệ");
            else
            {
                MessageBox.Show("Thêm thành công");
                if (loaiphong == "Phòng đơn")
                {
                    LoadLoaiPhongBLL loadphongdon = new LoadLoaiPhongBLL();
                    dtg_PhongDon.ItemsSource = loadphongdon.ChecKLoadLoaiPhong("Phòng đơn").DefaultView;
                }


                else if (loaiphong == "Phòng đôi")
                {
                    LoadLoaiPhongBLL loadphongdoi = new LoadLoaiPhongBLL();
                    dtg_PhongDoi.ItemsSource = loadphongdoi.ChecKLoadLoaiPhong("Phòng đôi").DefaultView;

                }
                else
                {
                    LoadLoaiPhongBLL loadphonggiadinh = new LoadLoaiPhongBLL();
                    dtg_PhongGiaDinh.ItemsSource = loadphonggiadinh.ChecKLoadLoaiPhong("Phòng gia đình").DefaultView;
                }
            }
        }
        #endregion

        #region sửa phòng
        private void btn_SuaPhong_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region xóa phòng đơn
        private void btn_XoaPhongDon_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_PhongDon.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_PhongDon.SelectedItem;
                if (dtr != null)
                {
                    var maphong = dtr[0].ToString();
                    var trangthaiphong = dtr[4].ToString();
                    if (trangthaiphong == "Đã thuê") MessageBox.Show("Không thể xóa phòng đang thuê");
                    else
                    {
                        ThemSuaXoaPhongBLL xoaphongdon = new ThemSuaXoaPhongBLL();
                        xoaphongdon.CheckXoaPhong(maphong);
                        LoadLoaiPhongBLL loadphongdon = new LoadLoaiPhongBLL();
                        dtg_PhongDon.ItemsSource = loadphongdon.ChecKLoadLoaiPhong("Phòng đơn").DefaultView;
                    }
                }
            }
        }
        #endregion

        #region xóa phòng đôi
        private void btn_XoaPhongDoi_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_PhongDon.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_PhongDoi.SelectedItem;
                if (dtr != null)
                {
                    var maphong = dtr[0].ToString();
                    var trangthaiphong = dtr[4].ToString();
                    if (trangthaiphong == "Đã thuê") MessageBox.Show("Không thể xóa phòng đang thuê");
                    else
                    {
                        ThemSuaXoaPhongBLL xoaphongdon = new ThemSuaXoaPhongBLL();
                        xoaphongdon.CheckXoaPhong(maphong);
                        LoadLoaiPhongBLL loadphongdoi = new LoadLoaiPhongBLL();
                        dtg_PhongDoi.ItemsSource = loadphongdoi.ChecKLoadLoaiPhong("Phòng đôi").DefaultView;
                    }
                }
            }
        }
        #endregion

        #region xóa phòng gia đình
        private void btn_XoaPhongGiaDinh_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_PhongDon.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_PhongGiaDinh.SelectedItem;
                if (dtr != null)
                {
                    var maphong = dtr[0].ToString();
                    var trangthaiphong = dtr[4].ToString();
                    if (trangthaiphong == "Đã thuê") MessageBox.Show("Không thể xóa phòng đang thuê");
                    else
                    {
                        ThemSuaXoaPhongBLL xoaphongdon = new ThemSuaXoaPhongBLL();
                        xoaphongdon.CheckXoaPhong(maphong);
                        LoadLoaiPhongBLL loadphonggiadinh = new LoadLoaiPhongBLL();
                        dtg_PhongGiaDinh.ItemsSource = loadphonggiadinh.ChecKLoadLoaiPhong("Phòng gia đình").DefaultView;
                    }
                }
            }
        }
        #endregion

        #region nhập vào textbox khi nhấn vào bảng phòng đơn
        private void dtg_Phongdon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtg_PhongDon.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_PhongDon.SelectedItem;
                {
                    if(dtr != null)
                    {
                        txt_TenPhong.Text = dtr[1].ToString();
                        txt_GiaPhong.Text = dtr[3].ToString();
                        cbo_LoaiPhong.SelectedIndex = 0;
                    }
                }
            }
        }
        #endregion

        #region nhập vào textbox khi nhấn vào phòng đôi
        private void dtg_PhongDoi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_PhongDon.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_PhongDoi.SelectedItem;
                {
                    if (dtr != null)
                    {
                        txt_TenPhong.Text = dtr[1].ToString();
                        txt_GiaPhong.Text = dtr[3].ToString();
                        cbo_LoaiPhong.SelectedIndex = 1;
                    }
                }
            }
        }
        #endregion

        #region nhập vào phòng gia đình khi nhấn vào phòng gia đình
        private void dtg_PhongGiaDinh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_PhongDon.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_PhongGiaDinh.SelectedItem;
                {
                    if (dtr != null)
                    {
                        txt_TenPhong.Text = dtr[1].ToString();
                        txt_GiaPhong.Text = dtr[3].ToString();
                        cbo_LoaiPhong.SelectedIndex = 2;
                    }
                }
            }
        }
        #endregion

        
        
    }
}
