using BUS_QLKS;
using DuAn_QuanLiKhachSan.Views;
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

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for themPhong.xaml
    /// </summary>
    public partial class themPhong : Window
    {
        static BUS_LoaiPhong bus_LoaiPhong = new BUS_LoaiPhong();
        static BUS_Phong bus_Phong = new BUS_Phong();
        public event EventHandler ChildClosed;

        public themPhong()
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

        private void Load(object sender, RoutedEventArgs e)
        {
            var loaiPhongs = bus_LoaiPhong.SelectLoaiPhong().ToList();
            if (loaiPhongs != null)
            {
                // Thiết lập DisplayMemberPath và SelectedValuePath
                loaiPhong.DisplayMemberPath = "DisplayValue"; //Thuộc tính bạn muốn hiển thị
                loaiPhong.SelectedValuePath = "MaLoaiPhong"; // Thuộc tính bạn muốn làm giá trị được chọn

                // Thêm các đối tượng vào ComboBox từ DataTable
                foreach (var items in loaiPhongs)
                {
                    // Tạo một đối tượng dữ liệu mới từ các cột của hàng
                    var dichvus = new
                    {
                        DisplayValue = items.MaLoaiPhong.ToString() + "| " + items.TenLoaiPhong.ToString(),
                        MaLoaiPhong = items.MaLoaiPhong.ToString()
                    };

                    // Thêm đối tượng vào ComboBox
                    loaiPhong.Items.Add(dichvus);
                }
            }
        }


        private string FormatCurrency(decimal value)
        {
            return string.Format("{0:#,##0} VNĐ", value);
        }
        private void loaiPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                string selecteMaDv = comboBox.SelectedValue.ToString();
                // Sử dụng FirstOrDefault() để lấy ra một đối tượng đầu tiên hoặc null nếu danh sách rỗng
                DTO_QLKS.ListLoaiPhong selectedDichVu = bus_LoaiPhong.SelectLoaiPhong().FirstOrDefault(c => c.MaLoaiPhong.Equals(selecteMaDv));

                if (selectedDichVu != null)
                {
                    // Truy cập vào thuộc tính giá của đối tượng được chọn
                    decimal giaGio = (decimal)selectedDichVu.GiaTheoGio;
                    decimal giaNgay = (decimal)selectedDichVu.GiaTheoNgay; // Thay đổi ở đây
                    giaTheoGio.Text = FormatCurrency(giaGio);
                    giaTheoNgay.Text = FormatCurrency(giaNgay);
                }

            }
        }

        private void addPhong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string MaLoaiPhong = "";
                if (loaiPhong.SelectedValue != null)
                {
                    MaLoaiPhong = loaiPhong.SelectedValue.ToString();
                }
                int Sotang = Convert.ToInt32(viTri.Text);
                if (MaLoaiPhong != null && Sotang > 0)
                {
                    bus_Phong.Insert(new DTO_QLKS.Phong
                    {
                        MaLoaiPhong = MaLoaiPhong,
                        Tang = Sotang
                    });

                    var ThongBao = new DialogCustoms("Thêm phòng thành công", "Thông báo", DialogCustoms.OK);
                    ThongBao.ShowDialog();
                    clear();
                }
                else
                {
                    var ThongBao = new DialogCustoms("Thêm phòng thất bại", "Lỗi", DialogCustoms.OK);
                    ThongBao.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                var ThongBao = new DialogCustoms(ex.Message, "Lỗi", DialogCustoms.OK);
                ThongBao.ShowDialog();
            }
        }

        private void clearTxt_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void clear()
        {
            loaiPhong.Text = string.Empty;
            viTri.Text = string.Empty;
            giaTheoGio.Text = string.Empty;
            giaTheoNgay.Text = string.Empty;
        }
    }
}

