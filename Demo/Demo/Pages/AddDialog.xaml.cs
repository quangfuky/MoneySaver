using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BusLayer;
using Entity;
// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.Pages
{
    public sealed partial class AddDialog : ContentDialog
    {
        public AddDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var bus = new Bus();
            var temp = 0;

            var giaoDich = new GiaoDich()
            {
                Ten = TxtTenGd.Text,
                SoTien = int.Parse(TxtTien.Text),
                GhiChu = TxtGhiChu.Text,
                Ngay = DateTime.Parse(DpNgay.Date.ToString()),
              
            };

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void AddDialog_OnLoaded(object sender, RoutedEventArgs e)
        {
            var bus = new BusLoaiGD();
            var listLoaiGD = await bus.LoadLoaiGD();
            BoxLoaiGd.ItemsSource = listLoaiGD;
            BoxLoaiGd.DisplayMemberPath = "Ten";
        }
    }
}
