using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DuAn_QuanLiKhachSan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Tạo và hiển thị cửa sổ "LoginForm"
            var loginForm = new Login.LoginForm(); // Đảm bảo rằng pages là namespace chứa LoginForm.xaml
            loginForm.Show();
        }
    }
}
