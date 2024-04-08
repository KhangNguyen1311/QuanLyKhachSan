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
using System.Windows.Shapes;
using DuAn_QuanLiKhachSan.Views;
using BUS_QLKS;

namespace DuAn_QuanLiKhachSan.PageChild
{
    /// <summary>
    /// Interaction logic for themDichVu.xaml
    /// </summary>
    public partial class themDichVu : Window
    {
        public event EventHandler ChildClosed;
        private void ChildWindowClosed(object sender, EventArgs e)
        {
            /*Copy vào btn*/
            /*ChildClosed?.Invoke(this, EventArgs.Empty);*/

            loadcombo();
        }
        public static BUS_DichVu bUS_DichVu = new BUS_DichVu();
        public static BUS_LoaiDichVu bUS_LoaiDichVu = new BUS_LoaiDichVu();
        public themDichVu()
        {
            InitializeComponent();
            loadcombo();
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

        private void themLoaiDichVu_Click(object sender, RoutedEventArgs e)
        {

            themLoaiDichVu themLoaiDichVu = new themLoaiDichVu();
            themLoaiDichVu.ChildClosed += ChildWindowClosed;
            themLoaiDichVu.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            themLoaiDichVu.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TenDichVu.Text != "" && loaiDichVu_combo.Text != "" && GiaBan.Text != "")
            {


                // Parse GiaBan.Text to decimal
                double a = double.Parse(GiaBan.Text);
                string selectitem = loaiDichVu_combo.Text;


                DTO_QLKS.DichVu dichVu = new DTO_QLKS.DichVu
                {

                    TenDV = TenDichVu.Text,
                    MaLoaiDV = loaiDichVu_combo.SelectedValue.ToString(),
                    GiaTien = a,
                };

                bUS_DichVu.Insert(dichVu);
                var ThongBao = new DialogCustoms("Lưu thành công", "Thông báo", DialogCustoms.OK);
                ThongBao.ShowDialog();
                ChildClosed?.Invoke(this, EventArgs.Empty);
                TenDichVu.Text = "";
                loaiDichVu_combo.Text = "";
                GiaBan.Text = "";
            }
            else
            {
                var ThongBao = new DialogCustoms("Vui lòng nhập đầy đủ thông tin!", "Thông báo", DialogCustoms.OK);
                ThongBao.ShowDialog();

            }


        }

        private void GiaBan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the input is a valid numeric value
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Cancel the input
            }
        }

        private bool IsNumeric(string text)
        {
            double result;
            return double.TryParse(text, out result);
        }

        private void Xoa_btn_Click(object sender, RoutedEventArgs e)
        {
            TenDichVu.Text = "";
            loaiDichVu_combo.Text = "";
            GiaBan.Text = "";
        }

        private void loadcombo()
        {
            var result = bUS_LoaiDichVu.GetAll();
            loaiDichVu_combo.ItemsSource = result;
            loaiDichVu_combo.DisplayMemberPath = "TenLoaiDV";
            loaiDichVu_combo.SelectedValuePath = "MaLoaiDV";
        }

    }
}
