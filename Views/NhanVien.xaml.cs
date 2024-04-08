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
using System.CodeDom;
using System.Diagnostics;
using BUS_QLKS;
using DTO_QLKS;

namespace DuAn_QuanLiKhachSan.Views
{

    /// <summary>
    /// Interaction logic for NhanVien.xaml
    /// </summary>
    public partial class NhanVien : Page
    {
        public static BUS_NhanVien bUS_NhanVien = new BUS_NhanVien();
        public NhanVien()
        {
            InitializeComponent();
            taidulieulengrid();
        }
        public void taidulieulengrid()
        {
            List<DanhSachNhanVien> nhanViens = bUS_NhanVien.laytatca();
            datagrid.ItemsSource = nhanViens;
        }
        private void ChildWindowClosed(object sender, EventArgs e)
        {
            // Handle child window closed event here
            taidulieulengrid();
        }

        private void btn_nhanVien_Click(object sender, RoutedEventArgs e)
        {
            themNhanVien themNhanVien = new themNhanVien();
            themNhanVien.ChildClosed += ChildWindowClosed;
            themNhanVien.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            themNhanVien.Show();
        }
        private void txtTim_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtTim.Text.ToLower();
            List<DanhSachNhanVien> timkiem = bUS_NhanVien.laytatca().Where(c => c.TenNV.ToLower().Contains(searchText)).ToList();
            datagrid.ItemsSource = timkiem;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = datagrid.SelectedIndex;
            /*chọn hàng theo stt*/
            var row = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row != null)
            {
                var cell = datagrid.Columns[0].GetCellContent(row) as TextBlock;

                if (cell != null)
                {
                    thongTinNVien thongTinNVien = new thongTinNVien(cell.Text);
                    thongTinNVien.ChildClosed += ChildWindowClosed;
                    thongTinNVien.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    thongTinNVien.Show();
                }
            }
        }
    }
}
