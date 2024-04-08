using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_HoaDon
    {
        public List<HoaDon> GetAll()
        {
            using(QL_KhachSanEntities db = new QL_KhachSanEntities())
            {
                return db.HoaDons.ToList();
            }
        }
        public List<ViewHoaDonNhanVienPhieuDatPhong> LayHet()
        {
            using (var csdl = new QL_KhachSanEntities())
            {
                var lay = csdl.ViewHoaDonNhanVienPhieuDatPhongs.ToList();
                return lay;
            }
        }
        public List<ThongTinHoaDon> selectHD()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<ThongTinHoaDon> loaiPhongs = db.ThongTinHoaDons.ToList();
                return loaiPhongs;
            }
        }
        public void Update(HoaDon hoadon)
        {
            using (QL_KhachSanEntities db = new QL_KhachSanEntities())
            {
                db.HoaDons.Attach(hoadon);
                db.Entry(hoadon).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
