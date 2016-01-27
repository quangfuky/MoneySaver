using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using DataLayer;
using Entity;

namespace BusLayer
{
    public class BusGiaoDich
    {

        public async Task<GiaoDich> LoadGiaoDichById(int id)
        {
            var dao = new DAO();
            var node = await dao.GetSingleNode("//GiaoDich[@ID=" + id + "]");
            if (node != null)
            {
                var giaoDich = new GiaoDich()
                {
                    ID = int.Parse(node.Attributes.GetNamedItem("ID").NodeValue.ToString()),
                    Ten = node.Attributes.GetNamedItem("Ten").NodeValue.ToString(),
                    SoTien = int.Parse(node.Attributes.GetNamedItem("SoTien").NodeValue.ToString()),
                    Ngay = DateTime.Parse(node.Attributes.GetNamedItem("Ngay").NodeValue.ToString()),
                    GhiChu = node.Attributes.GetNamedItem("GhiChu").NodeValue.ToString(),
                    LoaiGD = int.Parse(node.Attributes.GetNamedItem("IDLoai").NodeValue.ToString())
                };
                return giaoDich;
            }
            return null;
        }

        public async Task<List<GiaoDich>> LoadGiaoDichByLoaiGD(int IDLoai)
        {
            var dao = new DAO();
            var nodeList = await dao.GetNodeList("//GiaoDich[@IDLoai=" + IDLoai + "]");
            return nodeList.Select(xmlNode => new GiaoDich
            {
                ID = int.Parse(xmlNode.Attributes.GetNamedItem("ID").NodeValue.ToString()),
                Ten = xmlNode.Attributes.GetNamedItem("Ten").NodeValue.ToString(),
                SoTien = int.Parse(xmlNode.Attributes.GetNamedItem("SoTien").NodeValue.ToString()),
                Ngay = DateTime.Parse(xmlNode.Attributes.GetNamedItem("Ngay").NodeValue.ToString()),
                GhiChu = xmlNode.Attributes.GetNamedItem("GhiChu").NodeValue.ToString(),
                LoaiGD = int.Parse(xmlNode.Attributes.GetNamedItem("IDLoai").NodeValue.ToString())
            }).ToList();
        }

        public async Task<bool> DeleteGiaoDichByID(int id)
        {
            var dao = new DAO();
            var doc = await dao.LoadDatabase();
            var node = doc.SelectSingleNode("//GiaoDich[@ID=" + id + "]");
            var element = doc.DocumentElement;
            element.RemoveChild(node);
            await dao.SaveDatabase(element.OwnerDocument);
            return true;
        }

        public async Task<bool> AddNewGiaoDich(GiaoDich giaoDich)
        {
            var dao = new DAO();
            var listID = await dao.GetNodeList("//GiaoDich");
            var maxID =
                listID.Select(item => int.Parse(item.Attributes.GetNamedItem("ID").NodeValue.ToString()))
                    .Concat(new[] {0})
                    .Max();
            var doc = await dao.LoadDatabase();
            var newNode = doc.CreateElement("GiaoDich");
            newNode.SetAttribute("ID", (maxID + 1).ToString());
            newNode.SetAttribute("Ten",giaoDich.Ten);
            newNode.SetAttribute("SoTien", giaoDich.SoTien.ToString());
            newNode.SetAttribute("GhiChu", giaoDich.GhiChu);
            newNode.SetAttribute("Ngay",
                string.Format("{0}/{1}/{2}", giaoDich.Ngay.Month, giaoDich.Ngay.Day, giaoDich.Ngay.Year));
            newNode.SetAttribute("IDLoai", giaoDich.LoaiGD.ToString());
            doc.DocumentElement.AppendChild(newNode);
            await dao.SaveDatabase(doc);
            return true;
        }

        public async Task<bool> EditGiaoDich(GiaoDich giaoDich)
        {
            var dao = new DAO();
            var doc = await dao.LoadDatabase();
            var node = doc.SelectSingleNode("//GiaoDich[@ID=" + giaoDich.ID + "]");
            node.Attributes.GetNamedItem("Ten").NodeValue = giaoDich.Ten;
            node.Attributes.GetNamedItem("SoTien").NodeValue = giaoDich.SoTien.ToString();
            node.Attributes.GetNamedItem("GhiChu").NodeValue = giaoDich.GhiChu;
            node.Attributes.GetNamedItem("Ngay").NodeValue = string.Format("{0}/{1}/{2}", giaoDich.Ngay.Month,
                giaoDich.Ngay.Day, giaoDich.Ngay.Year);
            var element = doc.DocumentElement;
            await dao.SaveDatabase(element.OwnerDocument);
            return true;
        }
    }
}