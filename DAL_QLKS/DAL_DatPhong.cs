using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_DatPhong
    {
        public List<PhieuDatPhong> Selectall()
        {
            using (QL_KhachSanEntities dp = new QL_KhachSanEntities())
            {
                var datphong = dp.PhieuDatPhongs.ToList();
                return datphong;
            }
        }
        public void Delete(PhieuDatPhong datPhong)
        {
            using (QL_KhachSanEntities xoa = new QL_KhachSanEntities())
            {
                xoa.PhieuDatPhongs.Attach(datPhong);
                xoa.PhieuDatPhongs.Remove(datPhong);
                xoa.SaveChanges();
            }
        }

        public void Update(PhieuDatPhong datPhong)
        {
            using (QL_KhachSanEntities pdp = new QL_KhachSanEntities())
            {
                pdp.PhieuDatPhongs.Attach(datPhong);
                pdp.Entry(datPhong).State = System.Data.EntityState.Modified;
                pdp.SaveChanges();
            }
        }
    }
}
