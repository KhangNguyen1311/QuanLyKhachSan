using BUS_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for themkhachhangg.xaml
    /// </summary>
    public partial class themkhachhangg : Window
    {

        static BUS_KhachHang bUS_Khachhang = new BUS_KhachHang();
        public event EventHandler ChildClosed;

        public themkhachhangg()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ChildClosed.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string phai = "";




            if (ten.Text.Length == 0 || sdthoai.Text.Length == 0 || ngaysinh.Text.Length == 0 || diachi.Text.Length == 0 || cancuoc.Text.Length == 0 || quocgia.Text.Length == 0)
            {
                System.Windows.MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (radio_Nam.IsChecked == true)
                {
                    phai = "Nam";
                }
                else if (radio_Nu.IsChecked == true) // Sử dụng else if để tránh kiểm tra không cần thiết
                {
                    phai = "Nữ";
                }
                try
                {
                    string TenKH = ten.Text;
                    string SDT = sdthoai.Text;
                    string NgaySinh = ngaysinh.Text;
                    string Diachi = diachi.Text;
                    string Phai = phai;
                    string MaCMT = cancuoc.Text;
                    string QuocTich = quocgia.Text;
                    bUS_Khachhang.Insert(TenKH, SDT, NgaySinh, Diachi, Phai, MaCMT, QuocTich);
                    System.Windows.MessageBox.Show("Lưu thành công");

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Mời bạn nhập lại  ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void sdthoai_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Loại bỏ mọi ký tự không phải số
            string input = textBox.Text;
            string pattern = "[^0-9]"; // Chỉ lấy các ký tự không phải số
            string replacement = "";
            string result = Regex.Replace(input, pattern, replacement);

            // Cập nhật nội dung của TextBox
            textBox.Text = result;
            // Di chuyển con trỏ đến cuối chuỗi
            textBox.CaretIndex = textBox.Text.Length;

        }


    }
}
