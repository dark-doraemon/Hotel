using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BLL;
using DTO;

namespace HotelManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TaiKhoan tk = new TaiKhoan();//class tài khoản lấy từ GUI
        TaiKhoanBLL tkBLL = new TaiKhoanBLL();//từ tài khoản GUI chuyển vào tài khoản BLL
        public MainWindow()
        {
            InitializeComponent();
            
        }

        #region hàm đổi màu khi rê chuột vào button(không quan trọng)
        //để chuột vào thì đổi màu
        private void hoverEnter(object sender, MouseEventArgs e)
        {
            btn_TaoTaiKhoan.Foreground = Brushes.Black;
        }
        //kéo chuột ra thì màu reset
        private void hoverOut(object sender, MouseEventArgs e)
        {
            btn_TaoTaiKhoan.Background = null;
            btn_TaoTaiKhoan.Foreground = Brushes.Wheat;

        }
        #endregion

        #region xử lý đăng nhập
        private void btn_DangNhap_Click(object sender, RoutedEventArgs e) 
        {
            //kiểm tra tài khoản 
            tk.username = txt_TaiKhoan.Text;
            tk.password = txt_MatKhau.Password.ToString();


            //từ tk của GUI chuyển cho BLL, từ BLL chuyển cho DAL
            string getUser = tkBLL.CheckLoginBLL(tk);

            if (getUser == "error_user")
            {
                MessageBox.Show("Tài khoản không được để trống");
                return;
            }
            else if (getUser == "error_pass")
            {
                MessageBox.Show("Mật khẩu không được để trống");
                return;
            }
            else if (getUser == "error_user_or_pass")
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu sai");
                return;
            }
            else MessageBox.Show(getUser);


            //nếu đúng
            GUI.MainUI mainUI = new GUI.MainUI(getUser.Split('|')[1]);
            mainUI.lbl_TenHienThi.Content = getUser.Split('|')[0];
            mainUI.Show();//Màn hình gọi nó vẫn sử đụng được
            //mainUI.ShowDialog();//màn hình gọi nó không sử đụng được

            //của sổ hiện tại ẩn đi
            this.Close();
        }
        #endregion

        #region mở form tạo tài khoản
        private void btn_TaoTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            GUI.CreateAccount create = new GUI.CreateAccount();
            create.ShowDialog();
        }
        #endregion

        #region hàm show và hide password
        //show password
        private void chk_showpass_Checked(object sender, RoutedEventArgs e)
        {
            txt_ShowMatKhau.Text = txt_MatKhau.Password.ToString(); 
            txt_ShowMatKhau.Visibility= Visibility.Visible;
            txt_MatKhau.Visibility = Visibility.Collapsed;
        }

        //hide password
        private void chk_showpass_UnChecked(object sender, RoutedEventArgs e)
        {
            txt_ShowMatKhau.Visibility = Visibility.Collapsed;
            txt_MatKhau.Visibility = Visibility.Visible;
        }
        
        //nếu mà nhập mật khẩu lúc show password thì passwordbox cũng thay đổi theo
        private void txt_ShowMatKhau_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_MatKhau.Password = txt_ShowMatKhau.Text;
        }
        #endregion

        #region thang ngang dùng để di chuyển window
        private void gridHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region Hàm dùng để đóng ứng dụng(red button)
        private void red_exit(object sender, MouseButtonEventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region hàm dùng để thu nhỏ ứng dụng(yellow button)
        private void yellow_mininize(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

    }
}
