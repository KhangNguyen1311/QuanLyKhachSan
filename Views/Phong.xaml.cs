using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DuAn_QuanLiKhachSan.PageChild;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Globalization;
using MaterialDesignThemes.Wpf;
using BUS_QLKS;
using DTO_QLKS;

namespace DuAn_QuanLiKhachSan.Views
{
    /// <summary>
    /// Interaction logic for Phong.xaml
    /// </summary>
    ///         

    public partial class Phong : Page
    {
        static BUS_Phong bus_Phong = new BUS_Phong();
        static DTO_QLKS.NhanVien nhanvien;
        public Phong(DTO_QLKS.NhanVien nv)
        {
            InitializeComponent();
            dataPhong();
            nhanvien = nv;

        }

        public void Xacdinhvaitro()
        {
            if (nhanvien.VaiTro.ToLower() != "quản lý")
            {
                btn_themPhong.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_themPhong.Visibility = Visibility.Visible;
            }
        }



        private void Load(object sender, RoutedEventArgs e)
        {
            dtpChonNgay.Text = DateTime.Now.ToShortDateString();
            tpGio.Text = DateTime.Now.ToString("HH:mm:ss");
            timKiemPhongTheoThoiGian();
            lvPhongDon.PreviewMouseLeftButtonUp += ListView_PreviewMouseLeftButtonUp;
            lvPhongDoi.PreviewMouseLeftButtonUp += ListView_PreviewMouseLeftButtonUp;
            lvPhongGiaDinh.PreviewMouseLeftButtonUp += ListView_PreviewMouseLeftButtonUp;

        }

        private void btn_themPhong_Click(object sender, RoutedEventArgs e)
        {
            themPhong themPhong = new themPhong();
            themPhong.ChildClosed += ChildWindowClosed;

            themPhong.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            themPhong.Show();
        }
        private void ChildWindowClosed(object sender, EventArgs e)
        {
            // Handle child window closed event here
            dataPhong();
        }

        private void dataPhong()
        {
            List<DanhSachThongTinPhong> viewPhongs = bus_Phong.SelectPhongDon();
            lvPhongDon.ItemsSource = viewPhongs;
            List<DanhSachThongTinPhong> viewPhongs1 = bus_Phong.SelectPhongDoi();
            lvPhongDoi.ItemsSource = viewPhongs1;
            List<DanhSachThongTinPhong> viewPhongs2 = bus_Phong.SelectPhongFamily();
            lvPhongGiaDinh.ItemsSource = viewPhongs2;
        }
        private void ListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView lv = sender as System.Windows.Controls.ListView;
            if (lv != null)
            {
                // Lấy ListViewItem được chọn từ ListView
                System.Windows.Controls.ListViewItem selectedItem = lv.ItemContainerGenerator.ContainerFromItem(lv.SelectedItem) as System.Windows.Controls.ListViewItem;

                if (selectedItem != null)
                {
                    // Lấy DataContext của ListViewItem, đó chính là dữ liệu được binding trong DataTemplate
                    DanhSachThongTinPhong phong = selectedItem.DataContext as DanhSachThongTinPhong;

                    if (phong != null)
                    {
                        string maPhong = phong.MaPhong;
                        string tinhTrangPhong = phong.TinhTrang;
                        var maPDP = bus_Phong.SelectAllPhong()
                              .Where(c => c.MaPhong == maPhong)
                              .Select(c => c.MaPDP)
                              .ToList();
                        string maPDPString = string.Join(", ", maPDP);

                        thongtinPhong thongtinPhong = new thongtinPhong(maPhong, maPDPString, tinhTrangPhong);
                        thongtinPhong.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        thongtinPhong.Show();
                    }
                }
            }
        }


        private void txt_searchRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            lvPhongDon.Items.Filter = filterTimKiem;
            lvPhongDoi.Items.Filter = filterTimKiem;
            lvPhongGiaDinh.Items.Filter = filterTimKiem;
        }

        private bool filterTimKiem(object obj)
        {
            if (String.IsNullOrEmpty(txt_searchRoom.Text))
                return true;
            else
                return (obj as DanhSachThongTinPhong).MaPhong.ToLower().Contains(txt_searchRoom.Text.ToLower());
        }



        private void timKiemPhongTheoThoiGian()
        {
            string ngayChon = dtpChonNgay.Text;
            DateTime dateTime = new DateTime();
            if (string.IsNullOrEmpty(ngayChon))
            {
                new DialogCustoms("Ngày không được để trống!", "Lỗi", DialogCustoms.OK).ShowDialog();
                return;
            }

            if (!DateTime.TryParse(dtpChonNgay.Text + " " + tpGio.Text, out dateTime))
            {
                new DialogCustoms("Nhập đúng định dạng ngày giờ !", "Thông báo", DialogCustoms.OK).ShowDialog();
                return;
            }
            // Lấy danh sách phòng theo thời gian được chọn
            var danhSachPhongs = bus_Phong.GetPhongsByDate(dateTime);
            if (danhSachPhongs != null)
            {
                lvPhongDon.ItemsSource = danhSachPhongs.Where(p => p.TenLoaiPhong == "Phòng đơn").ToList();
                lvPhongDoi.ItemsSource = danhSachPhongs.Where(p => p.TenLoaiPhong == "Phòng đôi").ToList();
                lvPhongGiaDinh.ItemsSource = danhSachPhongs.Where(p => p.TenLoaiPhong == "Phòng gia đình").ToList();
            }
            rdTatCa.IsChecked = true;
            rdTatCaLoaiPhong.IsChecked = true;
            rdTatCaPhong.IsChecked = true;
        }


        private void dtpChonNgay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            timKiemPhongTheoThoiGian();

        }
        private void tpGio_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            timKiemPhongTheoThoiGian();
        }


        // click vào radioButton để lọc
        private List<DanhSachThongTinPhong> FilterPhongList(string tinhTrang)
        {
            switch (tinhTrang)
            {
                case "Phòng trống":
                    return bus_Phong.SelectAllPhong().Where(c => c.TinhTrang.Equals("Phòng trống")).ToList();
                case "Phòng đã đặt":
                    return bus_Phong.SelectAllPhong().Where(c => c.TinhTrang.Equals("Phòng đã đặt")).ToList();
                case "Phòng đang thuê":
                    return bus_Phong.SelectAllPhong().Where(c => c.TinhTrang.Equals("Phòng đang thuê")).ToList();
                case "Tất cả phòng":
                    return bus_Phong.SelectAllPhong();
                default:
                    return new List<DanhSachThongTinPhong>();
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton selectedRadioButton = sender as System.Windows.Controls.RadioButton;

            if (selectedRadioButton != null)
            {
                string selectedContent = selectedRadioButton.Content.ToString();

                List<DanhSachThongTinPhong> filteredPhongList = FilterPhongList(selectedContent);

                // Kiểm tra loại phòng và gán danh sách phòng đã lọc cho ListView tương ứng
                switch (selectedContent)
                {
                    case "Phòng trống":
                        lvPhongDon.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đơn").ToList();
                        lvPhongDoi.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đôi").ToList();
                        lvPhongGiaDinh.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng gia đình").ToList();
                        break;
                    case "Phòng đã đặt":
                    case "Phòng đang thuê":
                    case "Tất cả phòng":
                        lvPhongDon.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đơn").ToList();
                        lvPhongDoi.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đôi").ToList();
                        lvPhongGiaDinh.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng gia đình").ToList();
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoaiPhongRadioButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton selectedRadioButton = sender as System.Windows.Controls.RadioButton;

            if (selectedRadioButton != null)
            {
                string selectedContent = selectedRadioButton.Content.ToString();

                // Gán danh sách phòng đã lọc cho các ListView tương ứng
                switch (selectedContent)
                {
                    case "Phòng đơn":
                        lvPhongDon.ItemsSource = bus_Phong.SelectPhongDon().ToList();
                        lvPhongDoi.ItemsSource = new List<object>();
                        lvPhongGiaDinh.ItemsSource = new List<object>();
                        break;
                    case "Phòng đôi":
                        lvPhongDoi.ItemsSource = bus_Phong.SelectPhongDoi().ToList();
                        lvPhongDon.ItemsSource = new List<object>();
                        lvPhongGiaDinh.ItemsSource = new List<object>();
                        break;
                    case "Phòng gia đình":
                        lvPhongDoi.ItemsSource = new List<object>();
                        lvPhongDon.ItemsSource = new List<object>();
                        lvPhongGiaDinh.ItemsSource = bus_Phong.SelectPhongFamily().ToList();
                        break;
                    case "Tất cả loại phòng":
                        // Gán danh sách phòng đã lọc cho tất cả các ListView
                        dataPhong();
                        break;
                    default:
                        break;
                }
            }
        }


        private void DonDepRadioButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton selectedRadioButton = sender as System.Windows.Controls.RadioButton;

            if (selectedRadioButton != null)
            {
                string selectedContent = selectedRadioButton.Content.ToString();

                // Lọc danh sách phòng dựa trên tình trạng dọn dẹp và loại phòng
                List<DanhSachThongTinPhong> filteredPhongList = FilterPhongByDonDep(selectedContent);

                // Gán danh sách phòng đã lọc cho ListView tương ứng
                switch (selectedContent)
                {
                    case "Đã dọn dẹp":
                        lvPhongDon.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đơn").ToList();
                        lvPhongDoi.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đôi").ToList();
                        lvPhongGiaDinh.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng gia đình").ToList();
                        break;
                    case "Chưa dọn dẹp":
                        lvPhongDon.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đơn").ToList();
                        lvPhongDoi.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đôi").ToList();
                        lvPhongGiaDinh.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng gia đình").ToList();
                        break;
                    case "Sửa chữa":
                        lvPhongDon.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đơn").ToList();
                        lvPhongDoi.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đôi").ToList();
                        lvPhongGiaDinh.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng gia đình").ToList();
                        break;
                    case "Tất cả":
                        // Gán danh sách phòng đã lọc cho tất cả các ListView
                        lvPhongDon.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đơn").ToList();
                        lvPhongDoi.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng đôi").ToList();
                        lvPhongGiaDinh.ItemsSource = filteredPhongList.Where(p => p.TenLoaiPhong == "Phòng gia đình").ToList();
                        break;
                    default:
                        break;
                }
            }
        }

        private List<DanhSachThongTinPhong> FilterPhongByDonDep(string tinhTrangDonDep)
        {
            switch (tinhTrangDonDep)
            {
                case "Đã dọn dẹp":
                    return bus_Phong.SelectAllPhong().Where(p => p.GhiChu == "Đã dọn dẹp").ToList();
                case "Chưa dọn dẹp":
                    return bus_Phong.SelectAllPhong().Where(p => p.GhiChu == "Chưa dọn dẹp").ToList();
                case "Sửa chữa":
                    return bus_Phong.SelectAllPhong().Where(p => p.GhiChu == "Sửa chữa").ToList();
                case "Tất cả":
                    return bus_Phong.SelectAllPhong();
                default:
                    return new List<DanhSachThongTinPhong>();
            }
        }


        //


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Xacdinhvaitro();
        }

    }
}
