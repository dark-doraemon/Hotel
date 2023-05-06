create database DB_QuanLyKhachSan
go
use DB_QuanLyKhachSan
go

/*==============================
|								|
|	 Tạo bảng nhân viên			|
|								|
=================================*/

Create table Nhan_Vien
(
	MaNV nvarchar(20),
	TenNhanVien nvarchar(50),
	SDTNhanVien nvarchar(20),
)


go
--Chỉnh MaNv not null
Alter Table Nhan_Vien
Alter Column MaNv nvarchar(20) Not Null
go
--Thêm khóa chính cho bảng nhân viên(MaNV)
Alter Table Nhan_Vien
Add CONSTRAINT pk_MaNv Primary Key(MaNv)
go

/*==============================
|								|
|	 Tạo bảng tài khoản 		|
|								|
=================================*/
Create table Tai_Khoan
(
	username nvarchar(20),
	password nvarchar(20),
	MaNV nvarchar(20)
)
go
--Chinh username not null
Alter table Tai_Khoan
Alter Column username nvarchar(20) Not Null
go
--Thêm khóa chính cho bảng Tai_khoan(username)
Alter table Tai_Khoan
Add constraint pk_username primary key(username)
go
--Thêm khóa ngoại cho bảng Tai_Khoan(MaNv)
Alter table Tai_Khoan
add Foreign Key (MaNV) references Nhan_Vien(MaNv) 
go

/*==============================
|								|
|	 Tạo bảng khách hàng        |
|								|
=================================*/
create table Khach_Hang
(
	MaKH nvarchar(20),
	TenKH nvarchar(50),
	DiaChi nvarchar(100),
	GioiTinh nvarchar(20),
	SDTKhachHang nvarchar(20),
)
go
alter table Khach_Hang
alter column MaKH nvarchar(20) not null
go
alter table Khach_Hang
add constraint pk_MaKH primary key(MaKH)
go

/*==============================
|								|
|	    Tạo bảng bill           |
|								| 
=================================*/
create table Hoa_Don
(
	MaHoaDon nvarchar(20),
	MaKH nvarchar(20),
	TongHoaDon float,
	NgayInHoaDon datetime
)
go
alter table Hoa_Don
alter column MaHoaDon nvarchar(20) not null
go
alter table Hoa_Don
add constraint pk_MaHoaDon primary key(MaHoaDon)
go
alter table Hoa_Don
add constraint fk_MaKH_HoaDon_to_KhachHang foreign key(MaKH) references Khach_Hang(MaKH)


/*==============================
|								|
|	 Tạo bảng phòng		        |
|								|
=================================*/
create table Phong
(
	MaPhong nvarchar(20),
	TenPhong nvarchar(20),
	LoaiPhong nvarchar(20),
	GiaPhong float,
	TrangThaiPhong nvarchar(20),
	DonDep nvarchar(20)
)
go
alter table Phong 
alter column MaPhong nvarchar(20) not null
go 
alter table Phong
add constraint pk_MaPhong primary key(MaPhong)
go
insert into Phong values
(N'P0001',N'P1001',N'Phòng đơn',150000,N'Trống',N'Chưa dọn'),
(N'P0002',N'P1002',N'Phòng đơn',150000,N'Trống',N'Chưa dọn'),
(N'P0003',N'P1003',N'Phòng đơn',150000,N'Trống',N'Đã dọn'),
(N'P0004',N'P2004',N'Phòng đơn',300000,N'Trống',N'Đã dọn'),
(N'P0005',N'P2005',N'Phòng đôi',300000,N'Trống',N'Đã dọn'),
(N'P0006',N'P2006',N'Phòng đôi',300000,N'Trống',N'chưa dọn'),
(N'P0007',N'P3007',N'Phòng đôi',300000,N'Trống',N'chưa dọn'),
(N'P0008',N'P3008',N'Phòng gia đình',500000,N'Trống',N'Chưa dọn'),
(N'P0009',N'P3009',N'Phòng gia đình',500000,N'v',N'Đã dọn'),
(N'P0010',N'P3010',N'Phòng gia đình',500000,N'Trống',N'Chưa dọn'),
(N'P0011',N'P3011',N'Phòng gia đình',500000,N'Trống',N'Đã dọn')
go
/*==============================
|								|
|	    Bảng giá dịch vụ        |
|								| 
=================================*/
Create table Bang_Gia_Dich_vu
(
	MaGiaDichVu nvarchar(20) not null,
	TenGiaDichVu nvarchar(50),
	GiaGiaDichVu float
)
go
alter table Bang_Gia_Dich_vu
add constraint pk_MaGiaDichVu primary key(MaGiaDichVu)
go

insert into Bang_Gia_Dich_vu values
(N'GDV001','Coca',10000),
(N'GDV002','Pepsi',10000),
(N'GDV003','Sting',10000),
(N'GDV004','Bia',12000),
(N'GDV005','Mì xào',30000),
(N'GDV006','Cơm chiên',30000),
(N'GDV007','Cá viên chiên',30000)




/*==============================
|								|
|	 Tạo bảng dịch vụ           |
|								|
=================================*/
create table Dich_Vu
(
	MaDichVu nvarchar(20),
	TenDichVu nvarchar(50),
	SoLuong int,
	GiaDichVu float,
)
go 
alter table Dich_Vu
alter column MaDichVu nvarchar(20) not null
go
alter table Dich_Vu
add constraint pk_MaDichVu primary key(MaDichVu)
go
alter table Dich_Vu Add MaGiaDichVu nvarchar(20)
go 
alter table Dich_Vu 
add constraint fk_MaGiaDichVu_to_BangGiaDichVu foreign key(MaGiaDichVu) references Bang_Gia_Dich_vu(MaGiaDichVu)



/*==============================
|								|
|  Tạo bảng chi tiết đặt phòng  |
|								|
=================================*/

create table Chi_Tiet_Dat_Phong
(
	MaDatPhong nvarchar(20),
	MaPhong nvarchar(20),
	MaKH nvarchar(20),
	SoPhong nvarchar(20),
	DateIn datetime,
	DateOut datetime,
	MaNV nvarchar(20)
)
go
alter table Chi_Tiet_Dat_Phong
alter column MaDatPhong nvarchar(20) not null
go 
alter table Chi_Tiet_Dat_Phong
add constraint pk_Ma_Dat_Phong primary key(MaDatPhong)
go
alter table Chi_Tiet_Dat_Phong
add constraint  fk_MaKH_ChiTietDatPhong_to_Khach_Hang foreign key (MaKH) references Khach_Hang(MaKH),--KhachHang tới DatPhong
constraint  fk_MaPhong_ChiTietDatPhong_to_Phong foreign key (MaPhong) references Phong(MaPhong)--Phong tới DatPHong
go
alter table Chi_Tiet_Dat_Phong
add constraint fk_MaNV_ChiTietDatPhong_to_NhanVIen foreign key(MaNV) references Nhan_Vien(MaNV)
go



/*Do mối quan hệ giữa DatPhong và DichVu 
là mối quan hệ nhiều nhiều nên phải tạo 
thêm 1 một bảng mới để tránh trùng lặp dữ liệu*/
CREATE TABLE Dat_Phong_Dich_Vu 
(
    MaDatPhong nvarchar(20) not null,
    MaDichVu nvarchar(20) not null,
)

alter table Dat_Phong_Dich_Vu 
add constraint pk_MaDatPhong_MaDichVu_Dat_Phong_Dich_Vu primary key(MaDatPhong, MaDichVu)
go


alter table Dat_Phong_Dich_Vu 
add constraint fk_MaDatPhong_DatPhongDichVu_to_DatPhong foreign key(MaDatPhong) references Chi_Tiet_Dat_Phong(MaDatPhong),
constraint fk_MaDichVu_DatPhongDichVu_to_DatPhong foreign key(MaDichVu) references Dich_Vu(MaDichVu)

go


/*==============================
|								|
|  các funtion và procedure		|
|								|
=================================*/

/*==================================
|									|
|store procedure kiểm tra đăng nhập |
|									|
|===================================*/

create procedure proc_login
@user nvarchar(20),
@pass nvarchar(20)
as
begin
	select NV.TenNhanVien,NV.MaNV
	from Tai_Khoan TK join Nhan_Vien NV on TK.MaNV = NV.MaNV
	where TK.username = @user and TK.password = @pass

end
go



/*======================================
|										|
|function lấy vị trị cuối cùng của mã nv|
|										|
|=======================================*/

create function getLastIndexOfMaNV()
returns int
as 
begin 
	declare @lastvalue int
	set @lastvalue = (select Max(Cast(right(MaNV,4) as int)) from Nhan_Vien)
	if @lastvalue is null set @lastvalue = 0 
	return @lastvalue
end
go

 /*======================================
|										|
|     procedure insert nhân viên        |
|										|
|=======================================*/

create procedure insertNhanVien
@manv nvarchar(20),
@tennv nvarchar(50),
@sdt varchar(20)
as
begin
	insert into Nhan_Vien values
	(@manv,@tennv,@sdt)
end
go

 /*======================================
|										|
|     procedure insert tài khoản        |
|										|
|=======================================*/

create procedure insertTaiKhoan
@username nvarchar(20),
@password nvarchar(20),
@manv nvarchar(20)
as
begin
	insert into Tai_Khoan values
	(@username,@password,@manv)
end
go

 /*======================================
|										|
| xóa mã nv khi insert không thành công |
|										|
|=======================================*/

create procedure deleteMaNV
@manv nvarchar(20)
as
begin
	delete from Nhan_Vien 
	where MaNV = @manv
end
go

 

 /*======================================
|										|
|			load loại phòng				| 
|										|
|=======================================*/

create procedure proc_LoadLoaiPhong
@loaiphong nvarchar(20)
as
begin
	select * from Phong where LoaiPhong = @loaiphong
end
go


 /*======================================
|										|
|            lấy index cuối MaKh        |
|										|
|=======================================*/

create function getLastIndexOfMaKH()
returns int
as 
begin 
	declare @lastvalue int
	set @lastvalue = (select Max(Cast(right(MaKH,4) as int)) from Khach_Hang)
	if @lastvalue is null set @lastvalue = 0 
	return @lastvalue
end
go

 /*======================================
|										|
|            thêm khách hàng            |
|										|
|=======================================*/

create procedure proc_insertKhachHang
@makh nvarchar(20),
@tenkh nvarchar(20),
@diachi nvarchar(100),
@gioitinh nvarchar(20),
@sdtkhachhang nvarchar(20)
as
begin 
	insert into Khach_Hang values
	(@makh,@tenkh,@diachi,@gioitinh,@sdtkhachhang)
end
go


/*======================================
|										|
| insert dich vu                        |
|										|
|=======================================*/

create procedure proc_insertDichVu
@madichvu nvarchar(20),
@tendichvu nvarchar(20),
@soluong int,
@giadichvu int,
@magiadichvu nvarchar(20)
as
begin 
	insert into Dich_Vu values
	(@madichvu,@tendichvu,@soluong,@giadichvu,@magiadichvu)
end
go

/*======================================
|										|
|lấy index cuối mã chi tiết đặt phòng   |
|										|
|=======================================*/

create function getLastIndexOfMaChiTietDatPhong()
returns int
as 
begin 
	declare @lastvalue int
	set @lastvalue = (select Max(Cast(right(MaDatPhong,4) as int)) from Chi_Tiet_Dat_Phong)
	if @lastvalue is null set @lastvalue = 0 
	return @lastvalue
end
go

/*======================================
|										|
|      thêm chi tiêt đặt phòng			|
|										|
|=======================================*/

create procedure proc_insertChiTietDatPHong
@madatphong nvarchar(20),
@maphong nvarchar(20),
@makh nvarchar(20),
@sophong nvarchar(20),
@datein datetime,
@dateout datetime,
@manv nvarchar(20)
as
begin
	insert into Chi_Tiet_Dat_Phong values
	(@madatphong,@maphong,@makh,@sophong,@datein,@dateout,@manv)
end
go


 /*======================================
|										|
|lấy index cuối mã dịch vụ              |
|										|
|=======================================*/


create function getLastIndexOfMaDichVu()
returns int
as 
begin 
	declare @lastvalue int
	set @lastvalue = (select Max(Cast(right(MaDichVu,4) as int)) from Dich_Vu)
	if @lastvalue is null set @lastvalue = 0 
	return @lastvalue
end
go


/*======================================
|										|
|    insert dat phong dich vu			|
|										|
|=======================================*/

create procedure proc_DatPhongDichVu
@madatphong nvarchar(20),
@madichvu nvarchar(20)
as
begin
	insert into Dat_Phong_Dich_Vu values
	(@madatphong,@madichvu);
end
go


 /*======================================
|										|
|lấy thông tin của phòng                | 
|										|
|=======================================*/

create procedure proc_DichvuOfPhong
@maphong nvarchar(20)
as 
begin
	select P.MaPhong,
			p.TenPhong,
			P.GiaPhong,
			DV.TenDichVu,
			BGDV.GiaGiaDichVu,
			DV.SoLuong,
			DV.GiaDichVu,
			CONVERT(Date, CTDP.DateIn) as DateIn,
			Convert(date,CTDP.DateOut) as DateOut,
			NV.TenNhanVien,
			CTDP.MaDatPhong

	from Phong P join Chi_Tiet_Dat_Phong CTDP on P.MaPhong = CTDP.MaPhong
				 full outer join Dat_Phong_Dich_Vu DPDV on CTDP.MaDatPhong = DPDV.MaDatPhong
				 full outer join Dich_Vu DV on DV.MaDichVu = DPDV.MaDichVu
				 full outer join Bang_Gia_Dich_vu BGDV on BGDV.MaGiaDichVu = DV.MaGiaDichVu
				 join Nhan_Vien NV on NV.MaNV = CTDP.MaNV
				 where P.MaPhong = @maphong
end
go

/*======================================
|										|
|lấy thông tin khách hàng đăng kí phòng | 
|										|
|=======================================*/

create procedure proc_ThongTinKhachHangDangKyPhong
as
begin
	select KH.MaKH,
			KH.TenKH,
			KH.DiaChi,
			KH.GioiTinh,
			KH.SDTKhachHang,
			CONVERT(Date, CTDP.DateIn) as DateIn,
			Convert(date,CTDP.DateOut) as DateOut,
			P.TenPhong
	from Khach_Hang KH join Chi_Tiet_Dat_Phong CTDP on KH.MaKH = CTDP.MaKH
						join Phong P on P.MaPhong = CTDP.MaPhong
end
go


/*======================================
|										|
|   lấy index cuổi của phòng		    | 
|										|
|=======================================*/

create function getLastIndexOfMaPhong()
returns int
as 
begin 
	declare @lastvalue int
	set @lastvalue = (select Max(Cast(right(MaPhong,4) as int)) from Phong)
	if @lastvalue is null set @lastvalue = 0 
	return @lastvalue
end
go

 /*======================================
|										|
|   thêm phòng						    | 
|										|
|=======================================*/

create procedure proc_ThemPhong
@maphong nvarchar(20),
@tenphong nvarchar(20),
@loaiphong nvarchar(20),
@giaphong float
as
begin 
	insert into Phong values
	(@maphong,@tenphong,@loaiphong,@giaphong,N'Trống',N'Đã dọn')
end
go

 /*======================================
|										|
|   Sửa phòng						    | 
|										|
|=======================================*/

create procedure proc_SuaPhong
@maphong nvarchar(20),
@tenphong nvarchar(20),
@loaiphong nvarchar(20),
@giaphong float
as
begin
	Update Phong set TenPhong = @tenphong,
					LoaiPhong = @loaiphong,
					GiaPhong = @giaphong
					where MaPhong = @maphong and TrangThaiPhong = N'Trống'
end
go


/*======================================
|										|
|   xóa phòng    					    | 
|										|
|=======================================*/

create procedure proc_XoaPhong
@maphong nvarchar(20)
as
begin
	Delete Phong where  MaPhong = @maphong
end
go


 /*======================================
|										|
|   thay đổi giá phòng                  | 
|										|
|=======================================*/

create procedure proc_ThayDoiGiaPhong
@giaphong float,
@loaiphong nvarchar(20)
as
begin 
	update Phong set GiaPhong = @giaphong where LoaiPhong = @loaiphong and TrangThaiPhong = N'Trống'
end
go


 /*======================================
|										|
|        thêm dịch vụ                   | 
|										|
|=======================================*/

create procedure proc_ThemDichVu
@magiadichvu nvarchar(20),
@tendichvu nvarchar(50),
@giadichvu float 
as
begin
	insert into Bang_Gia_Dich_vu values
	(@magiadichvu,@tendichvu,@giadichvu)
end
go

 /*======================================
|										|
|    lấy index cuối bảng giá dịch vụ    | 
|										|
|=======================================*/

create function getLastIndexOfBangGiaDichVu()
returns int
as 
begin 
	declare @lastvalue int
	set @lastvalue = (select Max(Cast(right(MaGiaDichVu,3) as int)) from Bang_Gia_Dich_vu)
	if @lastvalue is null set @lastvalue = 0 
	return @lastvalue
end
go

 /*======================================
|										|
|   xóa dịch vụ						    | 
|										|
|=======================================*/

create procedure proc_XoaDichVu
@madv nvarchar(20)
as
begin
	delete Bang_Gia_Dich_vu where MaGiaDichVu = @madv
end
go

 /*======================================
|										|
|   sửa dịch vụ						    | 
|										|
|=======================================*/

create procedure proc_SuaDichVu
@madv nvarchar(20),
@tendv nvarchar(50),
@giadv float
as
begin
	update Bang_Gia_Dich_vu set TenGiaDichVu = @tendv,
								GiaGiaDichVu = @giadv
								where MaGiaDichVu = @madv

end
go

 /*======================================
|										|
|   Sửa thông tin khách hàng			| 
|										|
|=======================================*/
create procedure proc_SuaThongTinKhachHang
@makh nvarchar(20),
@tenkh nvarchar(20),
@diachi nvarchar(100),
@gioitinh nvarchar(20),
@sdt nvarchar(20)
as
begin
	Update Khach_Hang Set TenKH = @tenkh,
							DiaChi = @diachi,
							GioiTinh = @gioitinh,
							SDTKhachHang = @sdt
							where MaKH = @makh

end
go


/*======================================
|										|
|	sửa trạng thái phòng                | 
|										|
|=======================================*/

create procedure proc_SuaTrangThaiPhong
@maphong nvarchar(20)
as 
begin 
	update Phong set TrangThaiPhong = N'Trống' where MaPhong = @maphong
end
go

/*======================================
|										|
|	insert hóa đơn		                | 
|										|
|=======================================*/

create procedure proc_LayTenKhachHang
@madatphong nvarchar(20)
as
begin
	select KH.MaKH,KH.TenKH
	from Chi_Tiet_Dat_Phong CTDP join Khach_Hang KH on CTDP.MaKH = KH.MaKH
	where CTDP.MaDatPhong = @madatphong
end
go


create procedure proc_XoaDatPhongDichVu
@madatphong nvarchar(20)
as
begin
	Delete Dat_Phong_Dich_Vu where MaDatPhong = @madatphong
end
go

create procedure proc_LuuMaDichVuTrongDatPhongDichVu
@madatphong nvarchar(20)
as
begin
	select DPDV.MaDichVu
	from Chi_Tiet_Dat_Phong CTDP join Dat_Phong_Dich_Vu DPDV on CTDP.MaDatPhong = DPDV.MaDatPhong
	where CTDP.MaDatPhong = @madatphong

end
go

create procedure proc_XoaDV
@madichvu nvarchar(20)
as
begin
	delete Dich_Vu where MaDichVu= @madichvu
end
go

create procedure proc_XoaChiTietDatPhong
@madatphong nvarchar(20)
as
begin
	Delete Chi_Tiet_Dat_Phong where MaDatPhong = @madatphong
end
go

create function getLastIndexOfMaHoaDon()
returns int
as 
begin 
	declare @lastvalue int
	set @lastvalue = (select Max(Cast(right(MaHoaDon,4) as int)) from Hoa_Don)
	if @lastvalue is null set @lastvalue = 0 
	return @lastvalue
end
go

create procedure proc_insertHoaDon
@mahoadon nvarchar(20),
@makh nvarchar(20),
@tonghoadon float,
@ngayinhoadon datetime
as
begin
	insert into Hoa_Don values
	(@mahoadon,@makh,@tonghoadon,@ngayinhoadon)
end
go

/*======================================
|										|
|	load hoa đơn		                | 
|										|
|=======================================*/

create procedure proc_LoadHoaDon
as
begin
	select HD.MaHoaDon,KH.MaKH,KH.TenKH,HD.TongHoaDon,HD.NgayInHoaDon
	from Khach_Hang KH join Hoa_Don HD on KH.MaKH = HD.MaKH 
end
go


/*======================================
|										|
|	xoa hoa đơn							| 
|										|
|=======================================*/
create procedure proc_XoaHoaDon
@mahoadon nvarchar(20)
as
begin 
	Delete Hoa_Don Where MaHoaDon = @mahoadon;

end