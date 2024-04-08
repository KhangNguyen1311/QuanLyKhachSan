using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLKS
{
    public class DAL_LoaiDichVu
    {
        public int Insert(LoaiDichVu loaiDichVu)
        {
            using (var conn = new QL_KhachSanEntities())
            {
                return conn.InsertLoaiDichVu(loaiDichVu.TenLoaiDV);
            }
        }

        public List<LoaiDichVu> GetAll()
        {
            using (var conn = new QL_KhachSanEntities())
            {
                var laydulieu = conn.LoaiDichVus.ToList();
                return laydulieu;
            }
        }
    }
}
