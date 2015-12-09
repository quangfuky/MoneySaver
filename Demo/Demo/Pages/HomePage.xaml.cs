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
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

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

        private void HomePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<NameValueItem> items = new List<NameValueItem>();
            Random _random = new Random();
            for (int i = 0; i < 5; i++)
            {
                items.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            }
            ((PieSeries)this.piechart.Series[0]).ItemsSource = items;
            ((PieSeries)this.piechart2.Series[0]).ItemsSource = items;
            ((PieSeries)this.piechart3.Series[0]).ItemsSource = items;
            ((PieSeries)this.piechart4.Series[0]).ItemsSource = items;
        }
    }
}
