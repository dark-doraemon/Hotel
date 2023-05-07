using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace DAL
{
    #region class kết nối tới CSDL (để tái sử dụng) SqlConnectionData
    public class SqlConnectionData
    {
        //Tạo hàm kết nối tới CSDL để tái sử dụng
        public static SqlConnection Connect()
        {
            string pcname = System.Environment.MachineName;
            string strConn = @"Data Source=" + pcname + ";Initial Catalog=DB_QuanLyKhachSan;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(strConn);
            return sqlConn;
        }
    }
    #endregion

    #region lấy index cuối
    public class GetLastIndex
    {
        public static string MakeCode(SqlCommand lastindex,string MaCanTao)
        {
            string Ma = "";
            
                int last = (int)lastindex.ExecuteScalar();
                last += 1;
                Ma = last.ToString();
                while (Ma.Length < 4)
                {
                    Ma = "0" + Ma;
                }
                Ma = MaCanTao + Ma;
                return Ma;
           
        }
    }
    #endregion

    //Class kiểm tra trong Database (bước cuối cùng)
    public class DataAccess
    {
        #region kiểm tra login
        public static string CheckLoginFinalDAL(TaiKhoan tk)
        {
            string user = null; // kết quả sao khi thực hiện xong trả về tên của nhân viên

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
	            select NV.TenNhanVien,NV.MaNV
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
                    user = reader.GetString(0) + "|" +  reader.GetString(1);
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

            string MaNV = GetLastIndex.MakeCode(getlastMaNV, "NV");
            


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
            loadPhong.CommandText = "select P.MaPhong,TenPhong,P.LoaiPhong,P.GiaPhong,P.TrangThaiPhong,P.DonDep,KH.TenKH\r\nfrom Phong P left join Chi_Tiet_Dat_Phong CTDP on P.MaPhong = CTDP.MaPhong\r\n\t\t\tleft join Khach_Hang KH on KH.MaKH = CTDP.MaKH";
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
                string tenkh;
                if(reader.IsDBNull(reader.GetOrdinal("TenKH")))
                {
                    tenkh = "";
                }
                else tenkh = reader.GetString(6);

                Phong p = new Phong(maphong,tenphong,loaiphong,giaphong,trangthaiphong,dondep,tenkh);
                if (p.LoaiPhong == "Phòng đơn") phongdon.Add(p);
                else if (p.LoaiPhong == "Phòng đôi") phongdoi.Add(p);
                else phonggiadinh.Add(p);

            }
            reader.Close();
        }
        #endregion

        #region insertKhachHang
        public static void  insertKhachHang( ref KhachHang Kh)
        {

            //tạo kết nối tới CSDL
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == ConnectionState.Open) { sqlConn.Close(); }
            else { sqlConn.Open(); }
            
            //Lấy index cuôi của mã kh để tạo mã KH
            SqlCommand getlastindexmakh = new SqlCommand();
            getlastindexmakh.Connection = sqlConn;
            getlastindexmakh.CommandType = CommandType.Text;
            getlastindexmakh.CommandText = "select dbo.getLastIndexOfMaKH()";
            string MaKH = GetLastIndex.MakeCode(getlastindexmakh,"KH");
            Kh.MaKH = MaKH;


            //insert Khách hàng vào trong CSDL
            SqlCommand insertKhachHang = new SqlCommand();
            insertKhachHang.CommandType = CommandType.StoredProcedure;
            insertKhachHang.Connection = sqlConn;
            insertKhachHang.CommandText = "proc_insertKhachHang";

            insertKhachHang.Parameters.AddWithValue("@makh", Kh.MaKH);
            insertKhachHang.Parameters.AddWithValue("@tenkh", Kh.TenKH);
            insertKhachHang.Parameters.AddWithValue("@diachi", Kh.DiaChi);
            insertKhachHang.Parameters.AddWithValue("@gioitinh", Kh.GioiTinh);
            insertKhachHang.Parameters.AddWithValue("@sdtkhachhang", Kh.SDTKhachHang);

            insertKhachHang.ExecuteNonQuery();

        }

        #endregion

        #region load khách hàng
        public static List<ThongTinKhachHangDangKyPhong> LoadKhachHangToList()
        {
            List<ThongTinKhachHangDangKyPhong> khachhang = new List<ThongTinKhachHangDangKyPhong> ();

            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == ConnectionState.Open) { sqlConn.Close(); }
            else { sqlConn.Open(); }

            SqlCommand loadKH = new SqlCommand();
            loadKH.CommandType = CommandType.StoredProcedure;
            loadKH.CommandText = "proc_ThongTinKhachHangDangKyPhong";
            loadKH.Connection = sqlConn;

            SqlDataReader reader = loadKH.ExecuteReader();  
            while(reader.Read())
            {
                string makh = reader.GetString(0);
                string tenkh = reader.GetString(1);
                string diachi = reader.GetString(2);
                string gioitinh = reader.GetString(3);
                string sdtkhachhang = reader.GetString(4);
                string datein = reader.GetDateTime(5).ToString("dd/MM/yyyy");
                string deteout = reader.GetDateTime(6).ToString("dd/MM/yyyy");
                string tenphong = reader.GetString(7);
                khachhang.Add(new ThongTinKhachHangDangKyPhong(makh,tenkh,diachi,gioitinh,sdtkhachhang,datein, deteout, tenphong));
            }
            reader.Close();
            return khachhang;
        }
        #endregion

        #region load khách hàng 2
        public static DataTable LoadKhachHangToList2()
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            DataTable table = new DataTable();
            SqlCommand loadKH = new SqlCommand();
            loadKH.CommandType = CommandType.StoredProcedure;
            loadKH.CommandText = "proc_ThongTinKhachHangDangKyPhong";
            loadKH.Connection = sqlConn;
            SqlDataReader reader = loadKH.ExecuteReader();
            table.Load(reader);
            sqlConn.Close();
            return table;
        }
        #endregion

        #region chi tiet dat phong
        public static string insertChiTietDatPhong(ChiTietDatPhong chiTietDatPhong,KhachHang KH,List<DichVu> dichvu)
        {
            #region muốn insert chi tiết đặt phòng đầu tiền ta phải insert khách hàng
            insertKhachHang(ref KH);
            #endregion

            #region sau đó insert Dich Vụ
            insertDichVu(ref dichvu);
            #endregion

            #region tiếp theo insert vào Chi tiết dặt phòng
            //tạo kết nối tới CSDL
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }

            //lấy index cuối của bảng chi tiết đặt phòng
            SqlCommand getlastindexchitietdatphong = new SqlCommand();
            getlastindexchitietdatphong.Connection = sqlConn;
            getlastindexchitietdatphong.CommandType = CommandType.Text;
            getlastindexchitietdatphong.CommandText = "select dbo.getLastIndexOfMaChiTietDatPhong()";
            string MaDatPhong = GetLastIndex.MakeCode(getlastindexchitietdatphong, "MDP");
            chiTietDatPhong.MaDatPhong = MaDatPhong;

            //insert vào chi tiết đặt phòng
            SqlCommand insertChiTietDatPhong = new SqlCommand();
            insertChiTietDatPhong.CommandType = CommandType.StoredProcedure;
            insertChiTietDatPhong.Connection = sqlConn;
            insertChiTietDatPhong.CommandText = "proc_insertChiTietDatPHong";

            insertChiTietDatPhong.Parameters.AddWithValue("@madatphong", chiTietDatPhong.MaDatPhong);
            insertChiTietDatPhong.Parameters.AddWithValue("@maphong", chiTietDatPhong.MaPhong);
            insertChiTietDatPhong.Parameters.AddWithValue("@makh",KH.MaKH);
            insertChiTietDatPhong.Parameters.AddWithValue("@sophong", chiTietDatPhong.SoPhong);
            insertChiTietDatPhong.Parameters.AddWithValue("@datein", chiTietDatPhong.DateIn);
            insertChiTietDatPhong.Parameters.AddWithValue("@dateout", chiTietDatPhong.DateOut);
            insertChiTietDatPhong.Parameters.AddWithValue("@manv", chiTietDatPhong.MaNV);
            insertChiTietDatPhong.ExecuteNonQuery();
            #endregion

            #region chuyển trạng thái phòng
            //sao khi đặt phòng xong thì trạng thái phòng chuyển sang trạng thái đã đặt
            SqlCommand updateTrangThaiPhong = new SqlCommand();
            updateTrangThaiPhong.CommandType = CommandType.Text;
            updateTrangThaiPhong.CommandText = "update Phong set TrangThaiPhong = N'Đã thuê' where MaPhong = '"+ chiTietDatPhong.MaPhong + "' ";
            updateTrangThaiPhong.Connection = sqlConn;
            updateTrangThaiPhong.ExecuteNonQuery();
            #endregion

            #region insert đặt phòng dịch vụ
            //Cuối cùng insert vào bảng đặt phòng dịch vụ
            //do 1 phòng có nhiều dịch vụ 

            insertDatPhongDichVu(MaDatPhong, dichvu);
            
            return "success";
            #endregion
        }
        #endregion

        #region insert DichVu
        public static void insertDichVu(ref List<DichVu> dv)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }
            
            foreach(var d in dv)
            {
                //lấy index cuối của mã dịch vụ để làm mã dv
                using (SqlCommand getlastindexmadv = new SqlCommand())
                {
                    getlastindexmadv.Connection = sqlConn;
                    getlastindexmadv.CommandType = CommandType.Text;
                    getlastindexmadv.CommandText = "select dbo.getLastIndexOfMaDichVu()";
                    string MaDV = GetLastIndex.MakeCode(getlastindexmadv, "DV");
                    d.MaDichVu = MaDV;
                }

                using (SqlCommand insertDV = new SqlCommand())
                {
                    insertDV.CommandType = CommandType.StoredProcedure;
                    insertDV.Connection = sqlConn;
                    insertDV.CommandText = "proc_insertDichVu";

                    insertDV.Parameters.AddWithValue("@madichvu", d.MaDichVu);
                    insertDV.Parameters.AddWithValue("@tendichvu", d.TenDichVu);
                    insertDV.Parameters.AddWithValue("@soluong", d.SoLuong);
                    insertDV.Parameters.AddWithValue("@giadichvu", d.GiaDichVu);
                    insertDV.Parameters.AddWithValue("@magiadichvu", d.MaGiaDichVu);

                    insertDV.ExecuteNonQuery();

                }
            }

        }
        #endregion

        #region insert DatPhongDichVu
        public static void insertDatPhongDichVu(string maDatPhong, List<DichVu> dv)
        {
            //tạo kết nối
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == ConnectionState.Closed) { sqlConn.Open(); }

            foreach(var d in dv)
            {
                using (SqlCommand insertDatPhongDichVu = new SqlCommand())
                {
                    insertDatPhongDichVu.CommandType = CommandType.StoredProcedure;
                    insertDatPhongDichVu.Connection = sqlConn;
                    insertDatPhongDichVu.CommandText = "proc_DatPhongDichVu";
                    insertDatPhongDichVu.Parameters.AddWithValue("@madatphong", maDatPhong);
                    insertDatPhongDichVu.Parameters.AddWithValue("@madichvu", d.MaDichVu);
                    insertDatPhongDichVu.ExecuteNonQuery();
                }
            }

        }
        #endregion

        #region load thông tin của phong (tên khách hàng,dịch vụ đã dặt,....)

        public static void LoadThongTinPhongToList(ref List<ThongTinCuaPhong> Thongtin,string maphong)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand laythongtinphong = new SqlCommand();
            laythongtinphong.Connection = sqlConn;
            laythongtinphong.CommandType = CommandType.StoredProcedure;
            laythongtinphong.CommandText = "proc_DichvuOfPhong";

            laythongtinphong.Parameters.AddWithValue("@maphong", maphong);

            SqlDataReader reader = laythongtinphong.ExecuteReader();
            while(reader.Read())
            {
                string maPhong = reader.GetString(0);
                string tenphong = reader.GetString(1);
                double giaphong = reader.GetDouble(2);

                //4 cột này có thể null
                string tendichvu = reader.IsDBNull(3) ? "" : reader.GetString(3);
                double giadichvu = reader.IsDBNull(4) ? 0 : reader.GetDouble(4);
                int soluong = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                Double thanhtien = reader.IsDBNull(6) ? 0 : reader.GetDouble(6);    

                DateTime datein = reader.GetDateTime(7);
                DateTime dateout = reader.GetDateTime(8);
                string tennhanvien = reader.GetString(9);
                string madatphong = reader.GetString(10);

                Thongtin.Add(new ThongTinCuaPhong(maPhong,tenphong,giaphong,tendichvu, giadichvu,soluong, thanhtien, datein,dateout,tennhanvien,madatphong));
            }
            reader.Close();
        }
        #endregion

        #region load giá dịch vụ lên bảng dịch vụ (1)
        public static void LoadDichVuToList(ref List<BangGiaDichVu> list)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand loadDV = sqlConn.CreateCommand();
            loadDV.CommandType = CommandType.Text;
            loadDV.CommandText = "select * from Bang_Gia_Dich_vu";
            loadDV.Connection = sqlConn;    

            SqlDataReader reader = loadDV.ExecuteReader();  
            while(reader.Read())
            {
                string magiadv = reader.GetString(0);
                string tengiadichvu = reader.GetString(1);
                double giagiadv = reader.GetDouble(2);

                list.Add(new BangGiaDichVu(magiadv, tengiadichvu, giagiadv));
            }
            reader.Close() ;
        }
        #endregion

        #region load giá dịch vụ lên bảng dịch vụ (2)
        public static DataTable LoadDichVuToList2()
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            DataTable dt = new DataTable();
            string sql = "select * from Bang_Gia_Dich_vu";
            SqlCommand loadDV = new SqlCommand(sql, sqlConn);
            SqlDataReader reader = loadDV.ExecuteReader();
            dt.Load(reader);
            sqlConn.Close();

            return dt;
        }

        #endregion

        #region chuyển trạng thái phòng sang trống
        public static void ChuyenTrangThai(string maphong)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand suatrangthaiphong = new SqlCommand();
            suatrangthaiphong.CommandType = CommandType.StoredProcedure;
            suatrangthaiphong.Connection = sqlConn;
            suatrangthaiphong.CommandText = "proc_SuaTrangThaiPhong";

            suatrangthaiphong.Parameters.AddWithValue("@maphong", maphong);

            suatrangthaiphong.ExecuteNonQuery();
        }
        #endregion

        #region thay đổi giá phòng
        public static string ThayDoiGiaDaL(double giaphong,string loaiphong)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand thaydoigia = new SqlCommand();
            thaydoigia.CommandType = CommandType.StoredProcedure;   
            thaydoigia.Connection = sqlConn;
            thaydoigia.CommandText = "proc_ThayDoiGiaPhong";

            thaydoigia.Parameters.AddWithValue("@giaphong", giaphong);
            thaydoigia.Parameters.AddWithValue("@loaiphong", loaiphong);

            thaydoigia.ExecuteNonQuery();
            return "success";
        }
        #endregion

        #region thêm dịch vụ
        public static string ThemDichVuDaL(string tendv, string giadichvu)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand themdv = new SqlCommand();
            themdv.CommandType = CommandType.StoredProcedure;
            themdv.Connection = sqlConn;
            themdv.CommandText = "proc_ThemDichVu";

            //Lấy index cuôi của mã bảng giá dv để tạo mã 
            SqlCommand getlastindexmabanggiadichvu = new SqlCommand();
            getlastindexmabanggiadichvu.Connection = sqlConn;
            getlastindexmabanggiadichvu.CommandType = CommandType.Text;
            getlastindexmabanggiadichvu.CommandText = "select dbo.getLastIndexOfBangGiaDichVu()";
            string madv = GetLastIndex.MakeCode(getlastindexmabanggiadichvu, "BGDV");



            themdv.Parameters.AddWithValue("@magiadichvu", madv);
            themdv.Parameters.AddWithValue("@tendichvu", tendv);
            themdv.Parameters.AddWithValue("@giadichvu", Convert.ToDouble(giadichvu));

            themdv.ExecuteNonQuery();

            return "success";
        }
        #endregion

        #region xóa dịch vụ
        public static string XoaDichVuBLL(string madv)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand xoadv = new SqlCommand();
            xoadv.CommandType = CommandType.StoredProcedure;
            xoadv.Connection = sqlConn;
            xoadv.CommandText = "proc_XoaDichVu";

            try
            {
                xoadv.Parameters.AddWithValue("@madv", madv);
                xoadv.ExecuteNonQuery();
            }
            catch {
                return "code_KhongTheXoa";
            }
            return "success";

        }
        #endregion

        #region sửa dịch vụ
        public static string SuaDichVuDAL(string madv, string tendv, string giadv)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand suadv = new SqlCommand();
            suadv.CommandType = CommandType.StoredProcedure;
            suadv.Connection = sqlConn;
            suadv.CommandText = "proc_SuaDichVu";

            suadv.Parameters.AddWithValue("@madv", madv);
            suadv.Parameters.AddWithValue("@tendv", tendv);
            suadv.Parameters.AddWithValue("@giadv", Convert.ToDouble(giadv));

            suadv.ExecuteNonQuery();

            return "success";
        }
        #endregion

        #region load loại phòng
        public static DataTable LoadLoaiPhongDAL(string loaiphong)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            DataTable dt = new DataTable();
            SqlCommand loadPhong = new SqlCommand();
            loadPhong.CommandType = CommandType.StoredProcedure;
            loadPhong.Connection = sqlConn;
            loadPhong.CommandText = "proc_LoadLoaiPhong";

            loadPhong.Parameters.AddWithValue("@loaiphong", loaiphong);

            SqlDataReader reader = loadPhong.ExecuteReader();
            dt.Load(reader);
            sqlConn.Close();
            return dt;
        }
        #endregion

        #region Xóa phòng
        public static void XoaPhongDAL(string maphong)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            SqlCommand xoaphong = new SqlCommand();
            xoaphong.CommandType = CommandType.StoredProcedure;
            xoaphong.Connection = sqlConn;
            xoaphong.CommandText = "proc_XoaPhong";

            xoaphong.Parameters.AddWithValue("@maphong", maphong);

            xoaphong.ExecuteNonQuery();
        }
        #endregion

        #region thêm phòng
        public static string CheckThemPhongDAL(string tenphong, string giaphong, string loaiphong)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand getlastindexofmaphong = new SqlCommand();
            getlastindexofmaphong.CommandType = CommandType.Text;
            getlastindexofmaphong.Connection = sqlConn;
            getlastindexofmaphong.CommandText = "select dbo.getLastIndexOfMaPhong()";

            string maphong = GetLastIndex.MakeCode(getlastindexofmaphong, "P");

            SqlCommand themphong = new SqlCommand();
            themphong.CommandType = CommandType.StoredProcedure;
            themphong.Connection = sqlConn;
            themphong.CommandText = "proc_ThemPhong";

            Debug.WriteLine(maphong);
            themphong.Parameters.AddWithValue("@maphong", maphong);
            themphong.Parameters.AddWithValue("@tenphong",tenphong);
            themphong.Parameters.AddWithValue("@loaiphong", loaiphong);
            themphong.Parameters.AddWithValue("@giaphong", Convert.ToDouble(giaphong));

            themphong.ExecuteNonQuery();

            return "success";
        }
        #endregion

        #region sửa phòng
        public static string SuaPhongDAL(string maphong, string tenphong, string loaiphong, string giaphong)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            SqlCommand suaphong = new SqlCommand();
            suaphong.CommandType = CommandType.StoredProcedure;
            suaphong.Connection = sqlConn;
            suaphong.CommandText = "proc_SuaPhong";

            suaphong.Parameters.AddWithValue("@maphong", maphong);
            suaphong.Parameters.AddWithValue("@tenphong", tenphong);
            suaphong.Parameters.AddWithValue("@loaiphong", loaiphong);
            suaphong.Parameters.AddWithValue("@giaphong", giaphong);

            int rs = suaphong.ExecuteNonQuery();
            if (rs == 0) return "code_khong_the_sua";
            return "success";
        }
        #endregion

        #region Sửa thông tin khách hàng
        public static string SuaThongTinKhachHangDAL(string makh, string tenkh, string diachi,string gioitinh, string sdt)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            SqlCommand suattkhachhang = new SqlCommand();
            suattkhachhang.CommandType = CommandType.StoredProcedure;
            suattkhachhang.Connection = sqlConn;
            suattkhachhang.CommandText = "proc_SuaThongTinKhachHang";

            suattkhachhang.Parameters.AddWithValue("@makh", makh);
            suattkhachhang.Parameters.AddWithValue("@tenkh", tenkh);
            suattkhachhang.Parameters.AddWithValue("@diachi", diachi);
            suattkhachhang.Parameters.AddWithValue("@gioitinh", gioitinh);
            suattkhachhang.Parameters.AddWithValue("@sdt", sdt);

            suattkhachhang.ExecuteNonQuery();
            return "success";
        }

        #endregion

        #region insert hóa đơn
        public static void InsertHoaDonDAL(string madatphong, double tongtien, DateTime ngayinhoadon)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //đầu tiên từ mã đặt phòng lấy tên khách hàng, mã khách hàng
            SqlCommand laytenkhachhang = new SqlCommand();
            laytenkhachhang.CommandType = CommandType.StoredProcedure;
            laytenkhachhang.Connection = sqlConn;
            laytenkhachhang.CommandText = "proc_LayTenKhachHang";

            laytenkhachhang.Parameters.AddWithValue("@madatphong", madatphong);
            SqlDataReader reader = laytenkhachhang.ExecuteReader();
            string makh = "";
            string tenkh = "";
            while(reader.Read())
            {
                makh = reader.GetString(0);
                tenkh = reader.GetString(1);
            }
            reader.Close();
            //sau đó xóa Chi Tiết đặt phòng
            //trước khi xóa CTDP phải xóa Đặt phòng dịch vụ trước
           

            //trước khi xóa đặt phòng dv phải lưu lại lại mã DichVu tron dặt phòng dv
            SqlCommand luumadv = new SqlCommand();
            luumadv.CommandType = CommandType.StoredProcedure;
            luumadv.Connection = sqlConn;
            luumadv.CommandText = "proc_LuuMaDichVuTrongDatPhongDichVu";
            luumadv.Parameters.AddWithValue("@madatphong", madatphong);
            List<string> madv = new List<string>();
            SqlDataReader rdr = luumadv.ExecuteReader();
            while (rdr.Read())
            {
                madv.Add(rdr.GetString(0));
            }
            rdr.Close();
            //xóa đặt phòng dịch vụ
            SqlCommand xoadatphongdichvu = new SqlCommand();
            xoadatphongdichvu.CommandType = CommandType.StoredProcedure;
            xoadatphongdichvu.Connection = sqlConn;
            xoadatphongdichvu.CommandText = "proc_XoaDatPhongDichVu";
            xoadatphongdichvu.Parameters.AddWithValue("@madatphong", madatphong);
            xoadatphongdichvu.ExecuteNonQuery();


            //sao đó xóa dịch vụ
            for (int i = 0;i < madv.Count; i++)
            {
                SqlCommand xoadv = new SqlCommand();
                xoadv.CommandType = CommandType.StoredProcedure;
                xoadv.Connection = sqlConn;
                xoadv.CommandText = "proc_XoaDV";
                xoadv.Parameters.AddWithValue("@madichvu", madv[i]);
                xoadv.ExecuteNonQuery();
            }
                        

            //sau đó xóa Chi tiết đặt phòng
            SqlCommand xoachitietdatphong = new SqlCommand();
            xoachitietdatphong.CommandType = CommandType.StoredProcedure;
            xoachitietdatphong.Connection = sqlConn;
            xoachitietdatphong.CommandText = "proc_XoaChiTietDatPhong";
            xoachitietdatphong.Parameters.AddWithValue("@madatphong", madatphong);
            xoachitietdatphong.ExecuteNonQuery();

            //cuối cùng insert hóa đơn
            //lấy mã cuối của hóa đơn
            SqlCommand getlastindexofhoadon = new SqlCommand();
            getlastindexofhoadon.CommandType = CommandType.Text;
            getlastindexofhoadon.Connection = sqlConn;
            getlastindexofhoadon.CommandText = "select dbo.getLastIndexOfMaHoaDon()";

            string mahoadon = GetLastIndex.MakeCode(getlastindexofhoadon, "HD");

            SqlCommand insertHoaDon = new SqlCommand();
            insertHoaDon.CommandType = CommandType.StoredProcedure;
            insertHoaDon.Connection = sqlConn;
            insertHoaDon.CommandText = "proc_insertHoaDon";

            insertHoaDon.Parameters.AddWithValue("@mahoadon", mahoadon);
            insertHoaDon.Parameters.AddWithValue("@makh", makh);
            insertHoaDon.Parameters.AddWithValue("@tonghoadon", tongtien);
            insertHoaDon.Parameters.AddWithValue("@ngayinhoadon", ngayinhoadon);
            Debug.WriteLine(tongtien);
            insertHoaDon.ExecuteNonQuery();
        }
        #endregion

        #region load hóa đơn
        public static List<HoaDon> LoadHoaDonDAL()
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand loadhoadon = new SqlCommand();
            loadhoadon.CommandType = CommandType.StoredProcedure;
            loadhoadon.Connection = sqlConn;
            loadhoadon.CommandText = "proc_LoadHoaDon";
            List<HoaDon> hoaDon = new List<HoaDon>();
            SqlDataReader reader = loadhoadon.ExecuteReader();
            while (reader.Read())
            {
                string mahoadon = reader.GetString(0);
                string makh = reader.GetString(1);
                string tenkh = reader.GetString(2);
                double tongtien = reader.GetDouble(3);
                DateTime ngayinhoadon = reader.GetDateTime(4);
                hoaDon.Add(new HoaDon(mahoadon, makh, tenkh,tongtien, ngayinhoadon));
            }
            return hoaDon;
        }
        #endregion

        #region xóa hóa đơn
        public static void XoaHoaDonDAL(string maHoaDon)
        {
            SqlConnection sqlConn = SqlConnectionData.Connect();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            SqlCommand loadhoadon = new SqlCommand();
            loadhoadon.CommandType = CommandType.StoredProcedure;
            loadhoadon.Connection = sqlConn;
            loadhoadon.CommandText = "proc_XoaHoaDon";

            loadhoadon.Parameters.AddWithValue("@mahoadon", maHoaDon);
            loadhoadon.ExecuteNonQuery();
        }
        #endregion
    }
}
