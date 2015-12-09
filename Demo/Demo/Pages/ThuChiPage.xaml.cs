using System;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using BusLayer;
using Demo.Control;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThuChiPage : Page
    {
        private int _idThu;
        private int _idChi;
        public ThuChiPage()
        {
            this.InitializeComponent();
        }

        private async void ThuChiPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            //LoadThuChi();
            var bus = new BusLoaiGD();
            _idThu = await bus.LoadIDLoaiGD("Thu");
            _idChi = await bus.LoadIDLoaiGD("Chi");
            LoadGDThu();
            LoadGDChi();
        }

        private async void LoadGDThu()
        {
            var business = new BusGiaoDich();
            var listThu = await business.LoadGiaoDichByLoaiGD(_idThu);
            if (listThu.Count == 0)
            {
                var status = new TextBlock()
                {
                    FontSize = 30,
                    FontStyle = FontStyle.Italic,
                    Foreground = new SolidColorBrush(Colors.DimGray),
                    TextWrapping = TextWrapping.Wrap,
                    Text = "Hiện chưa có giao dịch thu trong sổ thu chi"
                };
                ThuPanel.Children.Add(status);
            }
            else
            {
                ThuPanel.Children.Clear();
                foreach (var giaoDich in listThu)
                {
                    var giaoDichItem = new ViewData(giaoDich);
                    giaoDichItem.delete += Delete;
                    ThuPanel.Children.Add(giaoDichItem);
                }
            }
        }

        private void Delete(int ID)
        {
            var bus = new BusGiaoDich();
            bus.DeleteGiaoDichByID(ID);
            LoadGDChi();
            LoadGDThu();
        }

        private async void LoadGDChi()
        {
            var business = new BusGiaoDich();
            var listChi = await business.LoadGiaoDichByLoaiGD(_idChi);
            if (listChi.Count == 0)
            {
                var status = new TextBlock()
                {
                    FontSize = 30,
                    FontStyle = FontStyle.Italic,
                    Foreground = new SolidColorBrush(Colors.DimGray),
                    TextWrapping = TextWrapping.Wrap,
                    Text = "Hiện chưa có giao dịch chi trong sổ thu chi"
                };
                ChiPanel.Children.Add(status);
            }
            else
            {
                ChiPanel.Children.Clear();
                foreach (var giaoDich in listChi)
                {
                    var giaoDichItem = new ViewData(giaoDich);
                    ChiPanel.Children.Add(giaoDichItem);
                }
            }
        }
    }
}
