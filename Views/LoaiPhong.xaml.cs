using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DuAn_QuanLiKhachSan.PageChild;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BUS_QLKS;
using DTO_QLKS;
using System.Diagnostics;

namespace DuAn_QuanLiKhachSan.Views
{
    /// <summary>
    /// Interaction logic for LoaiPhong.xaml
    /// </summary>
    public partial class LoaiPhong : Page
    {
        static DTO_QLKS.NhanVien nhanvien;
        static BUS_LoaiPhong BUS_LoaiPhong = new BUS_LoaiPhong();

        public LoaiPhong(DTO_QLKS.NhanVien nv)
        {
            InitializeComponent();
            nhanvien = nv;
            loaddata();

        }
        public void Xacdinhvaitro()
        {
            if (nhanvien.VaiTro.ToLower() != "quản lý")
            {
                btn_themLoaiPhong.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_themLoaiPhong.Visibility = Visibility.Visible;
            }
        }
        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Xacdinhvaitro();
        }

        private void btn_themLoaiPhong_Click(object sender, RoutedEventArgs e)
        {
            themLoaiPhong loaiPhong = new themLoaiPhong();
            loaiPhong.ChildClosed += ChildWindowClosed;

            loaiPhong.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            loaiPhong.Show();
        }


        public void loaddata()
        {
            List<DTO_QLKS.LoaiPhong> loaiphong = BUS_LoaiPhong.SelectAll();
            DatagridLoaiPhong.ItemsSource = loaiphong;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = DatagridLoaiPhong.SelectedIndex;
            var row = (DataGridRow)DatagridLoaiPhong.ItemContainerGenerator.ContainerFromIndex(index);
            if (row != null)
            {
                var cell = DatagridLoaiPhong.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    string malp = cell.Text;
                    DTO_QLKS.LoaiPhong lp = BUS_LoaiPhong.SelectAll().Where(c => c.MaLoaiPhong == malp).FirstOrDefault();
                    BUS_LoaiPhong.Xoa(lp);
                    var tb = new DialogCustoms("Xoá thành công", "Thông báo", DialogCustoms.OK);
                    tb.ShowDialog();

                }
            }
            loaddata();
        }
        private void ChildWindowClosed(object sender, EventArgs e)
        {
            // Handle child window closed event here
            loaddata();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            int index = DatagridLoaiPhong.SelectedIndex;

            var row = (DataGridRow)DatagridLoaiPhong.ItemContainerGenerator.ContainerFromIndex(index);
            if (row != null)
            {
                Debug.WriteLine("CC");
                var cell = DatagridLoaiPhong.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    string text = cell.Text;
                    EditLoaiPhong edit = new EditLoaiPhong(text);
                    edit.ChildClosed += ChildWindowClosed;
                    edit.Show();

                }
            }
        }

        private void txt_searchRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            string mlp = txt_searchRoom.Text.ToLower();
            List<DTO_QLKS.LoaiPhong> loaiPhongs = BUS_LoaiPhong.SelectAll().Where(c=>c.MaLoaiPhong.ToLower().Contains(mlp)).ToList();
            DatagridLoaiPhong.ItemsSource = loaiPhongs;
        }
    }
}
