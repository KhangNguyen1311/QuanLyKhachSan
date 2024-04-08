using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_Phong
    {
        static DAL_Phong Dal_PHONG = new DAL_Phong();
        static DAL_Phong dAL_Phong = new DAL_Phong();
        public List<Phong> GetAll()
        {
            return dAL_Phong.GetAll();
        }
        public List<DanhSachThongTinPhong> SelectAllPhong()
        {
            return Dal_PHONG.getValuePhong().ToList();
        }
        public List<DanhSachThongTinPhong> SelectPhongDon()
        {
            return Dal_PHONG.getValuePhong().Where(c => c.TenLoaiPhong.Equals("Phòng đơn")).ToList();
        }
        public List<DanhSachThongTinPhong> SelectPhongDoi()
        {
            return Dal_PHONG.getValuePhong().Where(c => c.TenLoaiPhong.Equals("Phòng đôi")).ToList();
        }
        public List<DanhSachThongTinPhong> SelectPhongFamily()
        {
            return Dal_PHONG.getValuePhong().Where(c => c.TenLoaiPhong.Equals("Phòng gia đình")).ToList();
        }
        public void UpdatePhong(Phong thongTinPhong)
        {
            Dal_PHONG.UpdatePhong(thongTinPhong);
        }

        public List<Phong> SelectPhong()
        {
            return Dal_PHONG.getPhong().ToList();
        }

        public int Insert(Phong Phong)
        {
            return Dal_PHONG.InsertPhong(Phong);
        }


        public List<DanhSachThongTinPhong> GetPhongsByDate(DateTime selectedDate)
        {
            // Gọi phương thức từ lớp DAL để lấy dữ liệu từ function
            var danhSachPhongs = Dal_PHONG.GetPhongInfoByDate(selectedDate);
            return danhSachPhongs;
        }
    }
}
