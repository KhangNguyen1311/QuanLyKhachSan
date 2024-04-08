using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_DatPhong
    {
        static DAL_DatPhong dAL_DatPhong = new DAL_DatPhong();
        public List<PhieuDatPhong> Selectall()
        {
            return dAL_DatPhong.Selectall();
        }
        public void xoa(PhieuDatPhong pdp)
        {
            dAL_DatPhong.Delete(pdp);
        }
        public void Update(PhieuDatPhong pdp)
        {
            dAL_DatPhong.Update(pdp);
        }
    }
}
