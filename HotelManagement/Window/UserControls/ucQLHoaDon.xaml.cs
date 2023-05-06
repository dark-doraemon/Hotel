using BLL;
using DTO;
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
    /// Interaction logic for ucQLHoaDon.xaml
    /// </summary>
    public partial class ucQLHoaDon : UserControl
    {
        public ucQLHoaDon()
        {
            InitializeComponent();
            LoadHoaDonBLL load = new LoadHoaDonBLL();
            dtg_HoaDon.ItemsSource = load.CheckLoadHoaDon();

            CollectionView filterHoaDon = (CollectionView)CollectionViewSource.GetDefaultView(dtg_HoaDon.ItemsSource);
            filterHoaDon.Filter = HoaDonFilter;
        }

        #region hàm filter
        private bool HoaDonFilter(object hoadon)
        {
            if (String.IsNullOrEmpty(txb_SearchHoaDon.Text))//nếu mà từ khóa search = null or empty thì không làm gì cả
                return true;
            else //ngược lại unbox item ra, chọn thuộc tính cần so sánh, nếu tìm thấy index là true 
                return ((hoadon as DTO.HoaDon).TenKH.IndexOf(txb_SearchHoaDon.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            //Nhưng item return false thì không được hiển thị

        }
        #endregion

        #region filter mỗi khi nhập tìm kiếm
        private void txb_SearchKhachHang_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dtg_HoaDon.ItemsSource).Refresh();
        }
        #endregion

        private void btn_XoaHoaDon(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var info = btn.DataContext as DTO.HoaDon;
            XoaHoaDonBLL xoa = new XoaHoaDonBLL();
            xoa.CheckXoaHoaDon(info.MaHoaDon);

            dtg_HoaDon.ItemsSource = null;
            LoadHoaDonBLL load = new LoadHoaDonBLL();
            dtg_HoaDon.ItemsSource = load.CheckLoadHoaDon();
        }
    }
}
