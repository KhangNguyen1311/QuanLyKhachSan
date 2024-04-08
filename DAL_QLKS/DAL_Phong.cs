using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_Phong
    {
        public List<DanhSachThongTinPhong> getValuePhong()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<DanhSachThongTinPhong> phongs = db.DanhSachThongTinPhongs.ToList();
                return phongs;
            }
        }

        public void UpdatePhong(Phong thongTinPhong)
        {
            using (var db = new QL_KhachSanEntities())
            {
                db.Phongs.Attach(thongTinPhong);
                db.Entry(thongTinPhong).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public List<Phong> GetAll()
        {
            using (QL_KhachSanEntities db = new QL_KhachSanEntities())
            {
                return db.Phongs.ToList();
            }
        }
        public int InsertPhong(Phong Phong)
        {
            using (var db = new QL_KhachSanEntities())
            {
                return db.InsertPhong(Phong.MaLoaiPhong, Phong.Tang);
            }
        }
        public List<Phong> getPhong()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<Phong> phongs = db.Phongs.ToList();
                return phongs;
            }
        }

        public List<DanhSachThongTinPhong> GetPhongInfoByDate(DateTime selectedDate)
        {
            using (var db = new QL_KhachSanEntities())
            {
                // Gọi function từ cơ sở dữ liệu bằng cách sử dụng context.Database.SqlQuery hoặc context.Database.ExecuteSqlCommand
                var danhSachPhongs = db.Database.SqlQuery<DanhSachThongTinPhong>("SELECT * FROM dbo.GetPhongInfoByDate(@selectedDate)", new SqlParameter("selectedDate", selectedDate)).ToList();
                return danhSachPhongs;
            }
        }

    }
}
