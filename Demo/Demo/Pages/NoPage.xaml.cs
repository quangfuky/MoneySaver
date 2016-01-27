using System;
using System.Threading.Tasks;
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
    public sealed partial class NoPage : Page
    {
        private int _idVay;
        private int _idChoVay;
        public NoPage()
        {
            this.InitializeComponent();
        }

        private async void NoPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            //LoadThuChi();
            var bus = new BusLoaiGD();
            _idVay = await bus.LoadIDLoaiGD("Vay");
            _idChoVay = await bus.LoadIDLoaiGD("Cho vay");
            try
            {
                await LoadGDChoVay();
            } catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                await LoadGDChoVay();
            }
            try
            {
                await LoadGDVay();
            } catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                await LoadGDVay();
            }
        }

        private async Task<bool> LoadGDVay()
        {
            var business = new BusGiaoDich();
            var listVay = await business.LoadGiaoDichByLoaiGD(_idVay);
            if (listVay.Count == 0)
            {
                VayPanel.Children.Clear();
                var status = new TextBlock()
                {
                    FontSize = 30,
                    FontStyle = FontStyle.Italic,
                    Foreground = new SolidColorBrush(Colors.DimGray),
                    TextWrapping = TextWrapping.Wrap,
                    Text = "Hiện chưa có giao dịch nào trong sổ vay nợ"
                };
                VayPanel.Children.Add(status);
            }
            else
            {
                VayPanel.Children.Clear();
                foreach (var giaoDich in listVay)
                {
                    var giaoDichItem = new ViewData(giaoDich) { Margin = new Thickness(10) };
                    giaoDichItem.Delete += Delete;
                    VayPanel.Children.Add(giaoDichItem);
                }
            }
            return true;
        }

        private async Task<bool> LoadGDChoVay()
        {
            var business = new BusGiaoDich();
            var listChoVay = await business.LoadGiaoDichByLoaiGD(_idChoVay);
            if (listChoVay.Count == 0)
            {
                ChoVayPanel.Children.Clear();
                var status = new TextBlock()
                {
                    FontSize = 30,
                    FontStyle = FontStyle.Italic,
                    Foreground = new SolidColorBrush(Colors.DimGray),
                    TextWrapping = TextWrapping.Wrap,
                    Text = "Hiện chưa có giao dịch nào trong sổ cho vay"
                };
                ChoVayPanel.Children.Add(status);
            }
            else
            {
                ChoVayPanel.Children.Clear();
                foreach (var giaoDich in listChoVay)
                {
                    var giaoDichItem = new ViewData(giaoDich) { Margin = new Thickness(10) };
                    giaoDichItem.Delete += Delete;
                    ChoVayPanel.Children.Add(giaoDichItem);
                }
            }
            return true;
        }
        private async void Delete(int ID)
        {
            var bus = new BusGiaoDich();
            if (await bus.DeleteGiaoDichByID(ID) == true)
            {
                try
                {
                    await LoadGDChoVay();
                }
                catch (UnauthorizedAccessException)
                {
                    await Task.Delay(millisecondsDelay: 500);
                    await LoadGDChoVay();
                }
                try
                {
                    await LoadGDVay();
                }
                catch (UnauthorizedAccessException)
                {
                    await Task.Delay(millisecondsDelay: 500);
                    await LoadGDVay();
                }
            }
        }
    }
}
