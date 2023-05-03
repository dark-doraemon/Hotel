﻿using BLL;
using DTO;
using GUI.Window.UserControls;
using HotelManagement.GUI;
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
using System.Windows.Shapes;

namespace GUI.Window
{
    /// <summary>
    /// Interaction logic for DatPhong.xaml
    /// </summary>
    public partial class DatPhong : System.Windows.Window
    {
        //chứa kết quả khi thêm dịch vụ
        public List<DanhSachDichVu> resultDV = new List<DanhSachDichVu> ();
        public string g_sophong = "";
        public string g_maphong = "";

       
        public DatPhong(string maphong,string sophong)
        {
            InitializeComponent();
            g_sophong= sophong;
            g_maphong= maphong;
            
        }
        #region nút thoát
        private void red_exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region thanh kéo
        private void gridHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region nhập ngày vào
        private void dpk_DateIn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? datein = dpk_DateIn.SelectedDate;
            DateTime? dateout = dpk_DateOut.SelectedDate;
            if (datein != null && dateout != null)
            {
                if (dateout <= datein)
                {
                    MessageBox.Show("Không hợp lệ");
                    datein = datein.Value.AddDays(-1);
                    dpk_DateIn.SelectedDate = datein;
                }
            }
            if (datein != null && dateout != null)
            {
                TimeSpan duration = dateout.Value - datein.Value;
                txb_SoNgay.Text = Math.Round(duration.TotalDays).ToString();
            }
        }
        #endregion

        #region nhập ngày ra
        private void dpk_DateOut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? datein = dpk_DateIn.SelectedDate;
            DateTime? dateout = dpk_DateOut.SelectedDate;
            if (datein != null && dateout != null)
            {
                if (dateout <= datein)
                {
                    MessageBox.Show("Không hợp lệ");
                    dateout = datein.Value.AddDays(1);
                    dpk_DateOut.SelectedDate = dateout;
                }
            }
            if (datein != null && dateout != null)
            {
                TimeSpan duration = dateout.Value - datein.Value;
                txb_SoNgay.Text = Math.Round(duration.TotalDays).ToString();
            }

        }
        #endregion

        #region nút thêm dịch vụ
        private void btn_ThemDichVu(object sender, RoutedEventArgs e)
        {
            ThemDichVu themdichvu = new ThemDichVu(ref resultDV);
            themdichvu.ShowDialog();
        }
        #endregion

        #region hàm tính tống tiền
        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            //Tiền đặt phòng
            dtg_DatPhongDichVu.ItemsSource = resultDV;

            //Tiền dịch vụ
            int sumdichvu = 0;
            resultDV.ForEach(i => sumdichvu += i.ThanhTien);
            string strtiendv = String.Format("{0:n0}", sumdichvu);
            txb_TienDichVu.Text = strtiendv;

            //Tổng tiền
            string tienphong = txb_TienPhong.Text;
            tienphong = tienphong.Replace(",","");

            int tongtien = (Convert.ToInt32(tienphong) + Convert.ToInt32(sumdichvu));
            string strtongtien = String.Format("{0:n0}",tongtien);
            txb_TongTien.Text = strtongtien;
        }
        #endregion

        private void btn_HoanTat_Click(object sender, RoutedEventArgs e)
        {
            DateTime? datein = dpk_DateIn.SelectedDate;
            DateTime? dateout = dpk_DateOut.SelectedDate;
            string tenkhachhang = txt_TenKhachHang.Text;
            string diachi = txt_DiaChi.Text;
            string? gioitinh = "";
            if (cbo_GioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn giới tính");
            }
            else
            {
                gioitinh = ((ComboBoxItem)cbo_GioiTinh.SelectedItem).Content.ToString();
            }
            string sdt = txt_SDT.Text;
            string manv = MainUI.manvgui;
            string sophong = g_sophong;
            string maphong = g_maphong;

            ChiTietDatPhong chitiet = new ChiTietDatPhong("", maphong, "", sophong, datein, dateout, manv);
            KhachHang Kh = new KhachHang("", tenkhachhang, diachi, gioitinh, sdt);

            //Truyền dữ liệu xuống BLL để check rồi từ BLL xuống DAL để thêm vào CSDL
            KhachDangKiPhongBLL dangkiphongBLL = new KhachDangKiPhongBLL();

            List<DichVu> LayDuLieuDV = new List<DichVu>();
            for (int i = 0; i < resultDV.Count; i++)
            {
                DichVu dv = new DichVu("",
                                        resultDV[i].TenDichVu,
                                        resultDV[i].SoLuong, 
                                        resultDV[i].ThanhTien);

                LayDuLieuDV.Add(dv);
            }
            string result = dangkiphongBLL.CheckKhachDangKiPhongBLL(chitiet, Kh, LayDuLieuDV);

            if (result == "code_date_in") MessageBox.Show("Date in không được để trống");
            else if (result == "code_date_out") MessageBox.Show("Date out không được để trống");
            else if (result == "code_ten_KH") MessageBox.Show("Tên khách hàng không được để trống");
            else if (result == "code_diachi_KH") MessageBox.Show("Địa chỉ KH không được để trống");
            else if (result == "code_SDT_KH") MessageBox.Show("SĐT KH không được để trống");
            else if (result == "code_date_in") MessageBox.Show("Date in không được để trống");
            else
            {
                ObservableCollection<Phong> phongdon = new ObservableCollection<Phong>();
                ObservableCollection<Phong> phongdoi = new ObservableCollection<Phong>();
                ObservableCollection<Phong> phonggiadinh = new ObservableCollection<Phong>();

                LoadRoomBLL load = new LoadRoomBLL();
                load.CheckLoadRoomBLL(ref phongdon,ref  phongdoi,ref phonggiadinh);
                this.Close();
            }




        }
    }
}
