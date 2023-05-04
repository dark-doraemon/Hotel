using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using GUI.Window.UserControls;
using System.Windows.Controls;
using System.Diagnostics;
using System.Threading;

namespace HotelManagement.GUI
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        //khai báo vài biến để xử lí đường dãn của hình ảnh 
        #region các biến để lưu hình ảnh
        string[] folder = Directory.GetCurrentDirectory().Split('\\');//lấy đường dẫn hiện tại của của chương trình 
        string path = "";//dường dẫn của thư mục chứa ảnh 
        int i = 1;//index của hình ảnh
        List<string> hotelIMG = new List<string>(); //List lưu hình ảnh
        #endregion

        #region hàm lấy background
        public void GetBackgoundImg()
        {
            //xử lý lấy đường dẫn của nơi lưu ảnh
            for (int i = 0; i < folder.Length - 3; i++)
            {
                path += folder[i] + "\\";
            }
            path += "Asset";

            //lưu trong List<string>
            string[] IMG = Directory.GetFiles(path, "*.jpg");
            for (int i = 0; i < IMG.Length; i++)
            {
                hotelIMG.Add(IMG[i]);
            }

            //khi màn hinh window hiện lên thì bắt đầu sự kiện thay đổi background
            DoubleAnimation animation1 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(5)
            };
            TranferBackground.BeginAnimation(Brush.OpacityProperty, animation1);
            TranferBackground.ImageSource = new BitmapImage(new Uri(hotelIMG[i++]));
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        #endregion

        #region Timer lấy hình ảnh thay đổi background
        private void Timer_Tick(object? sender, EventArgs e)
        {
            Thread.Sleep(1);
            //TranferBackground là background của border
            DoubleAnimation animation1 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(5)
            };

            TranferBackground.BeginAnimation(Brush.OpacityProperty, animation1);

            TranferBackground.ImageSource = new BitmapImage(new Uri(hotelIMG[i++]));
            if (i >= 5) i = 1;

        }
        #endregion
        public static string manvgui = "";
        public MainUI(string manv)
        {
            InitializeComponent();
            manvgui=manv;
            GetBackgoundImg();
        }

        #region xử lý đăng xuất
        private void btn_sign_out_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        #endregion

        #region hàm ẩn stackpannel
        private void hideStackPannel()
        {
            DoubleAnimation animation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(100)));
            stackpn_ChucNang.BeginAnimation(WidthProperty, animation);
        }
        #endregion

        #region hàm show stackpannel
        private void showStackPannel()
        {
            DoubleAnimation animation = new DoubleAnimation(200, new Duration(TimeSpan.FromMilliseconds(100)));
            stackpn_ChucNang.BeginAnimation(WidthProperty, animation);
        }
        #endregion

        //đổi màu chữ nút khi rê chuột vào nút đăng xuất và nút quay lại(không quan trọng)
        #region đổi màu khi rê chuột vào button
        private void btn_signout_enter(object sender, MouseEventArgs e)
        {
            btn_sign_out.Foreground = Brushes.Black;
        }
        private void btn_signout_leave(object sender, MouseEventArgs e)
        {
            btn_sign_out.Foreground = Brushes.White;
        }
        private void btn_back_enter(object sender, MouseEventArgs e)
        {
            btn_back.Foreground = Brushes.Black;
        }
        private void btn_back_leave(object sender, MouseEventArgs e)
        {
            btn_back.Foreground = Brushes.White;
        }
        #endregion

        #region nút quay lại
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            hideStackPannel();
        }
        #endregion

        #region nút menu nhấn vào nút menu thì hiện ra stackpannel
        private void btn_menu_click(object sender, RoutedEventArgs e)
        {
            //animation cho menu
            //cái dặt aniamtion có value là 200 set cho thuộc tính width của stackpn_chucnang trong thời gian là 100ms
            //DoubleAnimation sẽ thì thực hiện animation cho width tới khi width = 200 trong thời gian là 100ms
            //vì là Double kiểu số thực nên khi animation sẽ rất mượt
            showStackPannel();
        }
        #endregion

        #region thanh kéo window (dùng để kéo cửa sổ)
        private void gridHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region nút đóng ứng dụng
        private void red_exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region nút thu nhỏ ứng dụng
        private void yellow_mininize(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region nút trang chủ
        private void btn_TrangChu_Click(object sender, RoutedEventArgs e)
        {
            hideStackPannel();
            contentControl.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region nút quản lý phòng   
        private void btn_QLPhong_Click(object sender, RoutedEventArgs e)
        {
            #region đàu tiên là hiện lên giao diện quản lý phòng
            hideStackPannel();
            contentControl.Visibility = Visibility.Visible;
            Button clickedButton = (Button)sender;
            WhiteBar.Content = clickedButton.Content;
            ucQLPhong qlPhong = new ucQLPhong();
            contentControl.Content = qlPhong;
            #endregion

           
        }
        #endregion

        #region nút quản lý khách hàng
        private void btn_QLKhachHang_Click(object sender, RoutedEventArgs e)
        {
            hideStackPannel();
            contentControl.Visibility = Visibility.Visible;
            Button clickedButton = (Button)sender;
            WhiteBar.Content = clickedButton.Content;
            ucQLKhachHang qlKhachHang = new ucQLKhachHang();
            contentControl.Content = qlKhachHang;
        }


        #endregion

        #region quản lý loại phòng
        private void btn_QLLoaiPhong_Click(object sender, RoutedEventArgs e)
        {
            hideStackPannel();
            contentControl.Visibility = Visibility.Visible;
            Button clickedButton = (Button)sender;
            WhiteBar.Content = clickedButton.Content;
            ucQLLoaiPHong qloaiphong = new ucQLLoaiPHong();
            contentControl.Content = qloaiphong;
        }


        #endregion

        #region quản lý loại dịch vụ
        private void btn_QLDichVu_Click(object sender, RoutedEventArgs e)
        {
            hideStackPannel();
            contentControl.Visibility = Visibility.Visible;
            Button clickedButton = (Button)sender;
            WhiteBar.Content = clickedButton.Content;
            ucQLLoaiDV qlloaidv = new ucQLLoaiDV();
            contentControl.Content = qlloaidv;
        }
        #endregion
    }
}
