using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_DichVu
    {
        public List<ListDichVu> danhSachDichVu()
        {
            using (var db = new QL_KhachSanEntities())
            {
                List<ListDichVu> danhsachdichvu = db.ListDichVus.ToList();
                return danhsachdichvu;
            }
        }
        public int Insert(DichVu dichVu)
        {
            using (var conn = new QL_KhachSanEntities())
            {
                return conn.InsertDichVu(dichVu.MaLoaiDV, dichVu.TenDV, dichVu.GiaTien);
            }
        }
        public List<DichVu> getall()
        {
            using (QL_KhachSanEntities db = new QL_KhachSanEntities())
            {
                return db.DichVus.ToList();
            }
        }

        public List<DichVu> GetAll()
        {
            using (var conn = new QL_KhachSanEntities())
            {
                var laydulieu = conn.DichVus.ToList();
                return laydulieu;
            }
        }

        public void Update(DichVu dichVu)
        {
            using (var conn = new QL_KhachSanEntities())
            {
                conn.DichVus.Attach(dichVu);
                conn.Entry(dichVu).State = System.Data.EntityState.Modified;
                conn.SaveChanges();
            }
        }

        public List<DichVu> TimKiem()
        {
            using (var conn = new QL_KhachSanEntities())
            {
                var timkiem = conn.DichVus.ToList();
                return timkiem;
            }
        }

        public void Delete(string maDV)
        {
            using (var conn = new QL_KhachSanEntities())
            {
                var dichVu = conn.DichVus.Where(d => d.MaDV == maDV).FirstOrDefault(); // Fetch the DichVu entity based on maDV
                if (dichVu != null)
                {
                    conn.DichVus.Remove(dichVu); // Remove the fetched DichVu entity
                    conn.SaveChanges(); // Save changes to the database
                }
            }
        }



    }
}
