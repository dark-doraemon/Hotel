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
    /// Interaction logic for ucQLLoaiDV.xaml
    /// </summary>
    public partial class ucQLLoaiDV : UserControl
    {
        List<BangGiaDichVu> banggia = new List<BangGiaDichVu> ();
        public ucQLLoaiDV()
        {
            InitializeComponent();
            LoadBangGiaDVBLL load = new LoadBangGiaDVBLL();
            load.CheckLoadBangGiaDichVu(ref banggia);

            dtg_DichVu.ItemsSource = banggia;
        }
    }
}
