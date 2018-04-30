using PoeHUD.Poe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RedVex.InventoryHelper.Filter
{
    [XmlInclude(typeof(NameFilter))]
    [XmlInclude(typeof(BaseFilter))]
    [XmlRoot("Filters")]
    [XmlType("Filter")]
    public class IFilter
    {
        public String Name { get; set; }
        public List<FilterMod> Mods { get; set; }

        public virtual Boolean ItemPassesFilter(Entity Item)
        {
            return false;
        }
    }
}
