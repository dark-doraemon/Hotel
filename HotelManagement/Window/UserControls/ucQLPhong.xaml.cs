using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ucQLPhong.xaml
    /// </summary>
    public partial class ucQLPhong : UserControl
    {

        
        List<Phong> phongdon = new List<Phong>();
        List<Phong> phongdoi = new List<Phong>();
        List<Phong> phonggiadinh = new List<Phong>();
        public ucQLPhong()
        {
            InitializeComponent();

            //Truyền xuống BLL
            LoadRoomBLL load = new LoadRoomBLL();
            load.CheckLoadRoomBLL(ref phongdon,ref phongdoi,ref phonggiadinh);
           
            lsvPhongDon.ItemsSource = phongdon;
            lsvPhongDoi.ItemsSource = phongdoi;
            lsvPhongGiaDinh.ItemsSource = phonggiadinh;

            //CollectionView cung cấp các tính năng như filter, sắp xếp,...
            //CollectionView lây dữ liệu từ CollectionViewSource(<.ItemsSource>)
            CollectionView filterPhongDon = (CollectionView)CollectionViewSource.GetDefaultView(lsvPhongDon.ItemsSource);
            CollectionView filterPhongDoi = (CollectionView)CollectionViewSource.GetDefaultView(lsvPhongDoi.ItemsSource);
            CollectionView filterPhongGiaDinh = (CollectionView)CollectionViewSource.GetDefaultView(lsvPhongGiaDinh.ItemsSource);

            //mực đích sử dụng là filter, Filter bằng
            filterPhongDon.Filter = PhongFilter;
            filterPhongDoi.Filter = PhongFilter;
            filterPhongGiaDinh.Filter = PhongFilter;

        }
        #region Hàm filter
        private bool PhongFilter(object item)//Item là các phần tử trong Nguồn dữ liệu cần filter
        {
            if (String.IsNullOrEmpty(txb_SearchPhong.Text))//nếu mà từ khóa search = null or empty thì không làm gì cả
                return true;
            else //ngược lại unbox item ra, chọn thuộc tính cần so sánh, nếu tìm thấy index là true 
                return ((item as Phong).TenPhong.IndexOf(txb_SearchPhong.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            //Nhưng item return false thì không được hiển thị
        }
        #endregion

        #region filter mỗi khi tìm kiếm
        private void txb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //Mỗi lần Tìm kiếm thay đôi là CollectionSource Refresh() lại 
            CollectionViewSource.GetDefaultView(lsvPhongDon.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lsvPhongDoi.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lsvPhongGiaDinh.ItemsSource).Refresh();
        }
        #endregion


        #region Lọc phòng đơn
        private void chk_PhongDon_Checked(object sender, RoutedEventArgs e)
        {
            chk_TatCaPhong.IsChecked = false;
            spn_PhongDon.Visibility= Visibility.Visible;
            spn_PhongDoi.Visibility = Visibility.Collapsed;
            spn_PhongGiaDinh.Visibility= Visibility.Collapsed;
        }
        #endregion

        #region Lọc phòng đôi
        private void chk_PhongDoi_Checked(object sender, RoutedEventArgs e)
        {
            chk_TatCaPhong.IsChecked = false;
            spn_PhongDoi.Visibility = Visibility.Visible;
            spn_PhongDon.Visibility = Visibility.Collapsed;
            spn_PhongGiaDinh.Visibility = Visibility.Collapsed;

        }
        #endregion

        #region Lọc phòng gia đình
        private void chk_PhongGiaDinh_Checked(object sender, RoutedEventArgs e)
        {
            chk_TatCaPhong.IsChecked = false;
            spn_PhongGiaDinh.Visibility = Visibility.Visible;
            spn_PhongDon.Visibility = Visibility.Collapsed;
            spn_PhongDoi.Visibility = Visibility.Collapsed;


        }
        #endregion

        #region Hiện tất cả phòng
        private void chk_TatCaPhong_Checked(object sender, RoutedEventArgs e)
        {
            
            spn_PhongDon.Visibility = Visibility.Visible;
            spn_PhongDoi.Visibility = Visibility.Visible;
            spn_PhongGiaDinh.Visibility = Visibility.Visible;
            lsvPhongDon.ItemsSource = phongdon;
            lsvPhongDoi.ItemsSource = phongdoi;
            lsvPhongGiaDinh.ItemsSource = phonggiadinh;
            chk_PhongDon.IsChecked = false;
            chk_PhongDoi.IsChecked= false;
            chk_PhongGiaDinh.IsChecked= false;
            chk_DaDonDep.IsChecked = false;
            chk_ChuaDonDep.IsChecked = false;
            chk_TatCaDonDep.IsChecked = false;
        }
        #endregion

        #region lọc phòng trống
        private void chk_PhongTrong_Checked(object sender, RoutedEventArgs e)
        {
            List<Phong> phongdontrong = new List<Phong>();
            List<Phong> phongdoitrong = new List<Phong>();
            List<Phong> phonggiadinhtrong = new List<Phong>();
            phongdontrong = phongdon.Where(p => p.TrangThaiPhong == "Trống").ToList();
            phongdoitrong = phongdoi.Where(p => p.TrangThaiPhong == "Trống").ToList();
            phonggiadinhtrong = phonggiadinh.Where(p => p.TrangThaiPhong == "Trống").ToList();
            lsvPhongDon.ItemsSource = phongdontrong;
            lsvPhongDoi.ItemsSource = phongdoitrong;
            lsvPhongGiaDinh.ItemsSource = phonggiadinhtrong;
        }
        #endregion

        #region lọc phòng đã thuê
        private void chk_PhongDaThue_Checked(object sender, RoutedEventArgs e)
        {
            List<Phong> phongdondathue = new List<Phong>();
            List<Phong> phongdoidathue = new List<Phong>();
            List<Phong> phonggiadinhdathue = new List<Phong>();
            phongdondathue = phongdon.Where(p => p.TrangThaiPhong == "Đã thuê").ToList();
            phongdoidathue = phongdoi.Where(p => p.TrangThaiPhong == "Đã thuê").ToList();
            phonggiadinhdathue = phonggiadinh.Where(p => p.TrangThaiPhong == "Đã thuê").ToList();
            lsvPhongDon.ItemsSource = phongdondathue;
            lsvPhongDoi.ItemsSource = phongdoidathue;
            lsvPhongGiaDinh.ItemsSource = phonggiadinhdathue;
        }
        #endregion

        #region lọc phòng đã dọn
        private void chk_DaDonDep_Checked(object sender, RoutedEventArgs e)
        {
            List<Phong> phongdondadon = new List<Phong>();
            List<Phong> phongdoidadon = new List<Phong>();
            List<Phong> phonggiadinhdadon = new List<Phong>();
            phongdondadon = phongdon.Where(p => p.DonDep == "Đã dọn").ToList();
            phongdoidadon = phongdoi.Where(p => p.DonDep == "Đã dọn").ToList();
            phonggiadinhdadon = phonggiadinh.Where(p => p.DonDep == "Đã dọn").ToList();
            lsvPhongDon.ItemsSource = phongdondadon;
            lsvPhongDoi.ItemsSource = phongdoidadon;
            lsvPhongGiaDinh.ItemsSource = phonggiadinhdadon;
        }

        #endregion

        #region lọc phòng chưa dọn
        private void chk_ChuaDonDep_Checked(object sender, RoutedEventArgs e)
        {
            List<Phong> phongdonchuadon = new List<Phong>();
            List<Phong> phongdoichuadon = new List<Phong>();
            List<Phong> phonggiadinhchuadon = new List<Phong>();
            phongdonchuadon = phongdon.Where(p => p.DonDep == "Chưa dọn").ToList();
            phongdoichuadon = phongdoi.Where(p => p.DonDep == "Chưa dọn").ToList();
            phonggiadinhchuadon = phonggiadinh.Where(p => p.DonDep == "Chưa dọn").ToList();
            lsvPhongDon.ItemsSource = phongdonchuadon;
            lsvPhongDoi.ItemsSource = phongdoichuadon;
            lsvPhongGiaDinh.ItemsSource = phonggiadinhchuadon;
        }
        #endregion

        #region lọc tất cả dọn và chưa chọn
        private void chk_TatCaDonDep__Checked(object sender, RoutedEventArgs e)
        {
            lsvPhongDon.ItemsSource = phongdon;
            lsvPhongDoi.ItemsSource = phongdoi;
            lsvPhongGiaDinh.ItemsSource = phonggiadinh;
        }



        #endregion

        
    }

}
