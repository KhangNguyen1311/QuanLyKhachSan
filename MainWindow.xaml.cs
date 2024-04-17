using DuAn_QuanLiKhachSan.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuAn_QuanLiKhachSan.Views;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTO_QLKS;
namespace DuAn_QuanLiKhachSan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DTO_QLKS.NhanVien nhanvien;
        public MainWindow(DTO_QLKS.NhanVien nv)
        {
            InitializeComponent();
            frameContent.Navigate(new trangChu());
            WindowState = WindowState.Maximized;
            nhanvien = nv;
        }
        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            else
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }
        public void XacDinhVaiTro()
        {
            if(nhanvien.VaiTro.ToLower() == "giám đốc")
            {
                rdRoom.Visibility = Visibility.Hidden;
                rdKindOfRoom.Visibility = Visibility.Hidden;
                rdBookroom.Visibility = Visibility.Hidden;
                rdBills.Visibility = Visibility.Hidden;
                rdClient.Visibility = Visibility.Hidden;
                rdServices.Visibility = Visibility.Hidden;
            }
            else if(nhanvien.VaiTro.ToLower()=="quản lý")
            {
                rdChart.Visibility = Visibility.Hidden;

            }
            else
            {
                rdChart.Visibility= Visibility.Hidden;
                rdStaff.Visibility= Visibility.Hidden;
            }
        }
        public void loadThongtinNhanvien()
        {
            ten_nv.Text = nhanvien.TenNV.ToString();
            vaitro_nv.Text = nhanvien.VaiTro.ToString();
            BitmapImage bitmapImage = new BitmapImage(new Uri(nhanvien.HinhAnh));
            anh_nv.ImageSource = bitmapImage;
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

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new trangChu());

           
        }


        private void rdRoom_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Views.Phong(nhanvien));
        }

        private void rdBookroom_Click(object sender, RoutedEventArgs e)
        {
            
            frameContent.Navigate(new DatPhong());

        }

        private void rdClient_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Views.KhachHang());

        }

        private void rdBills_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Views.HoaDon());

        }

        private void rdServices_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Views.DichVu(nhanvien));

        }

        private void rdKindOfRoom_Click(object sender, RoutedEventArgs e)
        {
            
            frameContent.Navigate(new Views.LoaiPhong(nhanvien));

        }

        private void rdStaff_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Views.NhanVien());

        }

        private void rdChart_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new thongKe());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadThongtinNhanvien();
            XacDinhVaiTro();
        }

        private void rdSettings_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new SettingPage(nhanvien));
        }
    }
}
