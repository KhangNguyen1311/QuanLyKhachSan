using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLKS;
namespace DAL_QLKS
{
    public class DAL_ChiTietPhieuDatPhong
    {
       public List<ChiTietPhieuDatPhong> GetAll()
       {
            using(QL_KhachSanEntities db = new QL_KhachSanEntities())
            {
                return db.ChiTietPhieuDatPhongs.ToList();
            }
       }

        public List<ChiTietPhieuDatPhong> Selectall()
        {
            using (QL_KhachSanEntities ctpdp = new QL_KhachSanEntities())
            {
                var ctpdppp = ctpdp.ChiTietPhieuDatPhongs.ToList();
                return ctpdppp;
            }
        }
        public void Update(ChiTietPhieuDatPhong chitietphieuDatPhong)
        {
            using (QL_KhachSanEntities ctpdp = new QL_KhachSanEntities())
            {
                ctpdp.ChiTietPhieuDatPhongs.Attach(chitietphieuDatPhong);
                ctpdp.Entry(chitietphieuDatPhong).State = System.Data.EntityState.Modified;
                ctpdp.SaveChanges();
            }
        }
        public void Insert(ChiTietPhieuDatPhong chitietphieuDatPhong)
        {
            using (QL_KhachSanEntities lp = new QL_KhachSanEntities())
            {
                lp.InsertChiTietPhieuDatPhong(chitietphieuDatPhong.MaPDP, chitietphieuDatPhong.MaPhong, chitietphieuDatPhong.NgayDat, chitietphieuDatPhong.NgayKetThuc, chitietphieuDatPhong.GioDat, chitietphieuDatPhong.GioKetThuc,chitietphieuDatPhong.SoNguoi);
            }
        }
        public void UpdateChiTietPDP(ChiTietPhieuDatPhong chiTietPhieudatPhong)
        {
            using (var db = new QL_KhachSanEntities())
            {
                db.ChiTietPhieuDatPhongs.Attach(chiTietPhieudatPhong);
                db.Entry(chiTietPhieudatPhong).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public List<ChiTietPhieuDatPhong> getAll()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<ChiTietPhieuDatPhong> chitTietPDP = db.ChiTietPhieuDatPhongs.ToList();
                return chitTietPDP;
            }
        }
        public void Delete(ChiTietPhieuDatPhong ctpdp)
        {
            using (var db = new QL_KhachSanEntities())
            {
                db.ChiTietPhieuDatPhongs.Attach(ctpdp);
                db.ChiTietPhieuDatPhongs.Remove(ctpdp);
                db.SaveChanges();
            }
        }
    }
}
