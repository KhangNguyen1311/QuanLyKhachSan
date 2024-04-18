using BUS_QLKS;
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
    /// Interaction logic for themLoaiPhong.xaml
    /// </summary>
    public partial class themLoaiPhong : Window
    {
        static BUS_LoaiPhong bUS_LoaiPhong = new BUS_LoaiPhong();
        public event EventHandler ChildClosed;

        public themLoaiPhong()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ChildClosed?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txt_tenlp.Text.Length == 0 || txt_giagio.Text.Length == 0 || giangay.Text.Length == 0)
            {
                var tb = new DialogCustoms("Không để trống thông tin!","Thông báo",DialogCustoms.OK);
                tb.ShowDialog();
            }
            else
            {
                DTO_QLKS.LoaiPhong loaiPhong = new DTO_QLKS.LoaiPhong
                {
                    TenLoaiPhong = txt_tenlp.Text,
                    GiaTheoGio = double.Parse(txt_giagio.Text),
                    GiaTheoNgay = double.Parse(giangay.Text)
                };
                bUS_LoaiPhong.Insert(loaiPhong);
                var ThongBao1 = new DialogCustoms("Thêm loại phòng thành công", "Thông báo", DialogCustoms.OK);
                ThongBao1.ShowDialog();
            }
        }
    }
}
