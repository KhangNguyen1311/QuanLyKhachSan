using BUS_QLKS;
using DTO_QLKS;
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
using System.Windows.Shapes;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for chiTietPhieuThue.xaml
    /// </summary>
    public partial class chiTietPhieuThue : Window
    {


        static string mpdp;
        static BUS_ChiTietPhieuDatPhong bUS_ChiTietphieudatphong = new BUS_ChiTietPhieuDatPhong();
        static BUS_PhieuDatPhong bUS_PhieuDatPhong = new BUS_PhieuDatPhong();
        static BUS_NhanVien bUS_NhanVien = new BUS_NhanVien();
        static BUS_KhachHang bUS_KhachHang = new BUS_KhachHang();

        class CTPDP_Item
        {
            public string mapdp { get; set; }
            public string map { get; set; }
            public double tonggiatri { get; set; }
            public string ngaydat { get; set; }
            public string ngayketthuc { get; set; }
            public string giodat { get; set; }
            public string gioketthuc { get; set; }
            public string tinhtrang { get; set; }
            public int songuoi { get; set; }
        }




        public chiTietPhieuThue(string mapdp)
        {
            InitializeComponent();
            mpdp = mapdp;
            loaddata();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void ChildWindowClosed(object sender, EventArgs e)
        {
            // Handle child window closed event here
            loaddata();
        }


        public void loaddata()
        {
            PhieuDatPhong ph = bUS_PhieuDatPhong.SelectAll().Where(c=>c.MaPDP==mpdp).FirstOrDefault();
            DTO_QLKS.NhanVien nv = bUS_NhanVien.SelectAll().Where(c=>c.MaNV == ph.MaNV).FirstOrDefault();
            DTO_QLKS.KhachHang kh = bUS_KhachHang.SelectAll().Where(c=>c.MaKH == ph.MaKH).FirstOrDefault();
            txt_nhanvien.Text = nv.TenNV;
            txt_khachhang.Text = kh.TenKH;


            List<DTO_QLKS.ChiTietPhieuDatPhong> temp = bUS_ChiTietphieudatphong.SelectAll().Where(c => c.MaPDP == mpdp).ToList();
            List<CTPDP_Item> datPhongs = new List<CTPDP_Item>();
            foreach(ChiTietPhieuDatPhong item in temp)
            {
                datPhongs.Add(new CTPDP_Item
                {
                    mapdp = item.MaPDP,
                    map = item.MaPhong,
                    tonggiatri = item.TongGiaTri,
                    ngaydat = item.NgayDat.ToString("dd/MM/yyyy"),
                    ngayketthuc = item.NgayKetThuc.ToString("dd/MM/yyyy"),
                    giodat = item.GioDat.ToString(),
                    gioketthuc = item.GioKetThuc.ToString(),
                    tinhtrang = item.TinhTrang,
                    songuoi = item.SoNguoi,
                });
            }
            danhSachPhongTrong.ItemsSource = datPhongs;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = danhSachPhongTrong.SelectedIndex;

            var row = (DataGridRow)danhSachPhongTrong.ItemContainerGenerator.ContainerFromIndex(index);
            if (row != null)
            {
               
                string mpdp = "";
                string mp = "";

                var cell = danhSachPhongTrong.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    mpdp = cell.Text;

                }
                cell = danhSachPhongTrong.Columns[1].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    mp = cell.Text;

                }

                EditPhieudatphong editPhieudatphong = new EditPhieudatphong(mpdp, mp);
                editPhieudatphong.ChildClosed += ChildWindowClosed;
                editPhieudatphong.Show();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int index = danhSachPhongTrong.SelectedIndex;
            var row = (DataGridRow)danhSachPhongTrong.ItemContainerGenerator.ContainerFromIndex(index);
            if (row != null)
            {

                string mpdp = "";
                string mp = "";

                var cell = danhSachPhongTrong.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    mpdp = cell.Text;

                }
                cell = danhSachPhongTrong.Columns[1].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    mp = cell.Text;

                }
                ChiTietPhieuDatPhong ctpdp = bUS_ChiTietphieudatphong.SelectAll().Where(c=>c.MaPDP.Equals(mpdp)&&c.MaPhong.Equals(mp)).FirstOrDefault();
                bUS_ChiTietphieudatphong.Delete(ctpdp);
                var tb = new DialogCustoms("Xoá thành công!", "Thông báo!", DialogCustoms.OK);
                tb.ShowDialog();

            }
        }
    }
}
