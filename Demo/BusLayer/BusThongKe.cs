using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Entity;

namespace BusLayer
{
    public class BusThongKe
    {
        public async Task<int> TinhTongTienTheoLoaiGD(int LoaiGD)
        {
            var busGiaoDich = new BusGiaoDich();
            var nodeList = await busGiaoDich.LoadGiaoDichByLoaiGD(LoaiGD);
            return nodeList.Sum(node => node.SoTien);
        }

        public async Task<List<ThongKe>> ThongKeTheoLoaiGD()
        {
            var busLoaiGd = new BusLoaiGD();
            var listThongKe = new List<ThongKe>();
            var listGiaoDich = await busLoaiGd.LoadLoaiGD();
            foreach (var loaiGd in listGiaoDich)
            {
                listThongKe.Add(new ThongKe()
                {
                    Ten = loaiGd.Ten,
                    GiaTri = await TinhTongTienTheoLoaiGD(loaiGd.ID)
                });
            }
            return listThongKe;
        }

        public async Task<int> TinhTongTienTrongTaiKhoan()
        {
            var listThongKe = await ThongKeTheoLoaiGD();
            var tongTien = 0;
            foreach (var thongKe in listThongKe)
            {
                switch (thongKe.Ten)
                {
                    case "Vay":
                        tongTien += thongKe.GiaTri;
                        break;
                    case "Cho vay":
                        tongTien -= thongKe.GiaTri;
                        break;
                    case "Thu":
                        tongTien += thongKe.GiaTri;
                        break;
                    case "Chi":
                        tongTien -= thongKe.GiaTri;
                        break;
                    case "Tiet kiem":
                        tongTien -= thongKe.GiaTri;
                        break;
                }
            }
            return tongTien;
        }

        public async Task<List<ThongKe>> TongTienTheoThang()
        {
            var listThu = await TongTienTheoThangVaLoaiGD("Thu");
            var listChi = await TongTienTheoThangVaLoaiGD("Chi");
            var listTietKiem = await TongTienTheoThangVaLoaiGD("Tiet kiem");
            var listVay = await TongTienTheoThangVaLoaiGD("Vay");
            var listChoVay = await TongTienTheoThangVaLoaiGD("Cho vay");
            return listThu.Select((t, i) => new ThongKe()
            {
                Ten = t.Ten,
                GiaTri = t.GiaTri + listVay[i].GiaTri - listChi[i].GiaTri
                         - listChoVay[i].GiaTri - listTietKiem[i].GiaTri
            }).ToList();
        }

        public async Task<List<ThongKe>> TongTienTheoThangVaLoaiGD(string name)
        {
            var listThongKe = new List<ThongKe>();
            var busGiaoDich = new BusGiaoDich();
            var busLoaiGd = new BusLoaiGD();
            int idLoaiGd;
            List<GiaoDich> nodeList;
            try
            {
                idLoaiGd = await busLoaiGd.LoadIDLoaiGD(name);
                nodeList = await busGiaoDich.LoadGiaoDichByLoaiGD(idLoaiGd);
            }
            catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                idLoaiGd = await busLoaiGd.LoadIDLoaiGD(name);
                nodeList = await busGiaoDich.LoadGiaoDichByLoaiGD(idLoaiGd);
            }
            int i;
            if (DateTime.Now.Month - 3 <= 0)
                i = 1;
            else
                i = DateTime.Now.Month - 3;
            for (; i <= DateTime.Now.Month; i++)
            {
                var tongTien =
                    nodeList.Where(giaoDich => giaoDich.Ngay.Month == i && giaoDich.Ngay.Year == DateTime.Now.Year)
                        .Sum(giaoDich => giaoDich.SoTien);
                listThongKe.Add(new ThongKe()
                {
                    Ten = "Tháng " + i,
                    GiaTri = tongTien / 1000
                });
            }
            return listThongKe;
        }
        public async Task<int> TongChiTheoThang(int thang)
        {
            var busGiaoDich = new BusGiaoDich();
            var busLoaiGd = new BusLoaiGD();
            int idLoaiGd;
            List<GiaoDich> nodeList;
            try
            {
                idLoaiGd = await busLoaiGd.LoadIDLoaiGD("Chi");
                nodeList = await busGiaoDich.LoadGiaoDichByLoaiGD(idLoaiGd);
            }
            catch (UnauthorizedAccessException)
            {
                await Task.Delay(millisecondsDelay: 500);
                idLoaiGd = await busLoaiGd.LoadIDLoaiGD("Chi");
                nodeList = await busGiaoDich.LoadGiaoDichByLoaiGD(idLoaiGd);
            }

            var tongTien =
                nodeList.Where(giaoDich => giaoDich.Ngay.Month == thang && giaoDich.Ngay.Year == DateTime.Now.Year)
                    .Sum(giaoDich => giaoDich.SoTien);

            return tongTien;
        }
    }
}