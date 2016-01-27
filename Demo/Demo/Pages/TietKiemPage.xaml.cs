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
    public sealed partial class TietKiemPage : Page
    {
        private int _idTietkiem;
        public TietKiemPage()
        {
            this.InitializeComponent();
        }

        private async void TietKiemPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            //LoadThuChi();
            var bus = new BusLoaiGD();
            _idTietkiem = await bus.LoadIDLoaiGD("Tiet kiem");
            try
            {
                await LoadGDTietKiem();
            }
            catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                await LoadGDTietKiem();
            }
        }

        private async Task<bool> LoadGDTietKiem()
        {
            var business = new BusGiaoDich();
            var listTietKiem = await business.LoadGiaoDichByLoaiGD(_idTietkiem);
            if (listTietKiem.Count == 0)
            {
                TietKiemPanel.Children.Clear();
                var status = new TextBlock()
                {
                    FontSize = 30,
                    FontStyle = FontStyle.Italic,
                    Foreground = new SolidColorBrush(Colors.DimGray),
                    TextWrapping = TextWrapping.Wrap,
                    Text = "Hiện chưa có giao dịch nào trông sổ tiết kiệm"
                };
                TietKiemPanel.Children.Add(status);
            }
            else
            {
                TietKiemPanel.Children.Clear();
                foreach (var giaoDich in listTietKiem)
                {
                    var giaoDichItem = new ViewData(giaoDich);
                    giaoDichItem.Delete += Delete;
                    TietKiemPanel.Children.Add(giaoDichItem);
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
                    await LoadGDTietKiem();
                }
                catch (UnauthorizedAccessException)
                {
                    await Task.Delay(millisecondsDelay: 500);
                    await LoadGDTietKiem();
                }
            }
        }
    }
}
