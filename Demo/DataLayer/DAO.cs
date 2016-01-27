using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using Windows.ApplicationModel.Activation;
using Windows.Data.Xml.Dom;
using Windows.Storage;


namespace DataLayer
{
    public class DAO
    {
        public async Task<XmlDocument> LoadDatabase()
        {
            var loadSettings = new XmlLoadSettings
            {
                ProhibitDtd = false,
                ResolveExternals = true
            };
            var file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("database.xml",CreationCollisionOption.OpenIfExists);
            if ((await file.GetBasicPropertiesAsync()).Size != 0)
                return await XmlDocument.LoadFromFileAsync(file, loadSettings);
            var doc = new XmlDocument();
            var element = doc.CreateElement("MoneySaver");
            doc.AppendChild(element);
            return doc;
        }
        public async Task<XmlDocument> LoadReadOnlyDatabase()
        {
            var loadSettings = new XmlLoadSettings
            {
                ProhibitDtd = false,
                ResolveExternals = true
            };
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Database");
            var file = await folder.GetFileAsync("readonly_database.xml");
            
            return await Windows.Data.Xml.Dom.XmlDocument.LoadFromFileAsync(file, loadSettings);
        }
        public async Task<bool> SaveDatabase(XmlDocument document)
        {
            const string filename = "database.xml";
            var file =
                await
                    Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename,
                        Windows.Storage.CreationCollisionOption.OpenIfExists);
            await document.SaveToFileAsync(file);
            return true;
        }

        public async Task<XmlNodeList> GetNodeList(string xpath, bool isReadonly = false)
        {
            XmlDocument doc;
            if (!isReadonly)
                doc = await LoadDatabase();
            else
            {
                doc = await LoadReadOnlyDatabase();
            }
            var nodeList = doc.SelectNodes(xpath);
            return nodeList;
        }
        public async Task<IXmlNode> GetSingleNode(string xpath, bool isReadonly = false)
        {
            XmlDocument doc;
            if (!isReadonly)
                doc = await LoadDatabase();
            else
            {
                doc = await LoadReadOnlyDatabase();
            }
            var node = doc.SelectSingleNode(xpath);
            return node;
        }
    }
}
