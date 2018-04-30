using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RedVex.InventoryHelper.Filter
{
    [XmlRoot("InventoryFilter")]
    [XmlInclude(typeof(BaseFilter))]
    [XmlInclude(typeof(NameFilter))]
    public class FilterContainer
    {
        [XmlArray("Filters")]
        public List<IFilter> Filters = new List<IFilter>();
    }
}
