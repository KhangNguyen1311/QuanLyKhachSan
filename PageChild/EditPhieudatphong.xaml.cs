using BUS_QLKS;
using DuAn_QuanLiKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for EditPhieudatphong.xaml
    /// </summary>
    public partial class EditPhieudatphong : Window
    {
        public event EventHandler ChildClosed;
        static BUS_ChiTietPhieuDatPhong bUS_ChiTietphieudatphong = new BUS_ChiTietPhieuDatPhong();
        static BUS_PhieuDatPhong BUS_PhieuDatPhong = new BUS_PhieuDatPhong();
        static string MaPDPhong;
        static string MaP;

        public EditPhieudatphong(string mapdp, string map)
        {
            InitializeComponent();
            MaPDPhong = mapdp;
            MaP = map;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            ChildClosed?.Invoke(this, EventArgs.Empty);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime gd = (DateTime)giodat.SelectedTime;
            DateTime gkt = (DateTime)gioketthuc.SelectedTime;
            DTO_QLKS.ChiTietPhieuDatPhong cc = bUS_ChiTietphieudatphong.SelectAll().Where(c => c.MaPhong == MaP && c.MaPDP == MaPDPhong).FirstOrDefault();
            cc.NgayDat = (DateTime)ngaydat.SelectedDate;
            cc.NgayKetThuc = (DateTime)ngayketthuc.SelectedDate;
            cc.GioDat = gd.TimeOfDay;
            cc.GioKetThuc = gkt.TimeOfDay;
            bUS_ChiTietphieudatphong.Update(cc);
            var ThongBao1 = new DialogCustoms("Cập nhập thành công", "Thông báo", DialogCustoms.OK);
            ThongBao1.ShowDialog();
        }


    }
}
