using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_LoaiPhong
    {

        static DAL_LoaiPhong dal_LoaiPhong = new DAL_LoaiPhong();
        public List<ListLoaiPhong> SelectLoaiPhong()
        {
            return dal_LoaiPhong.getValueLoaiPhong().ToList();
        }

        public List<LoaiPhong> SelectAll()
        {
            return dal_LoaiPhong.Selectall();

        }
        public void Insert(LoaiPhong loaiPhong)
        {
            dal_LoaiPhong.Insert(loaiPhong);
        }
        public void Xoa(LoaiPhong lp)
        {
            dal_LoaiPhong.Delete(lp);
        }
        public void Update(LoaiPhong loaiPhong)
        {
            dal_LoaiPhong.Update(loaiPhong);
        }
    }
}
