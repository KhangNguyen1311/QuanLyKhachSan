using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_LoaiPhong
    {
        public List<ListLoaiPhong> getValueLoaiPhong()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<ListLoaiPhong> loaiPhongs = db.ListLoaiPhongs.ToList();
                return loaiPhongs;
            }
        }
        public List<LoaiPhong> Selectall()
        {
            using (QL_KhachSanEntities lp = new QL_KhachSanEntities())
            {
                var loaiphong = lp.LoaiPhongs.ToList();
                return loaiphong;
            }
        }
        public void Insert(LoaiPhong loaiPhong)
        {
            using (QL_KhachSanEntities lp = new QL_KhachSanEntities())
            {
                lp.InsertLoaiPhong(loaiPhong.TenLoaiPhong, loaiPhong.GiaTheoGio, loaiPhong.GiaTheoNgay);
            }
        }
        public void Delete(LoaiPhong loaiPhong)
        {
            using (QL_KhachSanEntities lp = new QL_KhachSanEntities())
            {
                lp.LoaiPhongs.Attach(loaiPhong);
                lp.LoaiPhongs.Remove(loaiPhong);
                lp.SaveChanges();
            }
        }


        public void Update(LoaiPhong loaiPhong)
        {
            using (QL_KhachSanEntities lp = new QL_KhachSanEntities())
            {
                lp.LoaiPhongs.Attach(loaiPhong);
                lp.Entry(loaiPhong).State = System.Data.EntityState.Modified;
                lp.SaveChanges();
            }
        }

    }
}
