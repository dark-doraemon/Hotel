using BLL;
using DTO;
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
    /// Interaction logic for ucQLLoaiDV.xaml
    /// </summary>
    public partial class ucQLLoaiDV : UserControl
    {
        List<BangGiaDichVu> banggia = new List<BangGiaDichVu> ();
        string? madv = "";
        public ucQLLoaiDV()
        {
            InitializeComponent();
            LoadBangGiaDVBLL load = new LoadBangGiaDVBLL();
            load.CheckLoadBangGiaDichVu(ref banggia);

            //dtg_DichVu.ItemsSource = banggia;
            dtg_DichVu.ItemsSource = load.CheckLoadBangGiaDichVu2().DefaultView;
        }
        #region nút thêm
        private void btn_Them_Click(object sender, RoutedEventArgs e)
        {
            string tendv = txt_TenDichVu.Text;
            ThemSuaXoaDichVuBLL them = new ThemSuaXoaDichVuBLL();
            string res = them.CheckThemDichVuBLL(tendv, txt_GiaDichVu.Text);
            if(res == "code_error_giadv")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else if(res == "code_string")
            {
                MessageBox.Show("Dữ liệu không hợp lệ");
            }
            else
            {
                MessageBox.Show("Thêm thành công");
                LoadBangGiaDVBLL load = new LoadBangGiaDVBLL();
                dtg_DichVu.ItemsSource = load.CheckLoadBangGiaDichVu2().DefaultView;

            }
        }
        #endregion

        #region nút sửa
        private void btn_Sua_Click(object sender, RoutedEventArgs e)
        {
            ThemSuaXoaDichVuBLL suadv = new ThemSuaXoaDichVuBLL();
            string res = suadv.CheckSuaDichVuBLL(madv, txt_TenDichVu.Text, txt_GiaDichVu.Text);
            if (res == "code_error_giadv")
            {
                MessageBox.Show("Giá không hợp lệ");
            }
            else if (res == "code_string")
            {
                MessageBox.Show("Dữ liệu không hợp lệ");
            }
            else
            {
                MessageBox.Show("Sửa thành công");
                LoadBangGiaDVBLL load = new LoadBangGiaDVBLL();
                dtg_DichVu.ItemsSource = load.CheckLoadBangGiaDichVu2().DefaultView;

            }
        }
        #endregion

        #region nút xóa
        private void btn_XoaDichVu(object sender, RoutedEventArgs e)
        {
            if (dtg_DichVu.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_DichVu.SelectedItem;
                {
                    if (dtr != null)
                    {
                        
                        var madv = dtr[0].ToString();
                        ThemSuaXoaDichVuBLL xoa = new ThemSuaXoaDichVuBLL();
                        string res = xoa.CheckXoaDichVuBLL(madv);
                        if (res == "code_KhongTheXoa")
                        {
                            MessageBox.Show("Không thể xóa");
                        }
                        else
                        {
                            MessageBox.Show("Xóa thành công");
                            LoadBangGiaDVBLL load = new LoadBangGiaDVBLL();
                            dtg_DichVu.ItemsSource = load.CheckLoadBangGiaDichVu2().DefaultView;
                        }
                    }
                }
            }
        }
        #endregion

        #region hiện thông tin mỗi khi bấm vào 1 hàng
        private void dtg_DichVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_DichVu.SelectedIndex.ToString() != null)
            {
                DataRowView dtr = (DataRowView)dtg_DichVu.SelectedItem;
                {
                    if (dtr != null)
                    {
                        madv = dtr[0].ToString();
                        txt_TenDichVu.Text = dtr[1].ToString();
                        txt_GiaDichVu.Text = dtr[2].ToString();
                    }
                }
            }
        }
        #endregion
    }
}
