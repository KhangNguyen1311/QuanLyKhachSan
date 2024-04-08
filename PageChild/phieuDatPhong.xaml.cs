﻿using BUS_QLKS;
using DTO_QLKS;
using DuAn_QuanLiKhachSan.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for phieuDatPhong.xaml
    /// </summary>
    public partial class phieuDatPhong : Window
    {
        static BUS_LoaiPhong bUS_LoaiPhong = new BUS_LoaiPhong();
        static BUS_Phong bUS_Phong = new BUS_Phong();
        static BUS_PhieuDatPhong bUS_PhieuDatPhong = new BUS_PhieuDatPhong();
        static BUS_NhanVien bUS_NhanVien = new BUS_NhanVien();
        static BUS_KhachHang bUS_khachhang = new BUS_KhachHang();
        static List<DTO_QLKS.Phong> phongtrongs;
        static BUS_ChiTietPhieuDatPhong bUS_ChiTietphieudatphong = new BUS_ChiTietPhieuDatPhong();
        public event EventHandler ChildClosed;

        public phieuDatPhong()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ChildClosed.Invoke(this, EventArgs.Empty);
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
            if (ngaydat.SelectedDate != null && ngayketthuc.SelectedDate != null && giodat.SelectedTime.HasValue && gioketthuc.SelectedTime.HasValue)
            {
                phongtrongs = bUS_Phong.GetAll();
                DateTime? ngayDat = (DateTime)ngaydat.SelectedDate;
                DateTime? ngayKetThuc = (DateTime)ngayketthuc.SelectedDate;
                DateTime temp1 = (DateTime)giodat.SelectedTime;
                DateTime temp2 = (DateTime)gioketthuc.SelectedTime;
                TimeSpan? gioDat = temp1.TimeOfDay;
                TimeSpan? gioKetThuc = temp2.TimeOfDay;
                DateTime ngayGioDat = ngayDat.Value.Date + gioDat.Value;
                DateTime ngayGioKetThuc = ngayKetThuc.Value.Date + gioKetThuc.Value;
                List<ChiTietPhieuDatPhong> chiTietPhieuDatPhongs = bUS_ChiTietphieudatphong.SelectAll().Where(c => c.NgayDat<=ngayKetThuc&&c.NgayKetThuc>=ngayDat).ToList();
                foreach(var item in chiTietPhieuDatPhongs)
                {
                    phongtrongs.RemoveAll(c => c.MaPhong == item.MaPhong);
                }

                danhSachPhongTrong.ItemsSource = phongtrongs;
                danhSachPhongTrong.Items.Refresh();
            }
        }
        public void loadcombobox_nhanvien()
        {
            List<DTO_QLKS.NhanVien> nhanviens = bUS_NhanVien.SelectAll();
            Debug.WriteLine(nhanviens.Count.ToString());
            nhanvien_box.ItemsSource = nhanviens;
            nhanvien_box.DisplayMemberPath = "TenNV";
            nhanvien_box.SelectedValuePath = "MaNV";


        }
        public void loadcombobox_khachhang()
        {
            List<DTO_QLKS.KhachHang> khachHangs = bUS_khachhang.SelectAll();
            Debug.WriteLine(khachHangs.Count.ToString());
            khachhang_box.ItemsSource = khachHangs;
            khachhang_box.DisplayMemberPath = "TenKH";
            khachhang_box.SelectedValuePath = "MaKH";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadcombobox_nhanvien();
            loadcombobox_khachhang();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int rowindex = danhSachPhongTrong.SelectedIndex;

            var row = (DataGridRow)danhSachPhongTrong.ItemContainerGenerator.ContainerFromIndex(rowindex);
            if (row != null)
            {
                var cell = danhSachPhongTrong.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    string text = cell.Text;

                    DTO_QLKS.Phong phong = bUS_Phong.GetAll().Where(c => c.MaPhong == cell.Text).FirstOrDefault();
                    DTO_QLKS.LoaiPhong loaiPhong = bUS_LoaiPhong.SelectAll().Where(x => x.MaLoaiPhong == phong.MaLoaiPhong).FirstOrDefault();
                    DateTime ngayDat = (DateTime)ngaydat.SelectedDate;
                    DateTime gioDat = (DateTime)giodat.SelectedTime;
                    DateTime ngayKetThuc = (DateTime)ngayketthuc.SelectedDate;
                    DateTime gioKetThuc = (DateTime)gioketthuc.SelectedTime;

                    // Tính toán chênh lệch ngày và giờ
                    TimeSpan chenhLechNgay = ngayKetThuc.Date - ngayDat.Date;
                    TimeSpan chenhLechGio = gioKetThuc.TimeOfDay - gioDat.TimeOfDay;
                    int dateDifferenceInDays = Convert.ToInt32(chenhLechNgay.TotalDays);
                    int timeDifferenceInHours = Convert.ToInt32(chenhLechGio.TotalHours);
                    Debug.WriteLine(dateDifferenceInDays);
                    Debug.WriteLine(timeDifferenceInHours);



                    double tonggiatri = loaiPhong.GiaTheoGio * timeDifferenceInHours + loaiPhong.GiaTheoNgay * dateDifferenceInDays;
                    ChiTietPhieuDatPhong chiTietPhieuDatPhong = new ChiTietPhieuDatPhong
                    {
                        MaPhong = phong.MaPhong,
                        GioDat = gioDat.TimeOfDay,
                        GioKetThuc = gioKetThuc.TimeOfDay,
                        NgayDat = ngayDat,
                        NgayKetThuc = ngayKetThuc,
                        TongGiaTri = tonggiatri
                    };

                    PhongChon.Items.Add(chiTietPhieuDatPhong);



                    phongtrongs.RemoveAll(c => c.MaPhong.Equals(text));
                    danhSachPhongTrong.Items.Refresh();

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int rowindex = PhongChon.SelectedIndex;

            var row = (DataGridRow)PhongChon.ItemContainerGenerator.ContainerFromIndex(rowindex);
            if (row != null)
            {
                var cell = PhongChon.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    string text = cell.Text;
                    DTO_QLKS.Phong phong = bUS_Phong.GetAll().Where(c => c.MaPhong == text).FirstOrDefault();
                    phongtrongs.Add(phong);
                    danhSachPhongTrong.Items.Refresh();

                    foreach (var row1 in PhongChon.Items)
                    {
                        var dataRow = PhongChon.ItemContainerGenerator.ContainerFromItem(row1) as DataGridRow;
                        if (dataRow != null && (dataRow.Item as ChiTietPhieuDatPhong)?.MaPhong == text)
                        {
                            PhongChon.Items.Remove(row1);
                            break;
                        }
                    }
                    PhongChon.Items.Refresh();

                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DTO_QLKS.PhieuDatPhong phieu = new DTO_QLKS.PhieuDatPhong
            {
                MaKH = khachhang_box.SelectedValue.ToString(),
                MaNV = nhanvien_box.SelectedValue.ToString(),
            };
            bUS_PhieuDatPhong.Insert(phieu);

            DTO_QLKS.PhieuDatPhong ph = bUS_PhieuDatPhong.SelectAll().LastOrDefault();
            for (int i = 0; i < PhongChon.Items.Count; i++)
            {
                ChiTietPhieuDatPhong temp = new ChiTietPhieuDatPhong();
                temp.MaPDP = ph.MaPDP;
                temp.SoNguoi = int.Parse(txt_songuoi.Text);
                var row = (DataGridRow)PhongChon.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {
                    var cell = PhongChon.Columns[0].GetCellContent(row) as TextBlock;
                    if (cell != null)
                    {
                        temp.MaPhong = cell.Text;
                    }
                    cell = PhongChon.Columns[1].GetCellContent(row) as TextBlock;
                    if (cell != null)
                    {
                        temp.NgayDat = DateTime.Parse(cell.Text);
                    }
                    cell = PhongChon.Columns[2].GetCellContent(row) as TextBlock;
                    if (cell != null)
                    {
                        temp.NgayKetThuc = DateTime.Parse(cell.Text);
                    }
                    cell = PhongChon.Columns[3].GetCellContent(row) as TextBlock;
                    if (cell != null)
                    {
                        DateTime temp1 = DateTime.Parse(cell.Text);
                        temp.GioDat = temp1.TimeOfDay;
                    }
                    cell = PhongChon.Columns[4].GetCellContent(row) as TextBlock;
                    if (cell != null)
                    {
                        DateTime temp1 = DateTime.Parse(cell.Text);
                        temp.GioKetThuc = temp1.TimeOfDay;
                    }
                    cell = PhongChon.Columns[5].GetCellContent(row) as TextBlock;
                    if (cell != null)
                    {
                        temp.TongGiaTri = float.Parse(cell.Text);
                    }
                }
                bUS_ChiTietphieudatphong.Insert(temp);
            }
         

        }

        private void ngaydat_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loaddata();
        }

        private void giodat_LostFocus(object sender, RoutedEventArgs e)
        {
            loaddata();
        }

        private void gioketthuc_LostFocus(object sender, RoutedEventArgs e)
        {
            loaddata();
        }
        private void btn_datPhong_Click(object sender, RoutedEventArgs e)
        {
            themkhachhangg kh = new themkhachhangg();
            kh.ChildClosed += ChildWindowClosed;
            kh.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            kh.Show();
        }
    }
}