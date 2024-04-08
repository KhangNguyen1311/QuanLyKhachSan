using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_PhieuDatPhong
    {
        public List<PhieuDatPhong> GetAll()
        {
            using(QL_KhachSanEntities db = new QL_KhachSanEntities())
            {
                return db.PhieuDatPhongs.ToList();
            }
        }
        public void UpdatePDP(PhieuDatPhong phieudatPhong)
        {
            using (var db = new QL_KhachSanEntities())
            {
                db.PhieuDatPhongs.Attach(phieudatPhong);
                db.Entry(phieudatPhong).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public List<PhieuDatPhong> SelectAll()
        {
            using (QL_KhachSanEntities Chitiet = new QL_KhachSanEntities())
            {
                var ct = Chitiet.PhieuDatPhongs.ToList();
                return ct;
            }
        }
        public void Insert(PhieuDatPhong phieu)
        {
            using (QL_KhachSanEntities Chitiet = new QL_KhachSanEntities())
            {
                Chitiet.InsertPhieuDatPhong(phieu.MaKH, phieu.MaNV);

            }
        }
        public void Update(PhieuDatPhong phieuDatPhong)
        {
            using (QL_KhachSanEntities pdp = new QL_KhachSanEntities())
            {
                pdp.PhieuDatPhongs.Attach(phieuDatPhong);
                pdp.Entry(phieuDatPhong).State = System.Data.EntityState.Modified;
                pdp.SaveChanges();
            }
        }
        public List<PhieuDatPhong> getPDP()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<PhieuDatPhong> PDP = db.PhieuDatPhongs.ToList();
                return PDP;
            }
        }
        
        
    }
}
