USE QL_KhachSan
GO


CREATE VIEW [dbo].[ViewHoaDonNhanVienPhieuDatPhong] AS
SELECT 
    HoaDon.MaHD, 
    HoaDon.NgayInHD, 
    HoaDon.TongGiaTri, 
    NhanVien.TenNV
FROM 
    HoaDon
INNER JOIN PhieuDatPhong ON HoaDon.MaPDP = PhieuDatPhong.MaPDP
INNER JOIN NhanVien ON PhieuDatPhong.MaNV = NhanVien.MaNV;

GO

CREATE VIEW DanhSachNhanVien AS
	
	SELECT MaNV,TenNV,NgaySinh,VaiTro,(CASE Phai WHEN 0 THEN 'Nam' WHEN 1 THEN N'Nữ' END) AS [GioiTinh],DiaChi,SDT,CCCD FROM NhanVien

GO

CREATE VIEW DanhSachKhachHang AS
	SELECT MaKH, TenKH,NgaySinh,(CASE Phai WHEN 0 THEN 'Nam' WHEN 1 THEN N'Nữ' END) AS [GioiTinh],DiaChi,SDT,MaCMT,QuocTich from KhachHang

GO;


CREATE VIEW DanhSachThongTinPhong AS
SELECT
    MaPhong, 
    MaPDP,
    TenLoaiPhong, 
    TinhTrang, 
    Tang, 
    GhiChu, 
    SoNguoi, 
    TinhTrangThanhToan,
    TenKH, 
    MaKH, 
    NgayDat, 
    NgayKetThuc, 
    GioDat, 
    GioKetThuc,
    CASE 
        WHEN TinhTrang = N'Phòng trống' THEN 0
        ELSE DATEDIFF(day, NgayDat, NgayKetThuc)
    END AS SoNgay,
    CASE 
        WHEN TinhTrang = N'Phòng trống' THEN 0
        ELSE
            CASE
                WHEN TinhTrang = N'Đã thanh toán' OR TinhTrangThanhToan IS NULL THEN NULL 
                ELSE 
                    CASE 
                        WHEN NgayKetThuc = NgayDat THEN DATEDIFF(hour, GioDat, GioKetThuc)
                        ELSE DATEDIFF(day, NgayDat, NgayKetThuc) * 24 + DATEDIFF(hour, GioDat, GioKetThuc)
                    END
            END
    END AS SoGio
FROM (
    SELECT
        Phong.MaPhong,
        PhieuDatPhong.MaPDP,
        LoaiPhong.TenLoaiPhong,
        Phong.TinhTrang,
        Phong.Tang,
        Phong.GhiChu,
        ChiTietPhieuDatPhong.SoNguoi,
        ChiTietPhieuDatPhong.TinhTrang AS TinhTrangThanhToan,
        KhachHang.TenKH,
        KhachHang.MaKH,
        ChiTietPhieuDatPhong.NgayDat,
        ChiTietPhieuDatPhong.NgayKetThuc,
        ChiTietPhieuDatPhong.GioDat,
        ChiTietPhieuDatPhong.GioKetThuc,
        ROW_NUMBER() OVER(PARTITION BY Phong.MaPhong ORDER BY ABS(DATEDIFF(day, ChiTietPhieuDatPhong.NgayDat, GETDATE()))) AS RowNumber
    FROM
        Phong
    INNER JOIN LoaiPhong ON Phong.MaLoaiPhong = LoaiPhong.MaLoaiPhong
    LEFT JOIN ChiTietPhieuDatPhong ON Phong.MaPhong = ChiTietPhieuDatPhong.MaPhong
    LEFT JOIN PhieuDatPhong ON ChiTietPhieuDatPhong.MaPDP = PhieuDatPhong.MaPDP
    LEFT JOIN KhachHang ON PhieuDatPhong.MaKH = KhachHang.MaKH
) AS Subquery
WHERE RowNumber = 1;
GO

CREATE FUNCTION dbo.GetPhongInfoByDate(@selectedDate DATETIME)
RETURNS TABLE
AS
RETURN
(
    SELECT
        Phong.MaPhong,
        PhieuDatPhong.MaPDP,
        LoaiPhong.TenLoaiPhong,
        CASE
            WHEN PhieuDatPhong.MaPDP IS NOT NULL AND CONVERT(DATETIME, NgayDat) + CAST(GioDat AS DATETIME) = @selectedDate THEN N'Phòng đã đặt'
            WHEN @selectedDate BETWEEN CONVERT(DATETIME, NgayDat) + CAST(GioDat AS DATETIME) 
			AND CONVERT(DATETIME, NgayKetThuc) + CAST(GioKetThuc AS DATETIME) THEN N'Phòng đang thuê'
            ELSE N'Phòng trống'
        END AS TinhTrang,
        ChiTietPhieuDatPhong.GioDat,
        ChiTietPhieuDatPhong.GioKetThuc,
        ChiTietPhieuDatPhong.SoNguoi,
        ChiTietPhieuDatPhong.NgayDat,
        ChiTietPhieuDatPhong.NgayKetThuc,
        Phong.Tang,
        Phong.GhiChu,
        ChiTietPhieuDatPhong.TinhTrang AS TinhTrangThanhToan,
        KhachHang.TenKH,
        KhachHang.MaKH,
        DATEDIFF(DAY, ChiTietPhieuDatPhong.NgayDat, ChiTietPhieuDatPhong.NgayKetThuc) AS SoNgay,
        DATEDIFF(HOUR, ChiTietPhieuDatPhong.GioDat,ChiTietPhieuDatPhong.GioKetThuc) AS SoGio
    FROM
        Phong
        LEFT JOIN ChiTietPhieuDatPhong ON Phong.MaPhong = ChiTietPhieuDatPhong.MaPhong
        LEFT JOIN PhieuDatPhong ON ChiTietPhieuDatPhong.MaPDP = PhieuDatPhong.MaPDP
        LEFT JOIN KhachHang ON PhieuDatPhong.MaKH = KhachHang.MaKH
        LEFT JOIN LoaiPhong ON Phong.MaLoaiPhong = LoaiPhong.MaLoaiPhong
    WHERE
    (
        CAST(ChiTietPhieuDatPhong.NgayDat AS DATETIME) + CAST(ChiTietPhieuDatPhong.GioDat AS DATETIME) <= @selectedDate
        AND CAST(ChiTietPhieuDatPhong.NgayKetThuc AS DATETIME) + CAST(ChiTietPhieuDatPhong.GioKetThuc AS DATETIME) >= @selectedDate
    )
    
    UNION
    
    SELECT
        Phong.MaPhong,
        NULL AS MaPDP,
        LoaiPhong.TenLoaiPhong,
        N'Phòng trống' AS TinhTrang,
        NULL AS GioDat,
        NULL AS GioKetThuc,
        NULL AS SoNguoi,
        NULL AS NgayDat,
        NULL AS NgayKetThuc,
        Phong.Tang,
        Phong.GhiChu,
        NULL AS TinhTrangThanhToan,
        NULL AS TenKH,
        NULL AS MaKH,
        NULL AS SoNgay,
        NULL AS SoGio
    FROM
        Phong
		LEFT JOIN LoaiPhong ON Phong.MaLoaiPhong = LoaiPhong.MaLoaiPhong
    WHERE
        Phong.MaPhong NOT IN (
            SELECT MaPhong FROM ChiTietPhieuDatPhong WHERE
    (
        CAST(ChiTietPhieuDatPhong.NgayDat AS DATETIME) + CAST(ChiTietPhieuDatPhong.GioDat AS DATETIME) <= @selectedDate
        AND CAST(ChiTietPhieuDatPhong.NgayKetThuc AS DATETIME) + CAST(ChiTietPhieuDatPhong.GioKetThuc AS DATETIME) >= @selectedDate
    )
        )
)

GO

create view ListDichVu as
	SELECT        LoaiDichVu.TenLoaiDV, DichVu.TenDV, DichVu.GiaTien,DichVu.MaDV
	FROM            DichVu INNER JOIN
							 LoaiDichVu ON DichVu.MaLoaiDV = LoaiDichVu.MaLoaiDV
GO

create view ListLoaiPhong as
SELECT        MaLoaiPhong, TenLoaiPhong, GiaTheoGio, GiaTheoNgay
FROM            LoaiPhong						 
GO

create view ThongTinHoaDon as
SELECT        HD.MaHD, HD.TongGiaTri, HD.NgayInHD, HD.MaPDP ,CTPDP.MaPhong, P.MaLoaiPhong, LP.GiaTheoGio, LP.GiaTheoNgay, 
				DV.TenDV, CTDV.MaDV, CTDV.SoLuongDV, DV.GiaTien, CTDV.TongGiaTri AS ThanhTien, CTPDP.SoNguoi,
				CTPDP.NgayDat, CTPDP.NgayKetThuc,
				   DATEDIFF(day, CTPDP.NgayDat, CTPDP.NgayKetThuc) AS SoNgay,
				 DATEDIFF(day, CTPDP.NgayDat, CTPDP.NgayKetThuc) * 24 + DATEDIFF(hour, CTPDP.GioDat, CTPDP.GioKetThuc) as SoGio
FROM            ChiTietDichVuPhieuDatPhong AS CTDV INNER JOIN
                         ChiTietPhieuDatPhong AS CTPDP ON CTDV.MaPDP = CTPDP.MaPDP AND CTDV.MaPhong = CTPDP.MaPhong INNER JOIN
                         HoaDon AS HD ON CTDV.MaPDP = HD.MaPDP INNER JOIN
                         DichVu AS DV ON CTDV.MaDV = DV.MaDV INNER JOIN
                         Phong AS P ON CTPDP.MaPhong = P.MaPhong INNER JOIN
                         LoaiPhong AS LP ON P.MaLoaiPhong = LP.MaLoaiPhong INNER JOIN
                         PhieuDatPhong as PDP ON CTPDP.MaPDP = PDP.MaPDP AND HD.MaPDP = PDP.MaPDP
GO
create view DichVuTheoPDP as
SELECT        ChiTietDichVuPhieuDatPhong.MaPDP, ChiTietDichVuPhieuDatPhong.MaPhong, ChiTietDichVuPhieuDatPhong.MaDV, DichVu.TenDV, ChiTietDichVuPhieuDatPhong.SoLuongDV, ChiTietDichVuPhieuDatPhong.TongGiaTri, 
                         DichVu.GiaTien
FROM            ChiTietDichVuPhieuDatPhong INNER JOIN
                         DichVu ON ChiTietDichVuPhieuDatPhong.MaDV = DichVu.MaDV
GO

CREATE TRIGGER UpdateTongGiaTri
ON ChiTietDichVuPhieuDatPhong
AFTER UPDATE
AS
BEGIN
    -- Kiểm tra xem các cột SoLuongDV hoặc TongGiaTri có được cập nhật không
    IF UPDATE(SoLuongDV) OR UPDATE(TongGiaTri)
    BEGIN
        -- Cập nhật tổng giá trị dựa trên số lượng dịch vụ và đơn giá
        UPDATE ChiTietDichVuPhieuDatPhong
        SET TongGiaTri = SoLuongDV * (select GiaTien from DichVu where MaDV = (select MaDV from inserted))  -- Giả sử GiaTien là đơn giá của dịch vụ
        FROM ChiTietDichVuPhieuDatPhong
		where MaDV =(select MaDV from inserted) and MaPDP = (select MaPDP from inserted) and MaPhong = (select MaPhong from inserted);
    END
END;
GO

CREATE PROCEDURE UpdatePhieuDatPhong
    @MaPDP char(10),
    @MaKH char(10),
    @MaNV char(10),
    @TongGiaTri float,
    @NgayTao DATETIME,
    @SoNguoi INT,
    @TinhTrang NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE PhieuDatPhong
    SET MaKH = @MaKH,
        MaNV = @MaNV,
        NgayTao = @NgayTao
    WHERE MaPDP = @MaPDP;
END;
GO

