using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;
namespace BUS_QLKS
{
    public class BUS_DichVu
    {
        static DAL_DichVu dAL_DichVu = new DAL_DichVu();
        static DAL_DichVu dal_dichVu = new DAL_DichVu();
        public List<ListDichVu> SelectDichVu()
        {
            return dal_dichVu.danhSachDichVu().ToList();
        }
        public List<DichVu> getall()
        {
            return dAL_DichVu.getall();
        }
        public void Insert(DichVu dichVu)
        {
            int temp = dal_dichVu.Insert(dichVu);
        }
        public List<DichVu> GetAll()
        {
            return dal_dichVu.GetAll();
        }
        public List<DichVu> TimKiem()
        {
            return dal_dichVu.TimKiem();
        }

        public void Update(DichVu dichVu)
        {
            dal_dichVu.Update(dichVu);
        }

        public void Delete(string maDV)
        {
            // Assuming dAL_DichVu.Delete method accepts the MaDV for deletion
            dal_dichVu.Delete(maDV);
        }
    }
}
