using BUS_QLKS;
using DTO_QLKS;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace DuAn_QuanLiKhachSan.Views
{
    /// <summary>
    /// Interaction logic for thongKe.xaml
    /// </summary>
    public partial class thongKe : Page
    {
        public class MonthItem
        {
            public int MonthNumber { get; set; }
            public string MonthName { get; set; }
        }

        public class DoanhThuHDNam
        {
            public int nam { get; set; }
            public int thang { get; set; }
            public int tonggiatri { get; set;}
        }
        public SeriesCollection senderChart { get; set; }
        public List<string> Labels { get; set; }

        static BUS_PhieuDatPhong bUS_PhieuDatPhong = new BUS_PhieuDatPhong();
        static BUS_ChiTietDichVuPhieuDatPhong bUS_ChiTietDichVuPhieuDatPhong = new BUS_ChiTietDichVuPhieuDatPhong();
        static BUS_ChiTietPhieuDatPhong bUS_ChiTietPhieuDatPhong = new BUS_ChiTietPhieuDatPhong();
        static BUS_HoaDon bUS_HoaDon = new BUS_HoaDon();

        public thongKe()
        {
            InitializeComponent();
            LoadInfoCards();
            
        }
        public void LoadInfoCards()
        {
            int dtdv = (int)Math.Round(bUS_ChiTietDichVuPhieuDatPhong.GetTongDoanhThu());
            int dtp = (int)Math.Round((bUS_ChiTietPhieuDatPhong.GetDoanhThuPDP() - bUS_ChiTietDichVuPhieuDatPhong.GetTongDoanhThu()));
            int slp = bUS_HoaDon.GetAll().Count();
            ic_dtdv.Number = string.Format("{0:N0} VND", dtdv);
            ic_dtp.Number = string.Format("{0:N0} VND", dtp);
            ic_slpdp.Number  =  slp.ToString()+"+";
        }

        public void Loadcombo_box()
        {
            //load combobox chọn tháng
            List<MonthItem> list = new List<MonthItem>();
            for(int i = 1;i<=12;i++) 
            {
                list.Add(new MonthItem { MonthName = "Tháng "+i.ToString(),MonthNumber = i });
            }
            chonThang.ItemsSource = list;
            chonThang.DisplayMemberPath = "MonthName";
            chonThang.SelectedValuePath = "MonthNumber";
            chonThang.SelectedValue = DateTime.Now.Month;
            //load combobox chọn năm
            List<int> years = new List<int>();
            for(int x = 2020;x<= DateTime.Now.Year; x++)
            {
                years.Add(x);
            }
            chonNam.ItemsSource = years;
            chonNam.SelectedItem = DateTime.Now.Year;
        }

        public void loadchart()
        {
            bieudo1.AxisX.Add(new Axis
            {
                Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" } // Thay đổi nhãn trục X ở đây
            });
            bieudo1.AxisY.Add(new Axis
            {
                Title = "Doanh Thu (VND)",
                LabelFormatter = value => value.ToString("N0")
            });
        }

        public void loadpiechart(int thang,int nam)
        {
            List<ChiTietPhieuDatPhong> temp = bUS_ChiTietPhieuDatPhong.SelectAll();

            int doanhthudv = (int)Math.Round(bUS_ChiTietDichVuPhieuDatPhong.GetTongDoanhThuByThangNam(thang, nam));
            int doanhthuphong = (int)Math.Round(bUS_ChiTietPhieuDatPhong.GetTongDoanhThuByThangNam(thang, nam) - bUS_ChiTietDichVuPhieuDatPhong.GetTongDoanhThuByThangNam(thang, nam));


            SeriesCollection seriesViews = new SeriesCollection();
            seriesViews.Add(new PieSeries
            {
                Title="Doanh thu phòng",
                Values= new ChartValues<int> { doanhthuphong },
            });

            seriesViews.Add(new PieSeries
            {
                Title = "Doanh thu dịch vụ",
                Values = new ChartValues<int> { doanhthudv },
            });
            piechart.Series = seriesViews;
        }
        public void loadvaluechart(int nam)
        {
            List<ChiTietPhieuDatPhong> temp = bUS_ChiTietPhieuDatPhong.SelectAll();

            List<DoanhThuHDNam> tongdt = (from c in temp
                                          join pdp in bUS_PhieuDatPhong.GetAll() on c.MaPDP equals pdp.MaPDP
                                          where pdp.NgayTao.Year == nam
                                          group c by pdp.NgayTao.Month into g
                                          select new DoanhThuHDNam
                                          {
                                              thang = g.Key,
                                              tonggiatri = (int)g.Sum(x=>x.TongGiaTri)
                                          }).ToList();




            List<DoanhThuHDNam> doanhthudv = (from cdv in bUS_ChiTietDichVuPhieuDatPhong.GetAll()
                                              join pdp in bUS_PhieuDatPhong.GetAll() on cdv.MaPDP equals pdp.MaPDP
                                              where pdp.NgayTao.Year == nam
                                              group cdv by pdp.NgayTao.Month into g
                                              select new DoanhThuHDNam
                                              {
                                                  thang = g.Key,
                                                  tonggiatri = (int)g.Sum(x => x.TongGiaTri)
                                              }).ToList();






            List<DoanhThuHDNam> doanhthuphong = new List<DoanhThuHDNam>();
            for (int i = 0; i < tongdt.Count; i++)
            {
                doanhthuphong.Add(new DoanhThuHDNam
                {
                    thang = tongdt[i].thang,
                    tonggiatri = tongdt[i].tonggiatri - doanhthudv[i].tonggiatri
                });
            }



            ChartValues<int> chartValues1 = new ChartValues<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ChartValues<int> chartValues2 = new ChartValues<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (var item in doanhthuphong)
            {
                Debug.WriteLine("doanh thu phòng" + item.tonggiatri);

                chartValues1[item.thang - 1] = item.tonggiatri;
            }
            foreach (var item in doanhthudv)
            {
                Debug.WriteLine("doanh thu dịch vụ" + item.tonggiatri);
                chartValues2[item.thang - 1] = item.tonggiatri;
            }
            SeriesCollection series = new SeriesCollection
            {
                new LineSeries()
                {
                    Title = "Doanh Thu phòng",
                    Values = chartValues1
                },
                new LineSeries()
                {
                    Title = "Doanh Thu dịch vụ",
                    Values = chartValues2
                },
            };

            bieudo1.Series = series;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Loadcombo_box();
            loadchart();
        }

        private void chonNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadvaluechart(int.Parse(chonNam.SelectedValue.ToString()));
        }

        private void chonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadpiechart((int)chonThang.SelectedValue, DateTime.Now.Year);

        }
    }
}
