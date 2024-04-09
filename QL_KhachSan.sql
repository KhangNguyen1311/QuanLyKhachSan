CREATE DATABASE [QL_KhachSan]
GO

USE [QL_KhachSan]
GO

CREATE TABLE [dbo].[NhanVien](
	[MaNV] [char](10) NOT NULL,
	[TenNV] [nvarchar](50) NOT NULL,
	[SDT] [nvarchar](11) NOT NULL,
	[NgaySinh] [nvarchar](15) NOT NULL,
	[DiaChi] [nvarchar](MAX) NOT NULL,
	[HinhAnh] [nvarchar](MAX) NOT NULL,
	[VaiTro] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phai] [bit] NOT NULL,
	[MatKhau] [nvarchar](25) NOT NULL,
	[CCCD] [nvarchar](15) NOT NULL,
	[TinhTrang] [nvarchar](25) NOT NULL,
	PRIMARY KEY (MaNV)
)
GO

CREATE TABLE [dbo].[KhachHang](
	[MaKH] [char](10) NOT NULL,
	[TenKH] [nvarchar](50) NOT NULL,
	[SDT] [nvarchar](11) NOT NULL,
	[NgaySinh] [nvarchar](15) NOT NULL,
	[DiaChi] [nvarchar](MAX) NOT NULL,
	[Phai] [bit] NOT NULL,
	[MaCMT] [nvarchar](15) NOT NULL,
	[QuocTich] [nvarchar](50) NOT NULL,
	PRIMARY KEY (MaKH)
)
GO

CREATE TABLE [dbo].[HoaDon](
	[MaHD] [char](10) NOT NULL,
	[MaPDP] [char](10) NOT NULL,
	[MaPhong] [char](10) NOT NULL,
	[TongGiaTri] [float] NOT NULL,
	[NgayInHD] [datetime] NOT NULL,
	[DanhGia] [nvarchar](100) NOT NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
	PRIMARY KEY (MaHD)
)
GO


CREATE TABLE [dbo].[PhieuDatPhong](
	[MaPDP] [char](10) NOT NULL,
	[MaKH] [char](10) NOT NULL,
	[MaNV] [char](10) NOT NULL,
	[NgayTao] [datetime] NOT NULL,
	PRIMARY KEY (MaPDP)
)
GO

CREATE TABLE [dbo].[ChiTietPhieuDatPhong](
	[MaPDP] [char](10) NOT NULL,
	[MaPhong] [char](10) NOT NULL,
	[TongGiaTri] [float] NOT NULL,
	[NgayDat] [date] NOT NULL,
	[NgayKetThuc] [date] NOT NULL,
	[GioDat] [time] NOT NULL,
	[GioKetThuc] [time] NOT NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
	[SoNguoi] [int] NOT NULL,

	PRIMARY KEY (MaPDP,MaPhong)
)
GO

CREATE TABLE [dbo].[ChiTietDichVuPhieuDatPhong](
	[MaPDP] [char](10) NOT NULL,
	[MaPhong] [char](10) NOT NULL,
	[MaDV] [char](10) NOT NULL,
	[SoLuongDV] [int] NOT NULL,
	[TongGiaTri] [float] NOT NULL,
	PRIMARY KEY (MaPDP,MaDV,MaPhong)
)
GO

CREATE TABLE [dbo].[Phong](
	[MaPhong] [char](10) NOT NULL,
	[MaLoaiPhong] [char](10) NOT NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
	[Tang] [int] NOT NULL,
	[GhiChu] [nvarchar](50) NOT NULL,
	PRIMARY KEY (MaPhong)
)
GO

CREATE TABLE [dbo].[LoaiPhong](
	[MaLoaiPhong] [char](10) NOT NULL,
	[TenLoaiPhong] [nvarchar](50) NOT NULL,
	[GiaTheoGio] [float] NOT NULL,
	[GiaTheoNgay] [float] NOT NULL,
	PRIMARY KEY (MaLoaiPhong)
)
GO

CREATE TABLE [dbo].[DichVu](
	[MaDV] [char](10) NOT NULL,
	[MaLoaiDV] [char](10) NOT NULL,
	[TenDV] [nvarchar](50) NOT NULL,
	[GiaTien] [float] NOT NULL,
	PRIMARY KEY (MaDV)
)
GO

CREATE TABLE [dbo].[LoaiDichVu](
	[MaLoaiDV] [char](10) NOT NULL,
	[TenLoaiDV] [nvarchar](50) NOT NULL,
	PRIMARY KEY (MaLoaiDV)
)
GO

CREATE TABLE [dbo].[TienNghiPhong](
	[MaLoaiPhong] [char](10) NOT NULL,
	[TenTienNghi] [nvarchar](50) NOT NULL,
	[SL] [int] NOT NULL,
	PRIMARY KEY (MaLoaiPhong,TenTienNghi)
)
GO

ALTER TABLE [dbo].[PhieuDatPhong] ADD FOREIGN KEY(MaNV) REFERENCES [dbo].[NhanVien](MaNV)
GO

ALTER TABLE [dbo].[PhieuDatPhong] ADD FOREIGN KEY(MaKH) REFERENCES [dbo].[KhachHang](MaKH)
GO

ALTER TABLE [dbo].[ChiTietPhieuDatPhong] ADD FOREIGN KEY(MaPDP) REFERENCES [dbo].[PhieuDatPhong](MaPDP)
GO

ALTER TABLE [dbo].[ChiTietPhieuDatPhong] ADD FOREIGN KEY(MaPhong) REFERENCES [dbo].[Phong](MaPhong)
GO

ALTER TABLE [dbo].[ChiTietDichVuPhieuDatPhong] ADD FOREIGN KEY(MaPDP,MaPhong) REFERENCES [dbo].[ChiTietPhieuDatPhong](MaPDP,MaPhong)
GO

ALTER TABLE [dbo].[ChiTietDichVuPhieuDatPhong] ADD FOREIGN KEY(MaDV) REFERENCES [dbo].[DichVu](MaDV)
GO

ALTER TABLE [dbo].[HoaDon] ADD FOREIGN KEY(MaPDP,MaPhong) REFERENCES [dbo].[ChiTietPhieuDatPhong](MaPDP,MaPhong)
GO

ALTER TABLE [dbo].[Phong] ADD FOREIGN KEY(MaLoaiPhong) REFERENCES [dbo].[LoaiPhong](MaLoaiPhong)
GO

ALTER TABLE [dbo].[DichVu] ADD FOREIGN KEY(MaLoaiDV) REFERENCES [dbo].[LoaiDichVu](MaLoaiDV)
GO

ALTER TABLE [dbo].[TienNghiPhong] ADD FOREIGN KEY(MaLoaiPhong) REFERENCES [dbo].[LoaiPhong](MaLoaiPhong)
GO

CREATE PROCEDURE InsertNhanVien
@TenNV nvarchar(50),
@SDT nvarchar(11),
@NgaySinh nvarchar(15),
@DiaChi nvarchar(MAX),
@HinhAnh nvarchar(MAX),
@VaiTro nvarchar(100),
@Email nvarchar(50),
@Phai nvarchar(3),
@MatKhau nvarchar(25),
@CCCD nvarchar(15)
AS
BEGIN
	DECLARE @gioitinh bit;
	SET @gioitinh = CASE WHEN @Phai LIKE N'Nam' Then 0 WHEN @Phai = N'Nữ' THEN 1 END;
	DECLARE @MaNV char(10);
	SET @MaNV = 'NV' + FORMAT(((SELECT COUNT(*) FROM NhanVien) + 1), '0000');
	INSERT INTO NhanVien(MaNV,TenNV,SDT,NgaySinh,DiaChi,HinhAnh,Email,Phai,MatKhau,CCCD,TinhTrang,VaiTro) values(@MaNV,@TenNV,@SDT,@NgaySinh,@DiaChi,@HinhAnh,@Email,@gioitinh,@MatKhau,@CCCD,N'Hoạt động',@VaiTro)
END;
GO

CREATE PROCEDURE InsertKhachHang
	
	@TenKH nvarchar(50),
	@SDT nvarchar(11),
	@NgaySinh nvarchar(15),
	@DiaChi nvarchar(MAX),
	@Phai nvarchar(3),
	@MaCMT nvarchar(15),
	@QuocTich nvarchar(50)
AS
BEGIN
	DECLARE @gioitinh bit;
	SET @gioitinh = CASE WHEN @Phai LIKE N'Nam' Then 0 WHEN @Phai = N'Nữ' THEN 1 END;
	DECLARE @MaKH char(10);
	SET @MaKH = 'KH' + FORMAT(((SELECT COUNT(*) FROM KhachHang) + 1), '0000');
	INSERT INTO KhachHang(MaKH,TenKH,SDT,NgaySinh,DiaChi,Phai,MaCMT,QuocTich) VALUES(@MaKH,@TenKH,@SDT,@NgaySinh,@DiaChi,@gioitinh,@MaCMT,@QuocTich)
END;
GO

CREATE PROCEDURE InsertHoaDon
	@MaPDP char(10),
	@MaPhong char(10),
	@TongGiaTri float
AS
BEGIN
	DECLARE @MaHD char(10);
	SET @MaHD = 'HD' + FORMAT(((SELECT COUNT(*) FROM HoaDon) + 1), '0000');
	DECLARE @NgayIn datetime;
	SET @NgayIn = GETDATE();
	INSERT INTO HoaDon(MaHD,MaPDP,MaPhong,TongGiaTri,NgayInHD,DanhGia,TinhTrang) VALUES(@MaHD,@MaPDP,@MaPhong,@TongGiaTri,@NgayIn,'',N'Chưa thanh toán');
END;
GO

CREATE PROCEDURE InsertPhieuDatPhong
	@MaKH char(10),
	@MaNV char(10)
AS
BEGIN
	DECLARE @MaPDP char(10);
	SET @MaPDP = 'PDP' + FORMAT(((SELECT COUNT(*) FROM PhieuDatPhong) + 1), '0000');
	DECLARE @NgayIn datetime;
	SET @NgayIn = GETDATE();
	INSERT INTO PhieuDatPhong(MaPDP,MaKH,MaNV,NgayTao) VALUES(@MaPDP,@MaKH,@MaNV,@NgayIn)
END;
GO






CREATE PROCEDURE InsertChiTietPhieuDatPhong
	@MaPDP char(10),
	@MaPhong char(10),
	@NgayDat date,
	@NgayKetThuc date,
	@GioDat time,
	@GioKetThuc time,
	@SoNguoi int
AS
BEGIN
	DECLARE @SoNgayChenhLech INT;
	SET @SoNgayChenhLech = DATEDIFF(DAY, @NgayDat, @NgayKetThuc)
	DECLARE @SoGioChenhLech INT;
	SET @SoGioChenhLech = DATEDIFF(HOUR, @GioDat, @GioKetThuc)
	DECLARE @tienphong float;
	SET @tienphong = @SoNgayChenhLech * (SELECT GiaTheoNgay FROM LoaiPhong inner join Phong on LoaiPhong.MaLoaiPhong = Phong.MaLoaiPhong WHERE Phong.MaPhong = @MaPhong)
		+ @SoGioChenhLech * (SELECT GiaTheoGio FROM LoaiPhong inner join Phong on LoaiPhong.MaLoaiPhong = Phong.MaLoaiPhong WHERE Phong.MaPhong = @MaPhong);
	INSERT INTO ChiTietPhieuDatPhong(MaPDP,MaPhong,NgayDat,NgayKetThuc,GioDat,GioKetThuc,TongGiaTri,TinhTrang,SoNguoi) VALUES(@MaPDP,@MaPhong,@NgayDat,@NgayKetThuc,@GioDat,@GioKetThuc,@tienphong,N'Chưa thanh toán',@SoNguoi);
END;
GO

CREATE TRIGGER TaoHD
ON ChiTietPhieuDatPhong
AFTER UPDATE
AS
BEGIN

	IF UPDATE(TinhTrang)
	BEGIN
		DECLARE @MaPDP char(10);
		SET @MaPDP = (SELECT MaPDP FROM inserted where TinhTrang = N'Đã thanh toán');
		DECLARE @MaPhong char(10);
		SET @MaPhong = (SELECT MaPhong FROM inserted where TinhTrang = N'Đã thanh toán');
		DECLARE @TongGiaTri float;
		SET @TongGiaTri = ((SELECT TongGiaTri FROM inserted WHERE TinhTrang = N'Đã thanh toán'));
		EXEC InsertHoaDon @MaPDP,@MaPhong,@TongGiaTri;
	END
END
GO



CREATE PROCEDURE InsertChiTietDVPhieuDatPhong
	@MaPDP char(10),
	@MaPhong char(10),
	@MaDV char(10),
	@SoLuongDichVu int
AS
BEGIN
	DECLARE @TongGiaTri float;
	SET @TongGiaTri = (SELECT GiaTien FROM DichVu WHERE MaDV = @MaDV) * @SoLuongDichVu;
	INSERT INTO ChiTietDichVuPhieuDatPhong(MaPDP,MaPhong,MaDV,SoLuongDV,TongGiaTri) VALUES(@MaPDP,@MaPhong,@MaDV,@SoLuongDichVu,@TongGiaTri)
END;
GO

CREATE TRIGGER TangGiaTriDV
ON ChiTietDichVuPhieuDatPhong
AFTER INSERT,UPDATE
AS
BEGIN
	UPDATE ChiTietPhieuDatPhong
    SET TongGiaTri += (SELECT SUM(TongGiaTri) FROM ChiTietDichVuPhieuDatPhong WHERE ChiTietPhieuDatPhong.MaPDP = ChiTietDichVuPhieuDatPhong.MaPDP AND ChiTietPhieuDatPhong.MaPhong = ChiTietDichVuPhieuDatPhong.MaPhong)
    FROM ChiTietPhieuDatPhong
    WHERE EXISTS (SELECT 1 FROM inserted CHD WHERE ChiTietPhieuDatPhong.MaPDP = CHD.MaPDP AND ChiTietPhieuDatPhong.MaPhong = CHD.MaPhong);
END
GO

CREATE TRIGGER TruGiaTriDV
ON ChiTietPhieuDatPhong
AFTER DELETE
AS
BEGIN
	UPDATE ChiTietPhieuDatPhong
	SET TongGiaTri = TongGiaTri - ISNULL((SELECT TongGiaTri FROM deleted),0)
	FROM ChiTietPhieuDatPhong
	WHERE ChiTietPhieuDatPhong.MaPDP IN (SELECT MaPDP FROM deleted) AND ChiTietPhieuDatPhong.MaPhong IN (SELECT MaPhong FROM deleted);
END
GO

CREATE PROCEDURE InsertPhong
	@MaLoaiPhong char(10),
	@Tang int
AS
BEGIN
	DECLARE @MaPhong char(10);
	SET @MaPhong = CONCAT('P',@Tang,'00',((SELECT COUNT(*) FROM Phong WHERE Tang = @Tang) + 1));
	INSERT INTO Phong(MaPhong,MaLoaiPhong,TinhTrang,Tang,GhiChu) VALUES(@MaPhong,@MaLoaiPhong,N'Phòng trống',@Tang,N'Đã dọn dẹp');
END;
GO

CREATE PROCEDURE InsertLoaiPhong
	@TenLoaiPhong nvarchar(50),
	@GiaTheoGio float,
	@GiaTheoNgay float
AS
BEGIN
	DECLARE @MaLoaiPhong char(10);
	SET @MaLoaiPhong = 'LP' + FORMAT(((SELECT COUNT(*) FROM LoaiPhong) + 1), '0000');
	INSERT INTO LoaiPhong(MaLoaiPhong,TenLoaiPhong,GiaTheoGio,GiaTheoNgay) VALUES(@MaLoaiPhong,@TenLoaiPhong,@GiaTheoGio,@GiaTheoNgay);
END;
GO

CREATE PROCEDURE InsertDichVu
	@MaLoaiDV char(10),
	@TenDV nvarchar(50),
	@GiaTien float
AS
BEGIN
	DECLARE @MaDV char(10);
	SET @MaDV = 'DV' + FORMAT(((SELECT COUNT(*) FROM DichVu) + 1), '0000');
	INSERT INTO DichVu(MaDV,MaLoaiDV,TenDV,GiaTien) VALUES(@MaDV,@MaLoaiDV,@TenDV,@GiaTien);
END;
GO

CREATE PROCEDURE InsertLoaiDichVu
	@TenLoaiDichVu nvarchar(50)
AS
BEGIN
	DECLARE @MaLoaiDV char(10);
	SET @MaLoaiDV = 'LDV' + FORMAT(((SELECT COUNT(*) FROM LoaiDichVu) + 1), '0000');
	INSERT INTO LoaiDichVu(MaLoaiDV,TenLoaiDV) VALUES(@MaLoaiDV,@TenLoaiDichVu);
END;
GO

CREATE PROCEDURE InsertTienNghiPhong
	@MaLoaiPhong char(10),
	@Ten nvarchar(50),
	@SL int
AS
BEGIN
	INSERT INTO TienNghiPhong(MaLoaiPhong,TenTienNghi,SL) VALUES(@MaLoaiPhong,@Ten,@SL);
END;
GO