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
using BusLayer;
using Demo.Control;
using Entity;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.Pages
{
    public sealed partial class EditDialog : ContentDialog
    {
        private GiaoDich _giaoDich;
       
        ViewData viewer;
        public EditDialog()
        {
            this.InitializeComponent();
        }

        public EditDialog(GiaoDich giaoDich)
        {
            this.InitializeComponent();
            _giaoDich = giaoDich;
            TxtTenGd.Text = giaoDich.Ten;
            TxtGhiChu.Text = giaoDich.GhiChu;
            TxtTien.Text = giaoDich.SoTien.ToString();
            DpNgay.Date = giaoDich.Ngay;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var bus = new BusGiaoDich();
            var giaoDich = new GiaoDich()
            {
                ID = _giaoDich.ID,
                Ten = TxtTenGd.Text,
                GhiChu = TxtGhiChu.Text,
                SoTien = int.Parse(TxtTien.Text),
                Ngay = Convert.ToDateTime(DpNgay.Date.ToString()),
                LoaiGD = _giaoDich.LoaiGD
            };
            await bus.EditGiaoDich(giaoDich);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
