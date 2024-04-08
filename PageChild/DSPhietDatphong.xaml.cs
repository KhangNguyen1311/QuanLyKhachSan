using BUS_QLKS;
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
    /// Interaction logic for DSPhietDatphong.xaml
    /// </summary>
    public partial class DSPhietDatphong : Window
    {

        static string mpdp;
        static BUS_ChiTietPhieuDatPhong bUS_ChiTietphieudatphong = new BUS_ChiTietPhieuDatPhong();

        public DSPhietDatphong(string mapdp)
        {
            InitializeComponent();
            mpdp = mapdp;
            loaddata();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ChildWindowClosed(object sender, EventArgs e)
        {
            // Handle child window closed event here
            loaddata();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void loaddata()
        {
            List<DTO_QLKS.ChiTietPhieuDatPhong> datPhong = bUS_ChiTietphieudatphong.SelectAll().Where(c => c.MaPDP == mpdp).ToList();
            danhSachPhongTrong.ItemsSource = datPhong;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = danhSachPhongTrong.SelectedIndex;

            var row = (DataGridRow)danhSachPhongTrong.ItemContainerGenerator.ContainerFromIndex(index);
            if (row != null)
            {
                Debug.WriteLine("CC");
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
    }
}
