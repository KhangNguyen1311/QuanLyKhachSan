using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_PhieuDatPhong
    {
        static DAL_PhieuDatPhong dAL_PhieuDatPhong = new DAL_PhieuDatPhong();
        static DAL_PhieuDatPhong dal_phieudp = new DAL_PhieuDatPhong();
        public List<PhieuDatPhong> SelectAll()
        {
            return dal_phieudp.SelectAll();
        }
        public void Update(PhieuDatPhong pdp)
        {
            dal_phieudp.Update(pdp);
        }
        public void Insert(PhieuDatPhong pdp)
        {
            dal_phieudp.Insert(pdp);
        }
        public List<PhieuDatPhong> GetAll()
        {
            return dAL_PhieuDatPhong.GetAll();
        }
      
        static DAL_PhieuDatPhong dal_PHIEUDATPHONG = new DAL_PhieuDatPhong();
        public void UpdatePDPhong(PhieuDatPhong phieudatPhong)
        {
            dal_PHIEUDATPHONG.UpdatePDP(phieudatPhong);
        }
        public List<PhieuDatPhong> SelectPDPhong()
        {
            return dal_PHIEUDATPHONG.getPDP().ToList();
        }
    }
}
