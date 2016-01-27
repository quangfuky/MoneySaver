using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using BusLayer;
using Entity;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }



        public class NameValueItem
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private async void HomePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<NameValueItem> items = new List<NameValueItem>();
            Random _random = new Random();
            for (int i = 0; i < 5; i++)
            {
                items.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            }
            var bus = new BusThongKe();
            var busLoaiGd = new BusLoaiGD();
            List<ThongKe> listThongKeLoaiGd;
            List<ThongKe> listThongKeThu;
            List<ThongKe> listThongKeChi;
            List<ThongKe> listTongTien;
            try
            {
                listThongKeLoaiGd = await bus.ThongKeTheoLoaiGD();
                TxtTotalMoney.Text = (await bus.TinhTongTienTrongTaiKhoan()).ToString();
                listThongKeThu = await bus.TongTienTheoThangVaLoaiGD("Thu");
                listThongKeChi = await bus.TongTienTheoThangVaLoaiGD("Chi");
                listTongTien = await bus.TongTienTheoThang();
            }
            catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                listThongKeLoaiGd = await bus.ThongKeTheoLoaiGD();
                TxtTotalMoney.Text = (await bus.TinhTongTienTrongTaiKhoan()).ToString();
                listThongKeThu = await bus.TongTienTheoThangVaLoaiGD("Thu");
                listThongKeChi = await bus.TongTienTheoThangVaLoaiGD("Chi");
                listTongTien = await bus.TongTienTheoThang();
            }
   
            
            ((PieSeries)this.ThongKeTheoLoai.Series[0]).ItemsSource = ConvertThongKeListToItems(listThongKeLoaiGd);
            LineThu.ItemsSource = ConvertThongKeListToItems(listThongKeThu);
            LineChi.ItemsSource = ConvertThongKeListToItems(listThongKeChi);
            LineTongTien.ItemsSource = ConvertThongKeListToItems(listTongTien);

            //On mobile
            ((PieSeries)this.ThongKeTheoLoaiMobile.Series[0]).ItemsSource = ConvertThongKeListToItems(listThongKeLoaiGd);
            LineThuMobile.ItemsSource = ConvertThongKeListToItems(listThongKeThu);
            LineChiMobile.ItemsSource = ConvertThongKeListToItems(listThongKeChi);
            LineTongTienMobile.ItemsSource = ConvertThongKeListToItems(listTongTien);
        }

        public List<NameValueItem> ConvertThongKeListToItems(List<ThongKe> listThongKe)
        {
            return listThongKe.Select(thongKe => new NameValueItem()
            {
                Name = thongKe.Ten,
                Value = thongKe.GiaTri
            }).ToList();
        } 
    }
}
