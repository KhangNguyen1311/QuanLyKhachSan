
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for EditLoaiPhong.xaml
    /// </summary>
    public partial class EditLoaiPhong : Window
    {
        public event EventHandler ChildClosed;
        static BUS_LoaiPhong bus_loaiphong = new BUS_LoaiPhong();
        static string maloaip;
        public EditLoaiPhong(string mlp)
        {
            InitializeComponent();
            maloaip = mlp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DTO_QLKS.LoaiPhong loaip = new DTO_QLKS.LoaiPhong
            {
                MaLoaiPhong = maloaip,
                TenLoaiPhong = txt_tenlp.Text,
                GiaTheoGio = double.Parse(txt_giagio.Text),
                GiaTheoNgay = double.Parse(giangay.Text),
            };
            bus_loaiphong.Update(loaip);
            ChildClosed.Invoke(this, EventArgs.Empty);
            var ThongBao1 = new DialogCustoms("Lưu thành công!", "Thông báo", DialogCustoms.OK);
            ThongBao1.ShowDialog();
        }
        public void loadata()
        {
            DTO_QLKS.LoaiPhong lp = bus_loaiphong.SelectAll().Where(c=>c.MaLoaiPhong==maloaip).FirstOrDefault();
            txt_tenlp.Text = lp.TenLoaiPhong;
            maloaiphong.Text = lp.MaLoaiPhong;
            txt_giagio.Text = lp.GiaTheoGio.ToString();
            giangay.Text = lp.GiaTheoNgay.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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
    }
}
