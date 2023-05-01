using BLL;
using DTO;
using System.Windows;

namespace HotelManagement.GUI
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void btn_TaoTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            string tenhienthi = txt_TenHienThi_ttk.Text;
            string sdt = txt_SDT_ttk.Text;
            string tendangnhap = txt_TenDangNhap_ttk.Text;
            string matkhau = txt_MatKhau_ttk.Text;
            string xacnhanmk = txt_XacNhanMatKhau_ttk.Text;

            NhanVien nv = new NhanVien();
            nv.TenNhanVien = tenhienthi;
            nv.SDTNhanVien = sdt;
            nv.MaNV = null;

            TaiKhoan tk = new TaiKhoan();
            tk.username = tendangnhap;
            tk.password = matkhau;
            tk.confirm = xacnhanmk;

            //đưa xuống BLL để kiểm tra lỗi nghiệp vụ
            CreateAccountBLL checkBLL = new CreateAccountBLL();

            string result = checkBLL.CheckCreateAccount(nv, tk);

            if (result == "code_ten_hien_thi") MessageBox.Show("Tên không được để trống");
            else if (result == "code_phone_number") MessageBox.Show("Số điện thoại không đúng định dạng");
            else if (result == "code_user_name") MessageBox.Show("Tên đăng nhập không được có khoảng trắng");
            else if (result == "code_password") MessageBox.Show("Mật khẩu không giống");
            else if (result == "code_create_account_fail") MessageBox.Show("Tên đăng nhập đã tồn tại");
            else if (result == "success") MessageBox.Show("Thành công");
        }
    }
}
