using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BusLayer;
using Demo.Pages;
using Entity;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Demo.Control
{
    public sealed partial class ViewData : UserControl
    {
        public delegate void DeleteButtonHandler(int ID);

        public DeleteButtonHandler Delete;
        private GiaoDich _giaoDich;

        public ViewData()
        {
            this.InitializeComponent();
        }

        public async Task<bool> Reload()
        {
            var bus = new BusGiaoDich();
            try
            {
                _giaoDich = await bus.LoadGiaoDichById(_giaoDich.ID);
            }
            catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                _giaoDich = await bus.LoadGiaoDichById(_giaoDich.ID);
            }
            TieuDe.Text = _giaoDich.Ten;
            SoTien.Text = _giaoDich.SoTien.ToString();
            GhiChu.Text = _giaoDich.GhiChu;
            Ngay.Text = string.Format("{0}/{1}/{2}", _giaoDich.Ngay.Day, _giaoDich.Ngay.Month, _giaoDich.Ngay.Year);
            return true;
        }

        public ViewData(GiaoDich giaoDich)
        {
            this.InitializeComponent();
            _giaoDich = giaoDich;
            RandomColor();
            TieuDe.Text = giaoDich.Ten;
            SoTien.Text = giaoDich.SoTien.ToString();
            GhiChu.Text = giaoDich.GhiChu;
            Ngay.Text = string.Format("{0}/{1}/{2}", giaoDich.Ngay.Day, giaoDich.Ngay.Month, giaoDich.Ngay.Year);
        }
        void RandomColor()
        {
            var Rnd = new Random(DateTime.Now.Millisecond);
            var k = Rnd.Next(0, 10);
            switch (k % 10)
            {
                case 0:
                    Panel.Background = new SolidColorBrush(Color.FromArgb(255, 2, 79, 39));
                    break;
                case 1:
                    Panel.Background = new SolidColorBrush(Color.FromArgb(255, 166, 5, 34));
                    break;
                case 2:
                    Panel.Background = new SolidColorBrush(Color.FromArgb(255, 18, 16, 36));
                    break;
                case 3:
                    Panel.Background = new SolidColorBrush(Color.FromArgb(255, 65, 42, 24));
                    break;
                case 4:
                    Panel.Background = new SolidColorBrush(Color.FromArgb(255, 89, 80, 32));
                    break;
                case 5:
                    Panel.Background = new SolidColorBrush(Color.FromArgb(255, 191, 54, 76));
                    break;
            }
        }

        private void ViewData_OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            commandBar.Visibility = Visibility.Visible;
        }

        private void ViewData_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            commandBar.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            commandBar.Visibility = commandBar.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Delete(_giaoDich.ID);
        }

        private async void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            var editDialog = new EditDialog(_giaoDich);
            var result = await editDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                 await Task.Delay(millisecondsDelay: 500);
                 await Reload();
            }
        }
    }
}
