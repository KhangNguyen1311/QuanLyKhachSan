using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_ChiTietDichVuPhieuDatPhong
    {
        public List<ChiTietDichVuPhieuDatPhong> GetAll()
        {
            using(QL_KhachSanEntities db = new QL_KhachSanEntities())
            {
                return db.ChiTietDichVuPhieuDatPhongs.ToList();
            }
        }
        public int InsertChiTietDVPDP(ChiTietDichVuPhieuDatPhong dichVuDaChon)
        {
            using (var db = new QL_KhachSanEntities())
            {
                return db.InsertChiTietDVPhieuDatPhong(dichVuDaChon.MaPDP, dichVuDaChon.MaPhong, dichVuDaChon.MaDV, dichVuDaChon.SoLuongDV);
            }
        }
        public List<DichVuTheoPDP> danhSachDichVuDaChon()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<DichVuTheoPDP> danhsachdichvu = db.DichVuTheoPDPs.ToList();
                return danhsachdichvu;
            }
        }

        public List<ChiTietDichVuPhieuDatPhong> selectDichVu()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<ChiTietDichVuPhieuDatPhong> danhsachdichvu = db.ChiTietDichVuPhieuDatPhongs.ToList();
                return danhsachdichvu;
            }
        }
        public void UpdateDichVuTheoPDP(ChiTietDichVuPhieuDatPhong dichvu)
        {
            using (var db = new QL_KhachSanEntities())
            {
                db.ChiTietDichVuPhieuDatPhongs.Attach(dichvu);
                db.Entry(dichvu).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void deleteDichVuTheoPDP(ChiTietDichVuPhieuDatPhong dichvu)
        {
            using (var db = new QL_KhachSanEntities())
            {
                db.ChiTietDichVuPhieuDatPhongs.Attach(dichvu);
                db.ChiTietDichVuPhieuDatPhongs.Remove(dichvu);
                db.SaveChanges();
            }
        }
    }
}
