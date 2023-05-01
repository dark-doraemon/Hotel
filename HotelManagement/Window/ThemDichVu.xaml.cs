using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
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
    /// Interaction logic for ThemDichVu.xaml
    /// </summary>
    public partial class ThemDichVu : System.Windows.Window
    {
        public ThemDichVu()
        {
            InitializeComponent();
            LoadDV l = new LoadDV();
            l.add();
            dtg_DichVu.ItemsSource = l.list;
            CollectionView filterDichVu = (CollectionView)CollectionViewSource.GetDefaultView(dtg_DichVu.ItemsSource);
            filterDichVu.Filter = DichVuFilter;
        }

        public bool DichVuFilter(object dv)
        {
            if (String.IsNullOrEmpty(txb_SearchDV.Text))//nếu mà từ khóa search = null or empty thì không làm gì cả
                return true;
            else //ngược lại unbox item ra, chọn thuộc tính cần so sánh, nếu tìm thấy index là true 
                return ((dv as DanhSachDichVu).TenDichVu.IndexOf(txb_SearchDV.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            //Nhưng item return false thì không được hiển thị
        }


        private void gridHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void red_exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_ThemDichVu(object sender, RoutedEventArgs e)
        {
            
        }

        private void txb_SearchDV_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dtg_DichVu.ItemsSource).Refresh();
        }
    }
}
