using BLL;
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
        public List<DanhSachDichVu> resultDV = new List<DanhSachDichVu>();
        public string g_sophong = "";
        public string g_maphong = "";
        public string g_tienphong = "";
        int i = 0;
        public DatPhong(string maphong, string sophong, string tienphong)
        {
            InitializeComponent();
            g_sophong = sophong;
            g_maphong = maphong;
            g_tienphong = tienphong;
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
            i++;
            //load dịch vụ
            dtg_DatPhongDichVu.ItemsSource = resultDV;

            //Tiền dịch vụ
            int sumdichvu = 0;
            resultDV.ForEach(i => sumdichvu += i.ThanhTien);
            string strtiendv = String.Format("{0:n0}", sumdichvu);
            txb_TienDichVu.Text = strtiendv;

            //tiền phòng
            g_tienphong = g_tienphong.Replace(",", "");
            double tienphong = Convert.ToDouble(g_tienphong);
            int sogngay = Convert.ToInt32(txb_SoNgay.Text);
            txb_TienPhong.Text = string.Format("{0:n0}", (tienphong * sogngay));

            //tổng tiền
            int songay = Convert.ToInt32(txb_SoNgay.Text);
            int tongtien = (Convert.ToInt32(g_tienphong) * sogngay + Convert.ToInt32(sumdichvu));
            string strtongtien = String.Format("{0:n0}", tongtien);
            txb_TongTien.Text = strtongtien;
        }
        #endregion

        #region đặt phòng hoàn tất
        private void btn_HoanTat_Click(object sender, RoutedEventArgs e)
        {
            if (i == 0) MessageBox.Show("Bạn chưa nhấn refresh");
            else
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
                    Debug.WriteLine(resultDV[i].TenDichVu + " " + resultDV[i].SoLuong + " " + resultDV[i].ThanhTien +  " " + resultDV[i].MaGiaDichVu);
                    DichVu dv = new DichVu("",
                                            resultDV[i].TenDichVu,
                                            resultDV[i].SoLuong,
                                            resultDV[i].ThanhTien,
                                            resultDV[i].MaGiaDichVu);

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
                    
                    this.Close();
                }
            }
        }
        #endregion
    }
}
