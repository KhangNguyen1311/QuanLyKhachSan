using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_KhachHang
    {
        static DAL_KhachHang dAL_KhachHang = new DAL_KhachHang();

        public List<KhachHang> GetAll()
        {
            return dAL_KhachHang.GetAll();
        }

        public void Update(KhachHang khachHang)
        {
            dAL_KhachHang.Update(khachHang);
        }
        static DAL_KhachHang DAL_Khachhang = new DAL_KhachHang();
        public List<KhachHang> SelectAll()
        {
            return DAL_Khachhang.SelectAll();

        }
        public void Insert(string TenKH, string SDT, string NgaySinh, string DiaChi, string Phai, string MaCMT, string QuocTich)
        {
            int temp = DAL_Khachhang.Insert(TenKH, SDT, NgaySinh, DiaChi, Phai, MaCMT, QuocTich);
        }
    }
}
