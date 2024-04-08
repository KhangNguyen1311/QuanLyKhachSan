using BUS_QLKS;
using DTO_QLKS;
using DuAn_QuanLiKhachSan.PageChild;
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
    /// Interaction logic for HoaDon.xaml
    /// </summary>
    public partial class HoaDon : Page
    {
        static BUS_HoaDon bUS_HoaDon = new BUS_HoaDon();
        public HoaDon()
        {
            InitializeComponent();
            taidulieulendatagrid();
        }
        public void taidulieulendatagrid()
        {
            List<ViewHoaDonNhanVienPhieuDatPhong> HoaDons = bUS_HoaDon.LayHet();
            datagrid.ItemsSource = HoaDons;
        }

        private void txt_searchBill_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txt_Tim.Text.ToLower();
            List<ViewHoaDonNhanVienPhieuDatPhong> timkiem = bUS_HoaDon.LayHet().Where(c => c.MaHD.ToLower().Contains(searchText)).ToList();
            datagrid.ItemsSource = timkiem;
        }
        private void dtpChonNgay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy ngày được chọn từ DatePicker, nếu không có ngày nào được chọn thì không làm gì cả
            if (dtpChonNgay.SelectedDate == null) return;

            // Chuyển đổi ngày được chọn sang kiểu DateTime
            DateTime selectedDate = dtpChonNgay.SelectedDate.Value;

            // Lọc dữ liệu dựa trên ngày được chọn
            // Giả sử NgayInHD là kiểu DateTime, chúng ta sẽ so sánh trực tiếp mà không cần chuyển đổi sang chuỗi
            // Nếu NgayInHD không phải là DateTime, bạn sẽ cần phải chuyển đổi nó sao cho phù hợp để so sánh
            List<ViewHoaDonNhanVienPhieuDatPhong> timkiem = bUS_HoaDon.LayHet()
                .Where(c => c.NgayInHD.Date == selectedDate.Date).ToList();

            // Cập nhật nguồn dữ liệu cho DataGrid
            datagrid.ItemsSource = timkiem;
        }

        public void View_Click(object sender, EventArgs e)
        {
            xuatHoaDon XuatHoaDon = new xuatHoaDon();

            XuatHoaDon.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            XuatHoaDon.Show();
        }

    }
}
