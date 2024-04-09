USE QL_KhachSan
GO
--Thêm nhanvien
EXEC InsertNhanVien N'Huỳnh Ngọc Luận', '09*********','01/01/2000','123 Nguyễn Văn A','',N'Quản lý','ngocluan@gmail.com','Nam','ngocluan123','';
GO
EXEC InsertNhanVien N'Nguyễn Hữu Khang', '09*********','02/02/2000','123 Nguyễn Văn A','',N'Nhân viên','huukhang@gmail.com','Nam','huukhang123','';
GO
EXEC InsertNhanVien N'Nguyễn Văn Đáng', '09*********','02/02/2000','123 Nguyễn Văn A','',N'Nhân viên','vandang@gmail.com','Nam','vandang123','';
GO
EXEC InsertNhanVien N'Hứa Văn Gia Bảo', '09*********','02/02/2000','123 Nguyễn Văn A','',N'Nhân viên','giabao@gmail.com','Nam','giabao123','';
GO
EXEC InsertNhanVien N'Trần Quang Anh', '09*********','02/02/2000','123 Nguyễn Văn A','',N'Nhân viên','quanganh@gmail.com','Nam','quanganh123','';
GO
--Thêm khachhang
EXEC InsertKhachHang N'Vũ Minh Nghĩa', '09*********','02/02/2000','123 Nguyễn Văn A', 'Nam','',N'Việt Nam'
GO

EXEC InsertKhachHang N'Hoàng Trung Kiên', '09*********','02/02/2000','123 Nguyễn Văn A', 'Nam','',N'Việt Nam'
GO

EXEC InsertKhachHang N'Bảy chọ', '09*********','05/02/1985','123 Nguyễn Văn A', 'Nam','',N'Bồ Đào Nha gốc Hàn'
GO

EXEC InsertKhachHang N'Dương Thanh Phương', '09*********','02/02/2000','123 Nguyễn Văn A', 'Nam','',N'Việt Nam'
GO

EXEC InsertKhachHang N'Quang Mai', '09*********','02/02/2000','123 Nguyễn Văn A', 'Nam','',N'Việt Nam'
GO
--Thêm loaiphong
EXEC InsertLoaiPhong N'Phòng đơn', 300000,3000000
GO
EXEC InsertLoaiPhong N'Phòng đôi', 550000,6000000
GO
EXEC InsertLoaiPhong N'Phòng gia đình', 850000,10000000
GO
--Thêm phòng
EXEC InsertPhong N'LP0001    ',1
GO
EXEC InsertPhong N'LP0002    ',1
GO
EXEC InsertPhong N'LP0003    ',1
GO
EXEC InsertPhong N'LP0001    ',2
GO
EXEC InsertPhong N'LP0002    ',2
GO
EXEC InsertPhong N'LP0003    ',2
GO
EXEC InsertPhong N'LP0001    ',3
GO
EXEC InsertPhong N'LP0002    ',3
GO
EXEC InsertPhong N'LP0003    ',3
GO
--Thêm tiện nghi loại phòng
EXEC InsertTienNghiPhong 'LP0001    ', N'Giường đơn', 1
GO
EXEC InsertTienNghiPhong 'LP0001    ', N'Máy lạnh', 1
GO
EXEC InsertTienNghiPhong 'LP0001    ', N'Tủ quần áo', 1
GO
EXEC InsertTienNghiPhong 'LP0002    ', N'Giường đôi', 1
GO
EXEC InsertTienNghiPhong 'LP0002    ', N'Máy lạnh', 1
GO
EXEC InsertTienNghiPhong 'LP0002    ', N'Tủ quần áo', 1
GO
EXEC InsertTienNghiPhong 'LP0003   ', N'Giường đôi', 2
GO
EXEC InsertTienNghiPhong 'LP0003    ', N'Máy lạnh', 1
GO
EXEC InsertTienNghiPhong 'LP0003    ', N'Tủ quần áo', 1
GO
--Thêm loại dịch vụ
EXEC InsertLoaiDichVu N'Ăn uống'
GO
EXEC InsertLoaiDichVu N'Đưa đón'
GO
EXEC InsertLoaiDichVu N'Vệ sinh'
GO
--Thêm dichvu
EXEC InsertDichVu N'LDV0001   ',N'Buffet',100000
GO
EXEC InsertDichVu N'LDV0002   ',N'Đưa đón sân bay',50000
GO
EXEC InsertDichVu N'LDV0003   ',N'Giặt ủi',30000
GO
--Thêm phieudatphong
EXEC InsertPhieuDatPhong 'KH0001','NV0002'
GO

EXEC InsertPhieuDatPhong 'KH0001','NV0003'
GO
--Thêm chitietphieudatphong
EXEC InsertChiTietPhieuDatPhong 'PDP0001','P1001','2024-03-12','2024-03-15','12:00:00','12:00:00',2
GO

EXEC InsertChiTietPhieuDatPhong 'PDP0002','P1001','2024-03-12','2024-03-15','12:00:00','12:00:00',2
GO
--Thêm chitietdichvupdp
EXEC InsertChiTietDVPhieuDatPhong 'PDP0001','P1001','DV0001',1
GO
