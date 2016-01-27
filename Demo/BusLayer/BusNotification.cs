using System;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications;

namespace BusLayer
{
    public class BusNotification
    {
        public async Task<bool> ThongBaoWarning()
        {
            var busThongKe = new BusThongKe();
            if (DateTime.Now.Month - 1 <= 0)
                return true;
            int tongChiHienTai;
            int tongChiThangTruoc;
            try
            {
                tongChiHienTai = await busThongKe.TongChiTheoThang(DateTime.Now.Month);
            }
            catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                tongChiHienTai = await busThongKe.TongChiTheoThang(DateTime.Now.Month);
            }
            try
            {
                tongChiThangTruoc = await busThongKe.TongChiTheoThang(DateTime.Now.Month - 1);
            }
            catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                tongChiThangTruoc = await busThongKe.TongChiTheoThang(DateTime.Now.Month - 1);
            }
            if (tongChiHienTai > tongChiThangTruoc && tongChiThangTruoc != 0)
                PopToast("Warning", "Số tiền chi tiêu của bạn đã vượt tháng trước");
            return true;

        }
        

        public ToastNotification PopToast(string title, string content)
        {
            return PopToast(title, content, null, null);
        }

        public static ToastNotification PopToast(string title, string content, string tag, string group)
        {
            string xml = $@"<toast activationType='foreground'>
                                            <visual>
                                                <binding template='ToastGeneric'>
                                                </binding>
                                            </visual>
                                        </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var binding = doc.SelectSingleNode("//binding");

            var el = doc.CreateElement("text");
            el.InnerText = title;

            binding.AppendChild(el);

            el = doc.CreateElement("text");
            el.InnerText = content;
            binding.AppendChild(el);

            return PopCustomToast(doc, tag, group);
        }

        public static ToastNotification PopCustomToast(string xml, string tag, string group)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);


            return PopCustomToast(doc, tag, group);
        }

        [DefaultOverload]
        public static ToastNotification PopCustomToast(XmlDocument doc, string tag, string group)
        {
            var toast = new ToastNotification(doc);

            if (tag != null)
                toast.Tag = tag;

            if (group != null)
                toast.Group = group;

            ToastNotificationManager.CreateToastNotifier().Show(toast);

            return toast;
        }
    }
}