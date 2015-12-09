using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Entity;

namespace BusLayer
{
    public class BusLoaiGD
    {
        public async Task<int> LoadIDLoaiGD(string name)
        {
            var dao = new DAO();
            var node = await dao.GetSingleNode("//LoaiGD[@Ten='" + name + "']");
            return int.Parse(node?.Attributes.GetNamedItem("ID").NodeValue.ToString());
        }

        public async Task<List<LoaiGD>> LoadLoaiGD()
        {
            var dao = new DAO();
            var nodeList = await dao.GetNodeList("//LoaiGD");
            return nodeList.Select(xmlNode => new LoaiGD
            {
                ID = int.Parse(xmlNode.Attributes.GetNamedItem("ID").NodeValue.ToString()),
                Ten = xmlNode.Attributes.GetNamedItem("Ten").NodeValue.ToString()
            }).ToList();
        }
    }
}