using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
            LoadGDVay();
            LoadGDChoVay();
        }

        private async void LoadGDVay()
        {
            var business = new BusGiaoDich();
            var listVay = await business.LoadGiaoDichByLoaiGD(_idVay);
            if (listVay.Count == 0)
            {
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
                    VayPanel.Children.Add(giaoDichItem);
                }
            }
        }

        private async void LoadGDChoVay()
        {
            var business = new BusGiaoDich();
            var listChoVay = await business.LoadGiaoDichByLoaiGD(_idChoVay);
            if (listChoVay.Count == 0)
            {
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
                    ChoVayPanel.Children.Add(giaoDichItem);
                }
            }
        }
    }
}
