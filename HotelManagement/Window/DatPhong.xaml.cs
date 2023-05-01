using DTO;
using System;
using System.Collections.Generic;
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

        //chứ kết quả khi thêm dịch vụ
        public List<DanhSachDichVu> result = new List<DanhSachDichVu> ();
        public DatPhong()
        {
            InitializeComponent();
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
            ThemDichVu themdichvu = new ThemDichVu(ref result);
            themdichvu.ShowDialog();
        }
        #endregion

        #region hàm tính tống tiền
        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            //Tiền đặt phòng
            dtg_DatPhongDichVu.ItemsSource = result;

            //Tiền dịch vụ
            int sumdichvu = 0;
            result.ForEach(i => sumdichvu += i.ThanhTien);
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
            this.Close();
        }
    }
}
