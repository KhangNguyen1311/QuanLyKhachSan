using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using BUS_QLKS;
using DAL_QLKS;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for themNhanVien.xaml
    /// </summary>
    public partial class themNhanVien : Window
    {
        static DAL_NhanVien DAL_NhanVIens = new DAL_NhanVien();

        static BUS_NhanVien bUS_NhanVIen = new BUS_NhanVien();
        public string imagepath = "";

        public string imagename = "";
        public event EventHandler ChildClosed;

        public themNhanVien()
        {
            InitializeComponent();
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
        private void bt_ChonAnh_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            /*string destinationFolder = @"C:\Quanlykhachsan";*/
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imagepath = openFileDialog.FileName;
                imagename = openFileDialog.SafeFileName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                ImageViewer.Source = bitmapImage;
                Debug.WriteLine(openFileDialog.SafeFileName);


            }
        }


        public void SaveImage()
        {
            string destinationFolder = @"C:\Quanlikhachsan";
            string destinationFilePath = System.IO.Path.Combine(destinationFolder, imagename);
            if (File.Exists(destinationFilePath) == false)
            {
                File.Copy(imagepath, destinationFilePath, true);
                imagepath = destinationFilePath;
            }
        }

        private void bt_Luu_Click(object sender, RoutedEventArgs e)
        {


            string phai = "";
            string maNV = string.Format("DV{0:D4}", bUS_NhanVIen.laytatca().Count + 1);


            if (errorCCCD.Text.Length != 0 || errorEmail.Text.Length != 0 || errorSDT.Text.Length != 0)

            {
                // Hiển thị thông báo lỗi
                System.Windows.MessageBox.Show("Vui lòng kiểm tra lại thông tin!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_TenNV.Text.Length == 0 || txt_SDT.Text.Length == 0 || txt_DiaChi.Text.Length == 0 || txt_Email.Text.Length == 0 || txt_ChucVu.Text.Length == 0 || txt_MatKhau.Text.Length == 0 || txt_NgaySinh.Text.Length == 0 || txt_CCCD.Text.Length == 0)
            {
                System.Windows.MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                // Cần chỉ cần kiểm tra một trong hai radio button, vì nếu không phải Nam thì chắc chắn sẽ là Nữ
                if (radio_Nam.IsChecked == true)
                {
                    phai = "Nam";
                }
                else if (radio_Nu.IsChecked == true) // Sử dụng else if để tránh kiểm tra không cần thiết
                {
                    phai = "Nữ";
                }

                // Đảm bảo bạn đã khai báo và định nghĩa 'imagepath' và 'txt_NgaySinh' phù hợp trước khi sử dụng

                // Tạo mới đối tượng NhanVien
                try
                {
                    string MaNV = maNV;
                    string TenNV = txt_TenNV.Text;
                    string SDT = txt_SDT.Text;
                    string DiaChi = txt_DiaChi.Text;
                    string Email = txt_Email.Text;
                    string VaiTro = txt_ChucVu.Text;
                    string MatKhau = txt_MatKhau.Text;
                    string NgaySinh = txt_NgaySinh.Text;// Chuyển đổi chuỗi sang DateTime
                    string HinhAnh = imagepath;
                    string CCCD = txt_CCCD.Text;
                    string Phai = phai;


                    // Gọi phương thức thêm mới nhân viên
                    bUS_NhanVIen.Insert(TenNV, SDT, NgaySinh, DiaChi, HinhAnh, VaiTro, Email, phai, MatKhau, CCCD);

                    // Thông báo kết quả
                    System.Windows.MessageBox.Show("Lưu thành công");
                    ChildClosed ?.Invoke(this, EventArgs.Empty);

                    // Gọi hàm LoadData để cập nhật dữ liệu hiển thị (giả sử bạn đã có hàm LoadData)



                    SaveImage();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Mời bạn nhập lại  ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }



        private void bt_Xoa_Click(object sender, RoutedEventArgs e)
        {
            txt_MaNV.Text = "";
            txt_TenNV.Text = "";
            txt_CCCD.Text = "";
            txt_Email.Text = "";
            txt_SDT.Text = "";
            txt_NgaySinh.Text = "";
            txt_DiaChi.Text = "";
            txt_ChucVu.Text = "";
            txt_MatKhau.Text = "";
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



        public static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy cho địa chỉ email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            // Sử dụng Regex.IsMatch để kiểm tra chuỗi
            return Regex.IsMatch(email, pattern);
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
    }
}

