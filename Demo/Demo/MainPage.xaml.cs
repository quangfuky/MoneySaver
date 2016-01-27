using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using BusLayer;
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
            //RegisterBackgroundTask();
        }

        async void RegisterBackgroundTask()
        {

            await (BackgroundTaskConfig.RegisterBackgroundTask(BackgroundTaskConfig.SampleBackgroundTaskEntryPoint,
                                                                   BackgroundTaskConfig.SampleBackgroundTaskName,
                                                                    new SystemTrigger(SystemTriggerType.TimeZoneChange, false),
                                                                   null));
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
                await Task.Delay(millisecondsDelay: 500);
                var s = ScenarioControl?.SelectedItem as Scenario;
                if (s == null) return;
                ScenarioFrame.Navigate(s.ClassType);
            }
        }

        private void BtnFeedback_OnClick(object sender, RoutedEventArgs e)
        {
            var bus = new Bus();
            bus.ComposeEmail(new Contact(), "MoneySaverTeam@gmail.com", null);
        }

        private void BtnInfo_OnClick(object sender, RoutedEventArgs e)
        {
            ScenarioFrame.Navigate(typeof(InfoPage));
        }
    }
}
