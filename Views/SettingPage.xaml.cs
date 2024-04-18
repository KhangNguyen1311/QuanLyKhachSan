using BUS_QLKS;
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
using System.Windows.Shapes;

namespace DuAn_QuanLiKhachSan.Views
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Window
    {
        static DTO_QLKS.NhanVien nhanVien = new DTO_QLKS.NhanVien();
        public SettingPage(DTO_QLKS.NhanVien nv)
        {
            InitializeComponent();
            nhanVien = nv;
        }
        public void DisableTextBox()
        {
            txt_TenNV.IsReadOnly = true;
            txt_MaNV.IsReadOnly = true;
            txt_Email.IsReadOnly =  true;
            txt_NgaySinh.IsEnabled = false;
            txt_DiaChi.IsReadOnly = true;
            txt_phai.IsReadOnly = true;
            txt_CCCD.IsReadOnly = true;
            txt_TenNV.IsReadOnly = true;
            txt_SDT.IsReadOnly = true;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DisableTextBox();
            txt_TenNV.Text = nhanVien.TenNV;
            txt_MaNV.Text = nhanVien.MaNV;
            txt_NgaySinh.Text = nhanVien.NgaySinh;
            txt_CCCD.Text = nhanVien.CCCD;
            txt_DiaChi.Text = nhanVien.DiaChi;
            txt_Email.Text = nhanVien.Email;
            txt_SDT.Text = nhanVien.SDT;
            if (nhanVien.Phai == false)
            {
                txt_phai.Text = "Nam";
            }
            else if (nhanVien.Phai == true)
            {
                txt_phai.Text = "Nữ";
            }
            BitmapImage bitmapImage = new BitmapImage(new Uri(nhanVien.HinhAnh));
            img.ImageSource = bitmapImage;
        }

        private void bt_CapNhatMatKhau_Click(object sender, RoutedEventArgs e)
        {
            if (txt_password.Text.Length == 0)
            {
                var tb = new DialogCustoms("Vui lòng điền mật khẩu cũ!", "Thông báo!", DialogCustoms.OK);
                tb.ShowDialog();
            }
            else if(txt_newPassword.Text.Length == 0)
            {
                var tb = new DialogCustoms("Vui lòng điền mật khẩu mới!", "Thông báo!", DialogCustoms.OK);
                tb.ShowDialog();
            }
            else
            {

                string matkhaucu = txt_password.Text;
                string matkhaumoi = txt_newPassword.Text;
                if(nhanVien.MatKhau.Equals(matkhaucu) == false)
                {
                    var tb = new DialogCustoms("Không khớp với mật khẩu cũ! Vui lòng điền lại!", "Thông báo!", DialogCustoms.OK);
                    tb.ShowDialog();
                }
                else if(matkhaucu.Equals(matkhaumoi))
                {
                    var tb = new DialogCustoms("Mật khẩu mới bị trùng! Vui lòng điền lại!", "Thông báo!", DialogCustoms.OK);
                    tb.ShowDialog();
                }
                else
                {
                    nhanVien.MatKhau = matkhaumoi;
                    BUS_NhanVien bUS_NhanVien = new BUS_NhanVien();
                    bUS_NhanVien.Update(nhanVien);
                    var tb = new DialogCustoms("Thay đổi mật khẩu thành công!", "Thông báo!", DialogCustoms.OK);
                    tb.ShowDialog();
                }
            }
        }
    }
}
