using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Entity;
using DataLayer;

namespace BusLayer
{
    public class Bus
    {
        //private readonly SoThuChi _soThuChi = new SoThuChi();
        //public List<GiaoDich> GetListThu()
        //{
        //    return new List<GiaoDich>()
        //    {
        //        new GiaoDich() {Ten = "Lương", SoTien = 500000, GhiChu = "", Ngay = DateTime.Today},
        //        new GiaoDich() {Ten = "Thưởng", SoTien = 500000, GhiChu = "", Ngay = DateTime.Today},
        //        new GiaoDich() {Ten = "Abc", SoTien = 500000, GhiChu = "", Ngay = DateTime.Today}
        //    };
        //}

        //public List<GiaoDich> GetListChi()
        //{
        //    return new List<GiaoDich>()
        //    {
        //        new GiaoDich() { Ten = "Xăng", SoTien = 500000, GhiChu = "", Ngay = DateTime.Today},
        //        new GiaoDich() { Ten = "Xe bus", SoTien = 500000, GhiChu = "", Ngay = DateTime.Today},
        //        new GiaoDich() { Ten = "Đi Nha Trang", SoTien = 500000, GhiChu = "", Ngay = DateTime.Today},
        //        new GiaoDich() { Ten = "Cơm trưa", SoTien = 30000, GhiChu = "", Ngay = DateTime.Today},
        //            NguoiThamGia = "", NhomGd = new NhomGD() { TenNhom = "Ăn uống"} }
        //    };
        //}

        //public bool AddGiaoDich(LoaiGD loaiGd, GiaoDich giaoDich)
        //{
        //    switch (loaiGd)
        //    {
        //        case LoaiGD.Thu:
        //            _soThuChi.AddGiaoDichThu(giaoDich);
        //            break;
        //        case LoaiGD.Chi:
        //            _soThuChi.AddGiaoDichChi(giaoDich);
        //            break;
        //        case LoaiGD.Vay:
        //            break;
        //        case LoaiGD.ChoVay:
        //            break;
        //        case LoaiGD.TietKiem:
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(loaiGd), loaiGd, null);
        //    }
        //    return true;
        //}

        //public async Task<List<NhomGD>> GetListNhomGd()
        //{
            
        //    return new List<NhomGD>()
        //    {
        //        new NhomGD() {TenNhom = "Di chuyển"}, new NhomGD() {TenNhom = "Giải trí"}, new NhomGD() {TenNhom = "Ăn uống"}, new NhomGD() {TenNhom = "Shopping"}
        //    };
        //}
    }
}
