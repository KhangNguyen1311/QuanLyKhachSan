using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using DuAn_QuanLiKhachSan.Views;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using BUS_QLKS;
using DTO_QLKS;

namespace DuAn_QuanLiKhachSan.PageChild
{


    /// Interaction logic for thongtinPhong.xaml
    /// </summary>
    /// 
    public partial class thongtinPhong : Window
    {
        static BUS_Phong bus_Phong = new BUS_Phong();
        static BUS_ChiTietPhieuDatPhong bus_chiTietPhieuDatPhong = new BUS_ChiTietPhieuDatPhong();
        static BUS_DichVu bus_DichVu = new BUS_DichVu();
        static BUS_ChiTietDichVuPhieuDatPhong bus_chiTietDichVuPDP = new BUS_ChiTietDichVuPhieuDatPhong();
        private string MaPhong { get; set; }
        private string MaPDP { get; set; }
        private string TinhTrangPhong { get; set; }
        public thongtinPhong()
        {
            InitializeComponent();
        }
        private void LoadPhongInfo(string maPhong, string maPDP, string tinhTrang)
        {
            tenPhong.Text = maPhong;
            MaPhong = maPhong;
            MaPDP = maPDP;
            TinhTrangPhong = tinhTrang;

            // Lấy thông tin chi tiết của phòng từ phương thức SelectchiTietPhong()
            var chiTietPhong = bus_Phong.SelectAllPhong().FirstOrDefault(p => p.MaPhong == maPhong && p.TinhTrang == "Phòng đang thuê");
            // Kiểm tra xem chiTietPhong có tồn tại không
            if (chiTietPhong != null)
            {
                // Gán giá trị cho các thành phần giao diện người dùng
                DateTime? ngayDat = chiTietPhong.NgayDat;
                DateTime? ngayKetThuc = chiTietPhong.NgayKetThuc;
                if (ngayDat.HasValue)
                {
                    ngayCheckIn.Text = ngayDat.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    ngayCheckIn.Text = string.Empty;
                }
                ngayCheckIn.Text = (chiTietPhong.NgayDat ?? DateTime.MinValue).ToString("dd/MM/yyyy");
                if (chiTietPhong.GioDat.HasValue)
                {
                    gioCheckIn.Text = chiTietPhong.GioDat.Value.ToString(@"hh\:mm");
                }
                else
                {
                    gioCheckIn.Text = string.Empty;
                }

                if (ngayKetThuc.HasValue)
                {
                    ngayCheckOut.Text = ngayKetThuc.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    ngayCheckOut.Text = string.Empty;
                }
                ngayCheckOut.Text = (chiTietPhong.NgayKetThuc ?? DateTime.MinValue).ToString("dd/MM/yyyy"); //null coalescing ??
                if (chiTietPhong.GioKetThuc.HasValue)
                {
                    gioCheckOut.Text = chiTietPhong.GioKetThuc.Value.ToString(@"hh\:mm");
                }
                else
                {
                    gioCheckOut.Text = string.Empty;
                }


                tenKh.Text = chiTietPhong.TenKH ?? "";
                if (chiTietPhong.SoGio.GetValueOrDefault() >= 24)
                {
                    icDayorHour.Kind = MaterialDesignThemes.Wpf.PackIconKind.CalendarRange;
                    thoigGianThueP.Text = chiTietPhong.SoNgay.HasValue ? chiTietPhong.SoNgay.Value.ToString() + " ngày" : "";
                }
                else
                {
                    icDayorHour.Kind = MaterialDesignThemes.Wpf.PackIconKind.AlarmCheck;
                    thoigGianThueP.Text = chiTietPhong.SoGio.HasValue ? chiTietPhong.SoGio.Value.ToString() + " giờ" : "";
                }
                slNguoi.Text = chiTietPhong.SoNguoi.HasValue ? chiTietPhong.SoNguoi.Value.ToString() : "";
                tinhTrangPhong.Text = chiTietPhong.TinhTrang ?? "";
                if (chiTietPhong.TinhTrang.Equals("Phòng đang thuê"))
                {
                    btn_NhanPhong.Visibility = Visibility.Hidden;
                    btn_ThanhToan.Visibility = Visibility.Visible;
                    btn_themDichVu.Visibility = Visibility.Visible;
                }
                tinhTrangDonDep.Text = chiTietPhong.GhiChu ?? ""; // Nếu GhiChu là null thì gán chuỗi rỗng
            }
        }

        public thongtinPhong(string maPhong, string maPDP, string tinhTrang) : this()
        {
            LoadPhongInfo(maPhong, maPDP, tinhTrang);
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
            loadComboBoxDV();
        }
        private void loadDataGrid()
        {
            // Giả sử danh sách chiTietPhong đã được tạo và chứa dữ liệu
            // Lấy danh sách chi tiết phòng với điều kiện mã phòng và tình trạng hóa đơn chưa thanh toán
            List<DichVuTheoPDP> danhSachDV = bus_chiTietDichVuPDP.SelectDichVu()
                .Where(c => c.MaPhong.Equals(MaPhong) && c.MaPDP.Equals(MaPDP))
                .ToList();
            if (TinhTrangPhong != "Phòng trống")
            {
                // Gán nguồn dữ liệu cho DataGrid
                datagridDichVu.ItemsSource = danhSachDV;

            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_ThanhToan_Click(object sender, RoutedEventArgs e)
        {
       

            DTO_QLKS.ChiTietPhieuDatPhong chiTietPDP = bus_chiTietPhieuDatPhong.SelectAll().Where(c => c.MaPDP.Equals(MaPDP) && c.MaPhong.Equals(MaPhong)).FirstOrDefault();
            if (chiTietPDP != null)
            {
                chiTietPDP.TinhTrang = "Đã thanh toán";
                bus_chiTietPhieuDatPhong.UpdatePDPhong(chiTietPDP);

                // Hiển thị thông báo xác nhận thanh toán
                var ThongBao = new DialogCustoms("Xác nhận thanh toán", "Thông báo", DialogCustoms.YesNo);
                if (ThongBao.ShowDialog() == true)
                {

                    xuatHoaDon xuatHoaDon = new xuatHoaDon(MaPhong, MaPDP);
                    xuatHoaDon.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    xuatHoaDon.Show();

                }
                // Mở cửa sổ in hóa đơn

            }


        }

        private void btn_NhanPhong_Click(object sender, RoutedEventArgs e)
        {
            var Phong = bus_Phong.SelectAllPhong().FirstOrDefault(p => p.MaPhong.Equals(MaPhong));
            if (!Phong.NgayDat.Equals(DateTime.Now.Date))
            {
                var ThongBao = new DialogCustoms("Phòng chưa có thông tin đặt phòng!", "Thông báo", DialogCustoms.OK);
                ThongBao.ShowDialog();
            }
            else
            {
                // Ẩn nút "Nhận phòng" và hiển thị các nút khác
                btn_NhanPhong.Visibility = Visibility.Hidden;
                btn_ThanhToan.Visibility = Visibility.Visible;
                btn_themDichVu.Visibility = Visibility.Visible;

                // Tạo đối tượng Phong và cập nhật thông tin
                try
                {
                    DTO_QLKS.Phong phong = bus_Phong.SelectPhong().Where(c => c.MaPhong.Equals(tenPhong.Text)).FirstOrDefault();
                    if (phong != null)
                    {
                        phong.TinhTrang = "Phòng đang thuê"; // Cập nhật tình trạng của phòng                                                  
                        bus_Phong.UpdatePhong(phong);
                        LoadPhongInfo(MaPhong, MaPDP, TinhTrangPhong);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }



        private void loadComboBoxDV()
        {
            var dichvu = bus_DichVu.SelectDichVu().ToList();
            // Kiểm tra xem DataTable có dữ liệu không trước khi thêm vào ComboBox
            if (dichvu != null)
            {
                // Thiết lập DisplayMemberPath và SelectedValuePath
                listDichVụ.DisplayMemberPath = "DisplayValue"; //Thuộc tính bạn muốn hiển thị
                listDichVụ.SelectedValuePath = "MaDV"; // Thuộc tính bạn muốn làm giá trị được chọn

                // Thêm các đối tượng vào ComboBox từ DataTable
                foreach (var items in dichvu)
                {
                    // Tạo một đối tượng dữ liệu mới từ các cột của hàng
                    var dichvus = new
                    {
                        DisplayValue = items.MaDV.ToString() + "| " + items.TenDV.ToString() + " | ",
                        MaDV = items.MaDV.ToString()
                    };

                    // Thêm đối tượng vào ComboBox
                    listDichVụ.Items.Add(dichvus);
                }
            }
        }
        private void listDichVụ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                string selecteMaDv = comboBox.SelectedValue.ToString();
                // Sử dụng FirstOrDefault() để lấy ra một đối tượng đầu tiên hoặc null nếu danh sách rỗng
                DTO_QLKS.ListDichVu selectedDichVu = bus_DichVu.SelectDichVu().FirstOrDefault(c => c.MaDV.Equals(selecteMaDv));

                if (selectedDichVu != null)
                {
                    // Truy cập vào thuộc tính giá của đối tượng được chọn
                    decimal productPrice = (decimal)selectedDichVu.GiaTien;
                    giaDv.Text = productPrice.ToString("N0");
                }


            }
            else
            {
                // Xử lý trường hợp không có mục nào được chọn
            }

        }
        private void cong_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem giá trị trong TextBox có phải là một số không
            if (int.TryParse(soLuong.Text, out int currentValue))
            {
                // Tăng giá trị hiện tại lên 1 đơn vị
                currentValue++;
                // Gán giá trị mới vào TextBox
                soLuong.Text = currentValue.ToString();
            }
            else
            {
                // Xử lý trường hợp giá trị trong TextBox không phải là một số
                System.Windows.MessageBox.Show("Giá trị không hợp lệ.");
            }
        }

        private void tru_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem giá trị trong TextBox có phải là một số không
            if (int.TryParse(soLuong.Text, out int currentValue))
            {
                // Giảm giá trị hiện tại đi 1 đơn vị
                if (currentValue > 1)
                {
                    currentValue--;
                    // Gán giá trị mới vào TextBox
                    soLuong.Text = currentValue.ToString();
                }
                else
                {
                    // Xử lý trường hợp giá trị hiện tại đã là 1 và không thể giảm xuống nữa
                    System.Windows.MessageBox.Show("Giá trị không thể nhỏ hơn 1.");
                }
            }
            else
            {
                // Xử lý trường hợp giá trị trong TextBox không phải là một số
                System.Windows.MessageBox.Show("Giá trị không hợp lệ.");
            }
        }

        private void btn_themDichVu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string maDV = "";

                if (listDichVụ.SelectedValue != null)
                {
                    maDV = listDichVụ.SelectedValue.ToString();
                }
                else
                {
                    throw new Exception("Vui lòng chọn một dịch vụ.");
                }

                if (string.IsNullOrEmpty(soLuong.Text))
                {
                    throw new Exception("Vui lòng nhập số lượng dịch vụ.");
                }

                int sl;
                if (!int.TryParse(soLuong.Text, out sl) || sl <= 0)
                {
                    throw new Exception("Số lượng dịch vụ không hợp lệ.");
                }

                if (string.IsNullOrEmpty(tenPhong.Text))
                {
                    throw new Exception("Vui lòng chọn một phòng.");
                }

                string maPhong = tenPhong.Text;

                string maPDP_Phong = bus_Phong.SelectAllPhong()
                                             .Where(c => c.MaPhong == maPhong)
                                             .Select(c => c.MaPDP)
                                             .FirstOrDefault();

                if (maPDP_Phong == null)
                {
                    throw new Exception("Không tìm thấy phiếu đặt phòng cho phòng này.");
                }

                bool daThemDichVu = bus_chiTietDichVuPDP.SelectDichVu()
                                                         .Any(c => c.MaPhong.Equals(maPhong) && c.MaPDP.Equals(MaPDP) && c.MaDV.Equals(maDV));
                if (daThemDichVu)
                {
                    throw new Exception("Dịch vụ đã được thêm vào phòng trước đó! Bạn có thể cập nhật số lượng dịch vụ.");
                }

                // Thực hiện thêm dịch vụ vào phòng
                bus_chiTietDichVuPDP.InsertDichVuPhong(new DTO_QLKS.ChiTietDichVuPhieuDatPhong
                {
                    MaPDP = maPDP_Phong,
                    MaPhong = maPhong,
                    MaDV = maDV,
                    SoLuongDV = sl
                });

                var ThongBao = new DialogCustoms("Thêm dịch vụ thành công", "Thông báo", DialogCustoms.OK);
                ThongBao.ShowDialog();
                clearTXT();
                loadDataGrid();
            }
            catch (Exception ex)
            {
                var ThongBao = new DialogCustoms(ex.Message, "Lỗi", DialogCustoms.OK);
                ThongBao.ShowDialog();
            }


        }
        private void clearTXT()
        {
            giaDv.Text = string.Empty;
            listDichVụ.Text = string.Empty;
            soLuong.Text = "1";
        }


        private void btn_CapNhat_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý logic tại đây
            ComboBoxItem selectedItem = tinhTrangDonDep.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();
                DTO_QLKS.Phong phong = bus_Phong.SelectPhong().FirstOrDefault(c => c.MaPhong.Equals(MaPhong));

                if (phong != null)
                {
                    phong.GhiChu = selectedValue;
                    bus_Phong.UpdatePhong(phong);
                    var ThongBao = new DialogCustoms("Cập nhật tình trạng dọn dẹp thành công", "Thông báo", DialogCustoms.OK);
                    ThongBao.ShowDialog();
                }
            }
        }
        private void btn_UpdateDichVu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = new List<object>(datagridDichVu.SelectedItems.Cast<object>());
                foreach (var selectedItem in selectedItems)
                {
                    DataRowView selectedRow = selectedItem as DataRowView;
                    int rowindex = datagridDichVu.SelectedIndex;
                    var row = (DataGridRow)datagridDichVu.ItemContainerGenerator.ContainerFromIndex(rowindex);
                    if (row != null)
                    {
                        var cell = datagridDichVu.Columns[0].GetCellContent(row) as TextBlock;

                        if (cell != null)
                        {
                            string maDV = cell.Text;
                            cell = datagridDichVu.Columns[2].GetCellContent(row) as TextBlock;
                            if (cell != null)
                            {
                                string slDV = cell.Text;
                                int sl;
                                if (int.TryParse(slDV, out sl))
                                {
                                    DTO_QLKS.ChiTietDichVuPhieuDatPhong DVphong = bus_chiTietDichVuPDP.SelectDVPhong()
                                        .Where(c => c.MaPhong.Equals(MaPhong) && c.MaDV.Equals(maDV) && c.MaPDP.Equals(MaPDP))
                                        .FirstOrDefault();

                                    if (DVphong != null)
                                    {
                                        DVphong.SoLuongDV = sl;
                                        bus_chiTietDichVuPDP.UpdateDichVu(DVphong);
                                        var ThongBao = new DialogCustoms("Cập nhật dịch vụ thành công", "Thông báo", DialogCustoms.OK);
                                        ThongBao.ShowDialog();
                                        loadDataGrid();
                                    }
                                    else
                                    {
                                        var ThongBao = new DialogCustoms("Cập nhật dịch vụ thất bại !", "Thông báo", DialogCustoms.OK);
                                        ThongBao.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                var ThongBao = new DialogCustoms("Giá trị không hợp lệ!", "Thông báo", DialogCustoms.OK);
                                ThongBao.ShowDialog();
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                var ThongBao = new DialogCustoms(ex.Message, "Lỗi", DialogCustoms.OK);
                ThongBao.ShowDialog();
            }
        }

        private void btn_delDichVu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ThongBao = new DialogCustoms("Bạn có chắc chắn muốn xóa dịch vụ đã chọn?", "Xác nhận xóa", DialogCustoms.YesNo);
                if (ThongBao.ShowDialog() == true) // Người dùng chọn Yes
                {
                    var selectedItems = new List<object>(datagridDichVu.SelectedItems.Cast<object>());
                    foreach (var selectedItem in selectedItems)
                    {
                        DataRowView selectedRow = selectedItem as DataRowView;
                        int rowindex = datagridDichVu.SelectedIndex;
                        var row = (DataGridRow)datagridDichVu.ItemContainerGenerator.ContainerFromIndex(rowindex);
                        if (row != null)
                        {
                            var cell = datagridDichVu.Columns[0].GetCellContent(row) as TextBlock;

                            if (cell != null)
                            {
                                string maDV = cell.Text;
                                DTO_QLKS.ChiTietDichVuPhieuDatPhong DVphong = bus_chiTietDichVuPDP.SelectDVPhong()
                                    .Where(c => c.MaPhong.Equals(MaPhong) && c.MaDV.Equals(maDV) && c.MaPDP.Equals(MaPDP))
                                    .FirstOrDefault();

                                if (DVphong != null)
                                {
                                    bus_chiTietDichVuPDP.DeleteDichVu(DVphong);
                                    var ThongBao1 = new DialogCustoms("Xóa dịch vụ thành công", "Thông báo", DialogCustoms.OK);
                                    ThongBao1.ShowDialog();
                                    loadDataGrid();
                                }
                                else
                                {
                                    var ThongBao2 = new DialogCustoms("Xóa dịch vụ thất bại !", "Thông báo", DialogCustoms.OK);
                                    ThongBao2.ShowDialog();
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var ThongBao = new DialogCustoms(ex.Message, "Lỗi", DialogCustoms.OK);
                ThongBao.ShowDialog();
            }

        }


    }
}
