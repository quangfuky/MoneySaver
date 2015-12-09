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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Demo.Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Demo
{
    public sealed partial class MainPage : Page
    {
        public  string Title { get; set; }
        public static MainPage Current;

        public List<Scenario> Scenarios { get; } = new List<Scenario>
        {
            new Scenario() { Icon = "Resources/btnHome.png", Title = "Home page", ClassType = typeof(HomePage) },
            new Scenario() { Icon = "Resources/btnThuChi.png", Title = "Sổ thu chi", ClassType = typeof(ThuChiPage) },
            new Scenario() { Icon = "Resources/btnTietKiem.png", Title = "Sổ tiết kiệm", ClassType = typeof(TietKiemPage) },
            new Scenario() { Icon = "Resources/btnNo.png", Title = "Sổ nợ", ClassType = typeof(NoPage) }
        };

        public MainPage()
        {
            this.InitializeComponent();
        }


        private void HambergerButton_OnClick(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
            SecondaryButtonGroup.Orientation = Orientation.Horizontal;
        }
        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            ScenarioControl.ItemsSource = Scenarios;
            ScenarioControl.SelectedIndex = 0;

        }

        private void ScenarioControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var scenarioListBox = sender as ListBox;
            var s = scenarioListBox?.SelectedItem as Scenario;
            if (s == null) return;
            ScenarioFrame.Navigate(s.ClassType);
            if (Window.Current.Bounds.Width < 640)
            {
                SplitView.IsPaneOpen = false;
            }
            Title = s.Title;
        }

        private void SplitView_OnPaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {
            SecondaryButtonGroup.Orientation = Orientation.Vertical;
        }

        private async void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var addDialog = new AddDialog();
            var result = await addDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var s = ScenarioControl?.SelectedItem as Scenario;
                if (s == null) return;
                ScenarioFrame.Navigate(s.ClassType);
            }
        }
    }
    //public class ScenarioBindingConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, string language)
    //    {
    //        Scenario s = value as Scenario;
    //        return (MainPage.Current.Scenarios.IndexOf(s) + 1) + ") " + s.Title;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, string language)
    //    {
    //        return true;
    //    }
    //}
}
