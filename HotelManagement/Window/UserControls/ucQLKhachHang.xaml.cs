using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace GUI.Window.UserControls
{
    /// <summary>
    /// Interaction logic for ucQLKhachHang.xaml
    /// </summary>
    public partial class ucQLKhachHang : UserControl
    {
        List<ThongTinKhachHangDangKyPhong> khachhang = new List<ThongTinKhachHangDangKyPhong>();
        public ucQLKhachHang()
        {
            InitializeComponent();
            LoadKhachHangBLL load = new LoadKhachHangBLL();

            load.CheckLoadKhachHangBLL(ref khachhang);
            dtg_KhachHang.ItemsSource = khachhang;

            CollectionView filterKhachHang = (CollectionView)CollectionViewSource.GetDefaultView(dtg_KhachHang.ItemsSource);

            filterKhachHang.Filter = KhachHangFilter;
        }

        #region hàm filter khách hàng
        public bool KhachHangFilter(object kh)
        {
            if (String.IsNullOrEmpty(txb_SearchKhachHang.Text))//nếu mà từ khóa search = null or empty thì không làm gì cả
                return true;
            else //ngược lại unbox item ra, chọn thuộc tính cần so sánh, nếu tìm thấy index là true 
                return ((kh as KhachHang).TenKH.IndexOf(txb_SearchKhachHang.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            //Nhưng item return false thì không được hiển thị
        }
        #endregion

        #region mỗi khi search change thì dữ liệu refresh lại
        private void txb_SearchKhachHang_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dtg_KhachHang.ItemsSource).Refresh();
        }



        #endregion

        private void btn_xoaKhachHang(object sender, RoutedEventArgs e)
        {
            //Button btn = (Button)sender;
            //var maKH = btn.DataContext.ToString();
            var btn = sender as Button;
            var info = btn.DataContext as ThongTinKhachHangDangKyPhong;
            

        }

        private void dtg_KhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThongTinKhachHangDangKyPhong? t = dtg_KhachHang.SelectedItem as ThongTinKhachHangDangKyPhong;
            txt_MaKhachHang.Text = t.MaKH.ToString();
            txt_TenKhachHang.Text = t.TenKH.ToString();
            txt_DiaChi.Text = t.DiaChiKH.ToString();
            txt_SDT.Text = t.SDTKhachHang.ToString();
        }
    }
        
}
