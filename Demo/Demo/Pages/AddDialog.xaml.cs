using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var bus = new BusGiaoDich();
            if (TxtTien.Text == "")
            {
                TxtTien.Header = "Bạn vui lòng nhập số tiền:";
                return;
            }
            int soTien;
            var check = int.TryParse(TxtTien.Text, out soTien);
            if (check == false)
            {
                TxtTien.Header = "Bạn vui lòng nhập lại số tiền (Nhập bằng số):";
                return;
            }

            await bus.AddNewGiaoDich( new GiaoDich()
            {
                Ten = TxtTenGd.Text,
                GhiChu = TxtGhiChu.Text,
                SoTien = int.Parse(TxtTien.Text),
                Ngay = Convert.ToDateTime(DpNgay.Date.ToString()),
                LoaiGD = int.Parse(BoxLoaiGd.SelectedValue.ToString())
            });
            var busNoti = new BusNotification();
            await busNoti.ThongBaoWarning();
            return;
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
            BoxLoaiGd.SelectedValuePath = "ID";
            BoxLoaiGd.SelectedIndex = 0;
        }
    }
}
