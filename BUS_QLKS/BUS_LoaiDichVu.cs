using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_LoaiDichVu
    {
        static DAL_LoaiDichVu dAL_LoaiDichVu = new DAL_LoaiDichVu();

        public void Insert(LoaiDichVu loaiDichVu)
        {
            int temp = dAL_LoaiDichVu.Insert(loaiDichVu);
        }

        public List<LoaiDichVu> GetAll()
        {
            return dAL_LoaiDichVu.GetAll();
        }
    }
}
