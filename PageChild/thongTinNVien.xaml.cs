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
using System.Diagnostics;
using System.Windows.Forms;
using DuAn_QuanLiKhachSan.Views;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using BUS_QLKS;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for thongTinNVien.xaml
    /// </summary>
    public partial class thongTinNVien : Window
    {
        public event EventHandler ChildClosed;
        static BUS_NhanVien bUS_NhanVien = new BUS_NhanVien();
        static BUS_DichVu bUS_DichVu = new BUS_DichVu();
        private string maNV;
        static DTO_QLKS.NhanVien NhanVien = new DTO_QLKS.NhanVien();
        class ChucVU
        {
            public int machucvu { get; set; }
            public string vaitro { get; set; }
        }
        public thongTinNVien(string manv)
        {



            maNV = manv;
            NhanVien = bUS_NhanVien.SelectAll().Where(c=>c.MaNV==maNV).FirstOrDefault();


            InitializeComponent();

        }





        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /*   public void LoadTinhTrangBtn()
           {
               if (nhanVien.TinhTrang.ToLower().Equals("hoạt động"))
               {
                   bt_VoHieuHoa.Content = "Vô hiệu hóa";

                   bt_VoHieuHoa.Click += bt_VoHieuHoa_Click_1;
                   bt_VoHieuHoa.Click -= btn_kichhoat_Click;
               }
               else if (nhanVien.TinhTrang.ToLower().Equals("không hoạt động"))
               {
                   bt_VoHieuHoa.Content = "Kích hoạt";
                   bt_VoHieuHoa.Click += btn_kichhoat_Click;
                   bt_VoHieuHoa.Click -= btn_unable_Click;
               }
           }*/


        public void ResetTextBox()
        {
            txt_MaNV.IsReadOnly = true;
            txt_TenNV.IsReadOnly = true;
            txt_Email.IsReadOnly = true;
            txt_SDT.IsReadOnly = true;
            txt_DiaChi.IsReadOnly = true;
            txt_ChuVu.IsEnabled = false;
            txt_NgaySinh.IsEnabled = false;
        }

        public void EnableTxtbBox()
        {
            txt_TenNV.IsReadOnly = false;
            txt_Email.IsReadOnly = false;
            txt_SDT.IsReadOnly = false;
            txt_DiaChi.IsReadOnly = false;
            txt_ChuVu.IsEnabled = true;
            txt_NgaySinh.IsEnabled = true;
        }

        private void txt_ChuVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void load()
        {
            txt_TenNV.Text = NhanVien.TenNV.ToString();
            txt_MaNV.Text = NhanVien.MaNV.ToString();
            txt_Email.Text = NhanVien.Email.ToString();
            txt_ChuVu.SelectedItem = NhanVien.VaiTro;
            txt_DiaChi.Text = NhanVien.DiaChi.ToString();
            txt_CCCD.Text = NhanVien.CCCD.ToString();
            txt_SDT.Text = NhanVien.SDT.ToString();
            txt_NgaySinh.Text = NhanVien.NgaySinh.ToString();
            BitmapImage bitmapImage = new BitmapImage(new Uri(NhanVien.HinhAnh));
            img.ImageSource = bitmapImage;
            string gioitinh = "";
            if (NhanVien.Phai)
                gioitinh = "Nữ";
            else
                gioitinh = "Nam";
            txt_GioiTinh.Text = gioitinh;






        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
            loadnut();

        }

        private void txt_ChuVu_Loaded(object sender, RoutedEventArgs e)
        {
            List<ChucVU> chucvus = new List<ChucVU>();
            chucvus.Add(new ChucVU { machucvu = 1, vaitro = "Quản lý" });
            chucvus.Add(new ChucVU { machucvu = 2, vaitro = "Nhân viên" });
            chucvus.Add(new ChucVU { machucvu = 3, vaitro = "Dịch vụ" });
            chucvus.Add(new ChucVU { machucvu = 4, vaitro = "Lễ tân" });
            txt_ChuVu.ItemsSource = chucvus;
            txt_ChuVu.DisplayMemberPath = "vaitro";
            txt_ChuVu.SelectedValuePath = "machucvu";


            int machucvu = 0;




            if (NhanVien.VaiTro.ToLower() == "quản lý")
                machucvu = 1;
            else if (NhanVien.VaiTro.ToLower() == "nhân viên")
                machucvu = 2;
            else if (NhanVien.VaiTro.ToLower() == "dịch vụ")
                machucvu = 3;
            else if (NhanVien.VaiTro.ToLower() == "lễ tân")
                machucvu = 4;
            txt_ChuVu.SelectedValue = machucvu;
        }

        private void txt_MaNV_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;

            // Kiểm tra nếu TextBox trống hoặc chỉ chứa khoảng trắng
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Hiển thị thông báo lỗi thông qua ToolTip
                textBox.ToolTip = "Bạn cần nhập thông tin vào đây!";
                textBox.BorderBrush = System.Windows.Media.Brushes.Red; // Đổi màu viền thành đỏ để người dùng chú ý
            }
            else
            {
                // Nếu người dùng đã nhập, ẩn thông báo lỗi và trả lại màu viền mặc định
                textBox.ToolTip = null;
                textBox.ClearValue(System.Windows.Controls.TextBox.BorderBrushProperty);
            }
        }






        private void bt_CapNhat_Click(object sender, RoutedEventArgs e)
        {


            string phai = "";



            if (errorCCCD.Text.Length != 0 || errorEmail.Text.Length != 0 || errorSDT.Text.Length != 0)

            {
                // Hiển thị thông báo lỗi
                System.Windows.MessageBox.Show("Vui lòng kiểm tra lại thông tin!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_TenNV.Text.Length == 0 || txt_SDT.Text.Length == 0 || txt_DiaChi.Text.Length == 0 || txt_Email.Text.Length == 0 || txt_ChuVu.Text.Length == 0 || txt_NgaySinh.Text.Length == 0 || txt_CCCD.Text.Length == 0)
            {
                System.Windows.MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }


            else
            {
                NhanVien.TenNV = txt_TenNV.Text;
                NhanVien.Email = txt_Email.Text;
                NhanVien.SDT = txt_SDT.Text;
                NhanVien.VaiTro = txt_ChuVu.Text;
                NhanVien.DiaChi = txt_DiaChi.Text;
                NhanVien.NgaySinh = txt_NgaySinh.Text;
                NhanVien.CCCD = txt_CCCD.Text;
                string Phai = phai;
                bUS_NhanVien.Update(NhanVien);
                System.Windows.MessageBox.Show("Cập nhập thành công");

                ChildClosed?.Invoke(this, EventArgs.Empty);

                load();
            }

        }

        public void loadnut()
        {
            DTO_QLKS.NhanVien nhanhvien = bUS_NhanVien.GetById(maNV);
            if (nhanhvien.TinhTrang == "Hoạt động")
            {
                bt_VoHieuHoa.Content = "Vô hiệu hoá";
            }
            else if (nhanhvien.TinhTrang == "Không hoạt động")
            {
                bt_VoHieuHoa.Content = "Kích hoạt";
            }
        }

        private void bt_VoHieuHoa_Click(object sender, RoutedEventArgs e)
        {


            DTO_QLKS.NhanVien nhanhvien = bUS_NhanVien.GetById(maNV);

            if (nhanhvien.VaiTro == "Quản lý")
            {
                System.Windows.MessageBox.Show("Không thể vô hiệu hoá quản lý");
            }
            else
            {
                if (nhanhvien.TinhTrang == "Hoạt động")
                {
                    nhanhvien.TinhTrang = "Không hoạt động";
                    bUS_NhanVien.Update(nhanhvien);
                    System.Windows.MessageBox.Show("Vô hiệu hoá thành công");

                }
                else if (nhanhvien.TinhTrang == "Không hoạt động")
                {
                    nhanhvien.TinhTrang = "Hoạt động";
                    bUS_NhanVien.Update(nhanhvien);
                    System.Windows.MessageBox.Show("Kích hoạt thành công");

                }
            }

            loadnut();
        }

        private void txt_SDT_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                // Chỉ cho phép nhập số
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());

                // Di chuyển con trỏ về cuối chuỗi để người dùng có thể tiếp tục nhập
                textBox.CaretIndex = textBox.Text.Length;

                // Kiểm tra độ dài của chuỗi
                if (textBox.Text.Length < 10 || textBox.Text.Length > 10)
                {
                    errorSDT.Text = "Bạn phải nhập đủ 10 số.";
                }
                else
                {
                    errorSDT.Text = ""; // Xóa thông báo lỗi khi đã nhập đủ 10 số
                }
            }
        }

        private void txt_CCCD_TextChanged(object sender, TextChangedEventArgs e)
        {

            var textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                // Loại bỏ ký tự không phải là số và cập nhật lại Text của TextBox
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());

                // Di chuyển con trỏ về cuối chuỗi để người dùng có thể tiếp tục nhập
                textBox.CaretIndex = textBox.Text.Length;

                // Kiểm tra độ dài của chuỗi
                if (textBox.Text.Length < 12 || textBox.Text.Length > 12)
                {
                    errorCCCD.Text = "Bạn phải nhập đúng 12 số.";
                }
                else
                {
                    errorCCCD.Text = ""; // Xóa thông báo lỗi khi đã nhập đúng 12 số
                }
            }
        }


        private void txt_Email_LostFocus(object sender, RoutedEventArgs e)
        {

            var textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                // Kiểm tra xem text trong textbox có kết thúc bằng "@gmail.com" hay không.
                if (!IsValidEmail(textBox.Text))
                {
                    // Hiển thị thông báo lỗi hoặc xử lý tùy thuộc vào yêu cầu.
                    errorEmail.Text = "Email sai định dạng!!";
                }
                else
                {
                    errorEmail.Text = "";
                }
                // Nếu có, bạn có thể thêm logic xử lý khác ở đây.
            }
        }
        public static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy cho địa chỉ email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            // Sử dụng Regex.IsMatch để kiểm tra chuỗi
            return Regex.IsMatch(email, pattern);
        }
    }
}
