using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
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
        //dùng để tránh bị trùng
        DanhSachDichVuDaChon danhSachDaChon = new DanhSachDichVuDaChon();
        //chứa dánh sách đã chọn trong list
        List<DanhSachDichVu>  dsdachon = new List<DanhSachDichVu> ();
        public ThemDichVu( ref List<DanhSachDichVu> list)
        {
            InitializeComponent();
            #region load dịch vụ lên danh sách dịch vụ
            LoadDV l = new LoadDV();
            l.add();
            dtg_DichVu.ItemsSource = l.list;
            #endregion

            #region code để filter
            CollectionView filterDichVu = (CollectionView)CollectionViewSource.GetDefaultView(dtg_DichVu.ItemsSource);
            filterDichVu.Filter = DichVuFilter;
            #endregion

            dtg_DichVuDaChon.ItemsSource = danhSachDaChon;
            dtg_DichVuDaChon.CellEditEnding += Dtg_DichVuDaChon_CellEditEnding;
            list = dsdachon;

        }


        #region mỗi khi số lượng thay đổi
        private void Dtg_DichVuDaChon_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Lấy đối tượng hàng được chọn
            var selectedRow = e.Row.Item as DanhSachDichVu;

            // Tính toán lại giá trị Thành tiền
            selectedRow.ThanhTien = selectedRow.SoLuong * selectedRow.GiaTien;

            for(int i =  0;i < dsdachon.Count;i++)
            {
                if (dsdachon[i] == selectedRow)
                {
                    dsdachon[i].ThanhTien = selectedRow.SoLuong * selectedRow.GiaTien;
                    Debug.WriteLine(dsdachon[i].TenDichVu + " " + dsdachon[i].GiaTien + " " + dsdachon[i].SoLuong + " " + dsdachon[i].ThanhTien);
                }
            }
        }
        #endregion

        #region filter dịch vụ
        public bool DichVuFilter(object dv)
        {
            if (String.IsNullOrEmpty(txb_SearchDV.Text))//nếu mà từ khóa search = null or empty thì không làm gì cả
                return true;
            else //ngược lại unbox item ra, chọn thuộc tính cần so sánh, nếu tìm thấy index là true 
                return ((dv as DanhSachDichVu).TenDichVu.IndexOf(txb_SearchDV.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            //Nhưng item return false thì không được hiển thị
        }
        #endregion

        #region thanh kéo
        private void gridHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region nút thoát
        private void red_exit(object sender, MouseButtonEventArgs e)
        {
            dsdachon.RemoveAll(item => item.ThanhTien == 0);
               
            this.Close();
        }
        #endregion

        #region nút thêm dịch vụ
        private void btn_ThemDichVu(object sender, RoutedEventArgs e)
        {
            //chọn dòng từ datagrid A để chuyển sang datagrid B
            var selectedRow = dtg_DichVu.SelectedItem as DanhSachDichVu;
            if (!danhSachDaChon.Contains(selectedRow))
            {
                // Nếu không tồn tại, thêm hàng được chọn vào DataGrid B
                danhSachDaChon.Add(selectedRow);
                dsdachon.Add(selectedRow);
            }

        }
        #endregion

        #region lọc dịch vụ mỗi khi search thay đổi
        private void txb_SearchDV_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dtg_DichVu.ItemsSource).Refresh();
        }
        #endregion


    }

    public class DanhSachDichVuDaChon : ObservableCollection<DanhSachDichVu>
    {

    }
   


}
