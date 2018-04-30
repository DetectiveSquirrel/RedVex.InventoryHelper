using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoeHUD.Hud.Settings;
using PoeHUD.Plugins;
using SharpDX;
using System.Xml.Serialization;
using System.IO;

namespace RedVex.InventoryHelper
{
    public class Settings : SettingsBase
    {
        [Menu("Reload Settings", 100)]
        public ButtonNode ReloadSettingsNode { get; set; }
        [Menu("KeepColor", 101)]
        public ColorNode GoodColor { get; set; }
        [Menu("VendorColor", 102)]
        public ColorNode VendorColor { get; set; }
        [Menu("MapsBorderColor", 103)]
        public ColorNode MapsBorderColor { get; set; }
        public Settings()
        {
            GoodColor = new ColorNode(new Color(0, 125, 0, 150));
            VendorColor = new ColorNode(new Color(125, 0, 0, 150));
            MapsBorderColor = new ColorNode(new Color(150, 110, 70, 150));
            ReloadSettingsNode = new ButtonNode();
            ReloadSettingsNode.OnPressed = () => {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Filter.FilterContainer));
                InventoryHelper.FilterContainer = (Filter.FilterContainer)xmlSerializer.Deserialize(new StreamReader("test.xml"));
            };
        }
    }
}
