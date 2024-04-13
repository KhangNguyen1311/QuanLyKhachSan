using BUS_QLKS;
using DTO_QLKS;
using DuAn_QuanLiKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for xuatHoaDon.xaml
    /// </summary>
    public partial class xuatHoaDon : Window
    {


        static BUS_ChiTietDichVuPhieuDatPhong bus_chiTietDichVuPDP = new BUS_ChiTietDichVuPhieuDatPhong();
        static BUS_HoaDon bus_hoaDon = new BUS_HoaDon();
        static BUS_Phong bus_Phong = new BUS_Phong();
        List<DanhsachDv> dvItem = new List<DanhsachDv>();
        private string MaPhong { get; set; }
        private string MaPDP { get; set; }

        public xuatHoaDon()
        {
            InitializeComponent();
            DataContext = this;

        }
        public xuatHoaDon(string maPhong, string maPDP) : this()
        {
            var chiTietPhong = bus_Phong.SelectAllPhong().Where(p => p.MaPhong == maPhong && p.MaPDP==maPDP).FirstOrDefault();
            int SoNgay = (int)chiTietPhong.SoNgay;
            int SoGio = (int)chiTietPhong.SoGio;
            MaPhong = maPhong;
            MaPDP = maPDP;

            decimal giaTheoGio = 0;
            decimal giaTheoNgay = 0;
            List<ThongTinHoaDon> thongTinHD = bus_hoaDon.SelectChiTietHD().Where(c => c.MaPhong.Equals(MaPhong) && c.MaPDP.Equals(MaPDP)).ToList();
            foreach (ThongTinHoaDon item in thongTinHD)
            {
                giaTheoGio = (decimal)item.GiaTheoGio;
                giaTheoNgay = (decimal)item.GiaTheoNgay;
                txbMaHoaDon.Text = item.MaHD;
                txbSoNguoi.Text = item.SoNguoi.ToString();
                txbTongTien.Text = item.TongGiaTri.ToString("N0") + " VNĐ";
                txbSoPhong.Text = MaPhong;
                if (SoGio >= 24)
                {
                    txbSoNgayOrGio.Text = "Số ngày: ";
                    txbSoNgay.Text = SoNgay.ToString();
                }
                else
                {
                    txbSoNgayOrGio.Text = "Số giờ: ";
                    txbSoNgay.Text = SoGio.ToString();
                }

                //truyền dữ liệu dịch vụ sử dụng vào lớp ObservableCollection
                DanhsachDv dichvuItem = new DanhsachDv
                {
                    tenDV = item.TenDV,
                    soLuong = item.SoLuongDV.ToString(),
                    giaTien = item.GiaTien.ToString(),
                    tongTien = item.ThanhTien.ToString(),
                };
                dvItem.Add(dichvuItem);
            }

            //thêm dịch vụ thuê phòng
            decimal giaPhong;
            decimal tongTien;
            if (SoGio >= 24)
            {
                // Nếu số ngày tồn tại, sử dụng số ngày và giá theo ngày
                giaPhong = giaTheoNgay;
                tongTien = giaTheoNgay * SoNgay;
            }
            else
            {
                // Nếu số ngày không tồn tại, sử dụng số giờ và giá theo giờ
                giaPhong = giaTheoGio;
                tongTien = giaTheoGio * SoGio;
            }

            DanhsachDv dichvuThuePhong = new DanhsachDv
            {
                tenDV = "Thuê phòng",
                soLuong = SoGio >= 24 ? SoNgay.ToString() : SoGio.ToString(),
                giaTien = giaPhong.ToString(),
                tongTien = tongTien.ToString(), // Tính tổng tiền dựa trên giá tiền và số lượng
            };
            // Thêm đối tượng vào danh sách
            dvItem.Add(dichvuThuePhong);
            lvDichVuDaSD.ItemsSource = dvItem;
            txbNgayLapHD.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnInHoaDon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnInHoaDon.Visibility = Visibility.Hidden;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    DTO_QLKS.HoaDon hoaDon = bus_hoaDon.GetAll().Where(c => c.MaPDP.Equals(MaPDP) && c.MaPhong.Equals(MaPhong)).FirstOrDefault();
                    if (hoaDon != null)
                    {
                        hoaDon.TinhTrang = "Đã thanh toán";
                        bus_hoaDon.Update(hoaDon);
                        printDialog.PrintVisual(print, "In Hóa đơn");
                        new DialogCustoms("In hóa đơn thành công!", "Thông báo", DialogCustoms.OK).ShowDialog();
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window != null && window != this && window.GetType() != typeof(MainWindow) && window.Visibility == Visibility.Visible)
                            {
                                window.Close();
                                this.Close();
                            }
                        }
                        try
                        {
                            DTO_QLKS.Phong phong = bus_Phong.SelectPhong().Where(c => c.MaPhong.Equals(MaPhong)).FirstOrDefault();
                            if (phong != null)
                            {
                                phong.TinhTrang = "Phòng trống";
                                bus_Phong.UpdatePhong(phong);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new DialogCustoms("In hóa đơn thất bại! \n Lỗi: " + ex.Message, "Thông báo", DialogCustoms.OK).ShowDialog();
            }
            finally
            {
                btnInHoaDon.Visibility = Visibility.Visible;
            }
        }

    }
    public class DanhsachDv
    {
        public string tenDV { get; set; }
        public string giaTien { get; set; }
        public string soLuong { get; set; }
        public string tongTien { get; set; }
    }
}
