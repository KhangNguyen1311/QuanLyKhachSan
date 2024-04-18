using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_ChiTietPhieuDatPhong
    {
        static DAL_ChiTietPhieuDatPhong dAL_ChiTietPhieuDatPhong = new DAL_ChiTietPhieuDatPhong();
        static DAL_PhieuDatPhong dAL_PhieuDatPhong = new DAL_PhieuDatPhong();
        public double GetDoanhThuPDP()
        {
            List<ChiTietPhieuDatPhong> chiTietPhieuDatPhongs = dAL_ChiTietPhieuDatPhong.GetAll();
            return chiTietPhieuDatPhongs.Sum(c => c.TongGiaTri);
        }
        static DAL_ChiTietPhieuDatPhong dAL_ChitietPhieuDatPhong = new DAL_ChiTietPhieuDatPhong();
        public List<ChiTietPhieuDatPhong> SelectAll()
        {
            return dAL_ChitietPhieuDatPhong.Selectall();

        }
        public void Update(ChiTietPhieuDatPhong ctpdp)
        {
            dAL_ChitietPhieuDatPhong.Update(ctpdp);
        }
        public void Insert(ChiTietPhieuDatPhong chiTietPhieuDatPhong)
        {
            dAL_ChitietPhieuDatPhong.Insert(chiTietPhieuDatPhong);
        }

        public void UpdatePDPhong(ChiTietPhieuDatPhong chitTietPhieudatPhong)
        {
            dAL_ChitietPhieuDatPhong.UpdateChiTietPDP(chitTietPhieudatPhong);
        }
        public double GetTongDoanhThuByThangNam(int thang, int nam)
        {
            double tongGiaTri = (from pdp in dAL_PhieuDatPhong.GetAll()
                                 join cdv in dAL_ChitietPhieuDatPhong.GetAll() on pdp.MaPDP equals cdv.MaPDP
                                 where pdp.NgayTao.Month == thang && pdp.NgayTao.Year == nam
                                 select cdv.TongGiaTri).Sum();
            return tongGiaTri;
        }
        public void Delete(ChiTietPhieuDatPhong ctpdp)
        {
            dAL_ChiTietPhieuDatPhong.Delete(ctpdp);
        }
    }
}
