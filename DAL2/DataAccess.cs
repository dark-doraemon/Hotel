using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
using System.Diagnostics;

namespace DAL
{
    #region class kết nối tới CSDL (để tái sử dụng) SqlConnectionData
    public class SqlConnectionData
    {
        //Tạo hàm kết nối tới CSDL để tái sử dụng
        public static SqlConnection Connect()
        {
            string strConn = @"Data Source=DESKTOP-49HU124;Initial Catalog=DB_QuanLyKhachSan;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(strConn);
            return sqlConn;
        }
    }
    #endregion


    //Class kiểm tra trong Database (bước cuối cùng)
    public class DataAccess
    {
        #region kiểm tra login
        public static string CheckLoginFinalDAL(TaiKhoan tk)
        {
            string user = null; // kết quả sao khi thực hiện xong trả vền tên của nhân viên

            SqlConnection sqlConn = SqlConnectionData.Connect();
            if(sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            Debug.WriteLine(tk.username + tk.confirm+ tk.password);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConn;//cmd kết nối thông qua sqlConn
            cmd.CommandType = CommandType.StoredProcedure;//kiểu truy vấn là Store Procedure
            cmd.CommandText = "proc_login";//lấy dữ liệu trong pro_login
            /*create procedure proc_login
            @user varchar(20),
            @pass varchar(20)
            as
            begin
	            select NV.TenNhanVien
	            from Tai_Khoan TK join Nhan_Vien NV on TK.MaNV = NV.MaNV
	            where TK.username = @user and TK.password = @pass

            end*/

            //"proc_login" : cứ hiểu làm hàm trong CSDL @user, @pass là các tham số
            cmd.Parameters.AddWithValue("@user", tk.username);
            cmd.Parameters.AddWithValue("@pass", tk.password);
         
            //đọc dữ liệu từ kết quả trả về
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)//nếu có kết quả trong record
            {
                while( reader.Read())
                {
                    user = reader.GetString(0);
                    return user;
                }
                reader.Close();
                sqlConn.Close();
            }
            else
            {
                return "error_user_or_pass";
            }

            //nếu return ở đây là trả về null(mà cũng không bao giờ return ở đây)
            return user;
        }
        #endregion

        #region kiềm tra tạo tài khoản
        public static string CheckCreateAccountFinalDAL(NhanVien nv, TaiKhoan tk)
        {
            //kết nối tới CSDL
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            else { sqlConn.Close(); }

            SqlCommand getlastMaNV = new SqlCommand();
            getlastMaNV.Connection = sqlConn;
            getlastMaNV.CommandType = CommandType.Text;
            getlastMaNV.CommandText = "select dbo.getLastIndexOfMaNV()";
            /*create function getLastIndexOfMaNV()
            returns int
            as 
            begin 
	            declare @lastvalue int
	            set @lastvalue = (select Max(Cast(right(MaNV,1) as int)) from Nhan_Vien)
	            if @lastvalue is null set @lastvalue = 0 
	            return @lastvalue
            end*/


            //nhận kết quả index cuối của trong database nv để tạo id 
            int last = (int)getlastMaNV.ExecuteScalar();
            last += 1;
            string MaNV = last.ToString();
            while (MaNV.Length < 4)
            {
                MaNV = "0" + MaNV;
            }
            MaNV = "NV" + MaNV;


            //Đầu tiên là insert Nhân viên trước
            SqlCommand insertNhanVien = new SqlCommand("insertNhanVien", sqlConn);
            insertNhanVien.CommandType = CommandType.StoredProcedure;

            insertNhanVien.Parameters.AddWithValue("@manv", MaNV);
            insertNhanVien.Parameters.AddWithValue("@tennv", nv.TenNhanVien);
            insertNhanVien.Parameters.AddWithValue("@sdt", nv.SDTNhanVien);


            //để insert dữ liệu dùng ExecuteNonQuery(kết quả trả về là số nguyên không âm)
            //là số hàng bị ảnh hưởng bởi truy vấn SQL được thực thi.
            //trả về 0 nếu không có hàng nào bị ảnh hưởng
            insertNhanVien.ExecuteNonQuery();
            

            //Tiếp theo insert tài khoản
            SqlCommand insertTaiKhoan = new SqlCommand("insertTaiKhoan", sqlConn);
            insertTaiKhoan.CommandType = CommandType.StoredProcedure;

            insertTaiKhoan.Parameters.AddWithValue("@username", tk.username);
            insertTaiKhoan.Parameters.AddWithValue("@password",tk.password);
            insertTaiKhoan.Parameters.AddWithValue("@manv", MaNV);

            //có thể xây ra lỗi tên đăng nhập trùng
            try
            {
                insertTaiKhoan.ExecuteNonQuery();
            }
            catch (Exception ) 
            {
                SqlCommand deleteMaNV = new SqlCommand("deleteMaNV", sqlConn);
                deleteMaNV.CommandType = CommandType.StoredProcedure;

                deleteMaNV.Parameters.AddWithValue("@manv", MaNV);
                
                deleteMaNV.ExecuteNonQuery();

                return "code_create_account_fail";
            }

            return "success";
        }
        #endregion

        #region load dữ liệu phòng

        public static void LoadRoomToList(ref List<Phong> phongdon, ref List<Phong> phongdoi, ref List<Phong> phonggiadinh)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if(sqlConn.State == ConnectionState.Open) { sqlConn.Close(); }
            else { sqlConn.Open(); }

            SqlCommand loadPhong = new SqlCommand();
            loadPhong.CommandType = CommandType.Text;
            loadPhong.CommandText = "select * from Phong";
            loadPhong.Connection= sqlConn;

            SqlDataReader reader = loadPhong.ExecuteReader();
            while(reader.Read())
            {
                string maphong = reader.GetString(0);
                string tenphong = reader.GetString(1);
                string loaiphong = reader.GetString(2);
                Double giaphong = reader.GetDouble(3);
                string trangthaiphong = reader.GetString(4);
                string dondep = reader.GetString(5);
                
                Phong p = new Phong(maphong,tenphong,loaiphong,giaphong,trangthaiphong,dondep);
                if (p.LoaiPhong == "Phòng đơn") phongdon.Add(p);
                else if (p.LoaiPhong == "Phòng đôi") phongdoi.Add(p);
                else phonggiadinh.Add(p);

            }
            reader.Close();
        }
        #endregion
        public static void LoadKhachHangToList(ref List<KhachHang> khachhang)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == ConnectionState.Open) { sqlConn.Close(); }
            else { sqlConn.Open(); }

            SqlCommand loadKH = new SqlCommand();
            loadKH.CommandType = CommandType.Text;
            loadKH.CommandText = "select * from Khach_Hang";
            loadKH.Connection = sqlConn;

            SqlDataReader reader = loadKH.ExecuteReader();  
            while(reader.Read())
            {
                string makh = reader.GetString(0);
                string tenkh = reader.GetString(1);
                string diachi = reader.GetString(2);
                string gioitinh = reader.GetString(3);
                string sdtkhachhang = reader.GetString(4);
                khachhang.Add(new KhachHang(makh,tenkh,diachi,gioitinh,sdtkhachhang));
            }
            reader.Close();

        }
    }
}
