using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_QLKS
{
    public class BUS_NhanVien
    {
        static DAL_NhanVien dAL_NhanVien = new DAL_NhanVien();
        public bool CheckLogin(string email,string password)
        {
            NhanVien nhanvien = dAL_NhanVien.GetAll().Where(c=>c.Email.Equals(email)&&c.MatKhau.Equals(password)).FirstOrDefault();
            if (nhanvien != null)
            {
                return true;
            }
            return false;
        }
        static DAL_NhanVien DAL_NhanVien = new DAL_NhanVien();
        public List<NhanVien> SelectAll()
        {
            return DAL_NhanVien.Selectall();

        }
        public List<DanhSachNhanVien> laytatca()
        {
            return dAL_NhanVien.LayTatCa();
        }



        public NhanVien GetById(string maNV)
        {
            return dAL_NhanVien.SelectAll().Where(c => c.MaNV.Contains(maNV)).FirstOrDefault();
        }
        public void Insert(string TenNV, string SDT, string NgaySinh, string DiaChi, string HinhAnh, string VaiTro, string Email, string Phai, string MatKhau, string CCCD)
        {
            int temp = dAL_NhanVien.Insert(TenNV, SDT, NgaySinh, DiaChi, HinhAnh, VaiTro, Email, Phai, MatKhau, CCCD);
        }
        public void Update(NhanVien NhanVien)
        {
            dAL_NhanVien.Update(NhanVien);
        }
        public NhanVien GetNhanVienByEmailAndPass(string email,string password)
        {
            return dAL_NhanVien.GetAll().Where(c => c.Email.Equals(email) && c.MatKhau.Equals(password)).FirstOrDefault();
        }

    }
}
