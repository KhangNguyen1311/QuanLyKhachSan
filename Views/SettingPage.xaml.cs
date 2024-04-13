using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuAn_QuanLiKhachSan.Views
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        static DTO_QLKS.NhanVien nhanVien= new DTO_QLKS.NhanVien();
        public SettingPage(DTO_QLKS.NhanVien nv)
        {
            InitializeComponent();
            nhanVien = nv;
        }
        public void DisableTextBox()
        {
            txt_TenNV.IsEnabled = false;
            txt_MaNV.IsEnabled = false;
            txt_Email.IsEnabled = false;
            txt_NgaySinh.IsEnabled = false;
            txt_DiaChi.IsEnabled = false;
            txt_phai.IsEnabled = false;
            txt_CCCD.IsEnabled = false;
            txt_TenNV.IsEnabled = false;
            txt_SDT.IsEnabled = false;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DisableTextBox();
            txt_TenNV.Text = nhanVien.TenNV;
            txt_MaNV.Text = nhanVien.MaNV;
            txt_NgaySinh.Text = nhanVien.NgaySinh;
            txt_CCCD.Text = nhanVien.CCCD;
            txt_ChuVu.Text = nhanVien.VaiTro;
            txt_DiaChi.Text = nhanVien.DiaChi;
            txt_Email.Text = nhanVien.Email;
            txt_SDT.Text = nhanVien.SDT;
            if (nhanVien.Phai == false)
            {
                txt_phai.Text = "Nam";
            }
            else if(nhanVien.Phai==true)
            {
                txt_phai.Text = "Nữ";
            }
        }
    }
}
