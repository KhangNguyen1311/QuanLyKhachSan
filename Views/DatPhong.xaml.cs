using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuAn_QuanLiKhachSan.PageChild;
using DuAn_QuanLiKhachSan.Login;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using BUS_QLKS;
using DTO_QLKS;

namespace DuAn_QuanLiKhachSan.Views
{
    /// <summary>
    /// Interaction logic for DatPhong.xaml
    /// </summary>
    public partial class DatPhong : Page
    {
        static BUS_DatPhong BUS_datPhong = new BUS_DatPhong();
        static BUS_ChiTietPhieuDatPhong bUS_ChiTietPhieuDatPhong = new BUS_ChiTietPhieuDatPhong();
        static DTO_QLKS.NhanVien nhanVien= new DTO_QLKS.NhanVien();
        public DatPhong(DTO_QLKS.NhanVien nv)
        {
            InitializeComponent();
            nhanVien = nv;
            loaddata();
        }
       

        private void btn_datPhong_Click(object sender, RoutedEventArgs e)
        {
            phieuDatPhong phieuDatPhong = new phieuDatPhong(nhanVien);
            phieuDatPhong.ChildClosed += ChildWindowClosed;
            phieuDatPhong.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            phieuDatPhong.Show();
        }

        public void loaddata()
        {
            List<DTO_QLKS.PhieuDatPhong> datPhong = BUS_datPhong.Selectall();
            dataGriddatphong.ItemsSource = datPhong;
        }
        private void ChildWindowClosed(object sender, EventArgs e)
        {
            // Handle child window closed event here
            loaddata();
        }

        private void button_remove(object sender, RoutedEventArgs e)
        {
            int slindex = dataGriddatphong.SelectedIndex;
            var row = (DataGridRow)dataGriddatphong.ItemContainerGenerator.ContainerFromIndex(slindex);
            if (row != null)
            {
                var cell = dataGriddatphong.Columns[0].GetCellContent(row) as TextBlock;
                if (cell != null)
                {
                    string mapdp = cell.Text;

                    List<ChiTietPhieuDatPhong> ctpdp = bUS_ChiTietPhieuDatPhong.SelectAll().Where(c=>c.MaPDP==mapdp).ToList();
                    if (ctpdp.Count != 0)
                    {
                        var tb = new DialogCustoms("Vui lòng xoá chi tiết phiếu đặt phòng trước!", "Thông báo!", DialogCustoms.OK);
                        tb.ShowDialog();
                    }
                    else
                    {
                        DTO_QLKS.PhieuDatPhong dp = BUS_datPhong.Selectall().Where(c => c.MaPDP == mapdp).FirstOrDefault();
                        BUS_datPhong.xoa(dp);
                        var thongba = new DialogCustoms("Xoá thành công", "Thông báo", DialogCustoms.OK);
                        thongba.ShowDialog();
                    }
                }
            }
            loaddata();
        }

        private void button_edit(object sender, RoutedEventArgs e)
        {
            int index = dataGriddatphong.SelectedIndex;

            var row = (DataGridRow)dataGriddatphong.ItemContainerGenerator.ContainerFromIndex(index);
            if (row != null)
            {
                var cell = dataGriddatphong.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    string text = cell.Text;
                    chiTietPhieuThue edit = new chiTietPhieuThue(text);
                    edit.Show();
                }
            }
        }

        private void txt_searchRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            string mapdp = txt_searchRoom.Text.ToLower();
            List<PhieuDatPhong> pdp = BUS_datPhong.Selectall().Where(c=>c.MaPDP.ToLower().Contains(mapdp)).ToList();
            dataGriddatphong.ItemsSource = pdp;
        }
    }

}
