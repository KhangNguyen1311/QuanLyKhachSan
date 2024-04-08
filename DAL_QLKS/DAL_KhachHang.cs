using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_KhachHang
    {
        public List<KhachHang> GetAll()
        {
            using (var conn = new QL_KhachSanEntities())
            {
                var getall = conn.KhachHangs.ToList();
                return getall;
            }
        }
        public List<KhachHang> SelectAll()
        {
            using (QL_KhachSanEntities kh = new QL_KhachSanEntities())
            {
                var ct = kh.KhachHangs.ToList();
                return ct;
            }
        }
        public void Update(KhachHang khachHang)
        {
            using (var conn = new QL_KhachSanEntities())
            {
                conn.KhachHangs.Attach(khachHang);
                conn.Entry(khachHang).State = System.Data.EntityState.Modified;
                conn.SaveChanges();
            }
        }
        public int Insert(string TenKH, string SDT, string NgaySinh, string DiaChi, string Phai, string MaCMT, string QuocTich)
        {
            using (var QL_KHACHSANEntities = new QL_KhachSanEntities())
            {
                return QL_KHACHSANEntities.InsertKhachHang(TenKH, SDT, NgaySinh, DiaChi, Phai, MaCMT, QuocTich);
            }
        }
    }
}
