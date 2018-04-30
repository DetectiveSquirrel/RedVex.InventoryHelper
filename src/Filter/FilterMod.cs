using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RedVex.InventoryHelper.Filter
{
    [XmlType(("Mod"))]
    public class FilterMod
    {
        public String Name;
        public Int32 Value;

        public override string ToString()
        {
            return String.Format("{0} = {1}", Name, Value);
        }
    }
}
