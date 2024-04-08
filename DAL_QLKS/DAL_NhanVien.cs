using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_NhanVien
    {
        public List<NhanVien> GetAll()
        {
            using(var db = new QL_KhachSanEntities())
            {
                return db.NhanViens.ToList();
            }
        }
        public List<DanhSachNhanVien> LayTatCa()
        {
            using (QL_KhachSanEntities csdl = new QL_KhachSanEntities())
            {
                return csdl.DanhSachNhanViens.ToList();
            }
        }

        public void Delete(NhanVien nhanVien)
        {
            using (var QL_KHACHSANEntities = new QL_KhachSanEntities())
            {
                QL_KHACHSANEntities.NhanViens.Attach(nhanVien);
                QL_KHACHSANEntities.NhanViens.Remove(nhanVien);
                QL_KHACHSANEntities.SaveChanges();
            }
        }
        public List<NhanVien> SelectAll()
        {
            using (var QL_KHACHSANEntities = new QL_KhachSanEntities())
            {
                var nhanviens = QL_KHACHSANEntities.NhanViens.ToList();
                return nhanviens;
            }
        }

        public int Insert(string TenNV, string SDT, string NgaySinh, string DiaChi, string HinhAnh, string VaiTro, string Email, string Phai, string MatKhau, string CCCD)
        {
            using (var QL_KHACHSANEntities = new QL_KhachSanEntities())
            {
                return QL_KHACHSANEntities.InsertNhanVien(TenNV, SDT, NgaySinh, DiaChi, HinhAnh, VaiTro, Email, Phai, MatKhau, CCCD);
            }
        }
        public void Update(NhanVien nhanVien)
        {
            using (var QL_KHACHSANEntities = new QL_KhachSanEntities())
            {
                QL_KHACHSANEntities.NhanViens.Attach(nhanVien);
                QL_KHACHSANEntities.Entry(nhanVien).State = System.Data.EntityState.Modified;
                QL_KHACHSANEntities.SaveChanges();
            }
        }
        public List<NhanVien> Selectall()
        {
            using (QL_KhachSanEntities nv = new QL_KhachSanEntities())
            {
                var nhanv = nv.NhanViens.ToList();
                return nhanv;
            }
        }
    }
}
