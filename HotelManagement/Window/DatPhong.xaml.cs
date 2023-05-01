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
using System.Windows.Shapes;

namespace GUI.Window
{
    /// <summary>
    /// Interaction logic for DatPhong.xaml
    /// </summary>
    public partial class DatPhong : System.Windows.Window
    {
        public DatPhong()
        {
            InitializeComponent();
        }

        private void red_exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void gridHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

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

        private void btn_ThemDichVu(object sender, RoutedEventArgs e)
        {
            ThemDichVu themdichvu = new ThemDichVu();
            themdichvu.ShowDialog();
        }
    }
}
