using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Entity;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Demo.Control
{
    public sealed partial class ViewData : UserControl
    {
        public delegate void DeleteButtonHandler(int ID);

        public DeleteButtonHandler delete;
        private int _id;

        public ViewData()
        {
            this.InitializeComponent();
        }

        public ViewData(GiaoDich giaoDich)
        {
            this.InitializeComponent();
            _id = giaoDich.ID;
            TieuDe.Text = giaoDich.Ten;
            SoTien.Text = giaoDich.SoTien.ToString();
            GhiChu.Text = giaoDich.GhiChu;
            Ngay.Text = string.Format("{0}/{1}/{2}", giaoDich.Ngay.Day, giaoDich.Ngay.Month, giaoDich.Ngay.Year);
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
            if (commandBar.Visibility == Visibility.Visible)
            {
                commandBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                commandBar.Visibility = Visibility.Visible;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            delete(_id);
        }
    }
}
