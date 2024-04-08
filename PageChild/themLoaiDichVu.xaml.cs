using BUS_QLKS;
using DTO_QLKS;
using DuAn_QuanLiKhachSan.Views;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for themLoaiDichVu.xaml
    /// </summary>
    public partial class themLoaiDichVu : Window
    {
        public event EventHandler ChildClosed;
        public static BUS_LoaiDichVu bUS_LoaiDichVu = new BUS_LoaiDichVu();
        public themLoaiDichVu()
        {
            InitializeComponent();
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

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tenloai_txt.Text != "")
            {


                LoaiDichVu loaiDichVu = new LoaiDichVu()
                {

                    TenLoaiDV = tenloai_txt.Text,
                };
                bUS_LoaiDichVu.Insert(loaiDichVu);
                ChildClosed?.Invoke(this, EventArgs.Empty);
                tenloai_txt.Text = "";
            }
            else
            {

                var ThongBao = new DialogCustoms("Vui lòng nhập đầy đủ thông tin!", "Thông báo", DialogCustoms.OK);
                ThongBao.ShowDialog();
            }


        }

        private void Xoa_btn_Click(object sender, RoutedEventArgs e)
        {
            tenloai_txt.Text = "";
        }
    }
}
