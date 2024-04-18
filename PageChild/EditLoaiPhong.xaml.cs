
using BUS_QLKS;

using DuAn_QuanLiKhachSan.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for EditLoaiPhong.xaml
    /// </summary>
    public partial class EditLoaiPhong : Window
    {
        public event EventHandler ChildClosed;
        static BUS_LoaiPhong bus_loaiphong = new BUS_LoaiPhong();
        static string maloaip;
        public EditLoaiPhong(string mlp)
        {
            InitializeComponent();
            maloaip = mlp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txt_giagio.Text.Length != 0 || txt_tenlp.Text.Length != 0 || giangay.Text.Length != 0)
            {
                DTO_QLKS.LoaiPhong loaip = new DTO_QLKS.LoaiPhong
                {
                    MaLoaiPhong = maloaip,
                    TenLoaiPhong = txt_tenlp.Text,
                    GiaTheoGio = double.Parse(txt_giagio.Text),
                    GiaTheoNgay = double.Parse(giangay.Text),
                };
                bus_loaiphong.Update(loaip);
                ChildClosed.Invoke(this, EventArgs.Empty);
                var ThongBao1 = new DialogCustoms("Lưu thành công!", "Thông báo", DialogCustoms.OK);
                ThongBao1.ShowDialog();
            }
        
        }
        public void loadata()
        {
            DTO_QLKS.LoaiPhong lp = bus_loaiphong.SelectAll().Where(c=>c.MaLoaiPhong==maloaip).FirstOrDefault();
            txt_tenlp.Text = lp.TenLoaiPhong;
            maloaiphong.Text = lp.MaLoaiPhong;
            txt_giagio.Text = lp.GiaTheoGio.ToString();
            giangay.Text = lp.GiaTheoNgay.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadata();
        }







        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ChildClosed?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void txt_giagio_TextChanged(object sender, TextChangedEventArgs e)
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

        private void giangay_TextChanged(object sender, TextChangedEventArgs e)
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
