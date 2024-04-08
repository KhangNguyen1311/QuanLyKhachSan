using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_HoaDon
    {
        static DAL_HoaDon dAL_HoaDon = new DAL_HoaDon();
        public List<HoaDon> GetAll()
        {
            return dAL_HoaDon.GetAll();
        }
        static DAL_HoaDon dal_HoaDon = new DAL_HoaDon();
        public List<ThongTinHoaDon> SelectChiTietHD()
        {
            return dal_HoaDon.selectHD().ToList();
        }
        public List<ViewHoaDonNhanVienPhieuDatPhong> LayHet()
        {
            return dal_HoaDon.LayHet();
        }
        public void Update(HoaDon hoaDon)
        {
            dAL_HoaDon.Update(hoaDon);
        }
    }
}
