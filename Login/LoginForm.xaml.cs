using BUS_QLKS;
using DTO_QLKS;
using DuAn_QuanLiKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DuAn_QuanLiKhachSan.Login
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        static BUS_NhanVien bUS_NhanVien = new BUS_NhanVien();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txt_email.Text;
            string password = txt_password.Password;
            if(bUS_NhanVien.CheckLogin(email, password))
            {
                DTO_QLKS.NhanVien nhanvien = bUS_NhanVien.GetNhanVienByEmailAndPass(email, password);
                MainWindow mainWindow = new MainWindow(nhanvien);
                mainWindow.Show();  
            }
            else
            {
                var ThongBao1 = new DialogCustoms("Email hoặc mật khẩu bị sai vui lòng nhập lại!", "Thông báo", DialogCustoms.OK);
                ThongBao1.ShowDialog();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txt_email.Text = string.Empty;
            txt_password.Password = string.Empty;
        }
    }
}
