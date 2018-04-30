using PoeHUD.Poe;
using PoeHUD.Poe.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using ItemMods = PoeHUD.Poe.Components.Mods;
using ItemBase = PoeHUD.Poe.Components.Base;
using ItemSockets = PoeHUD.Poe.Components.Sockets;
using PoeHUD.Models;
using PoeHUD.Controllers;

namespace RedVex.InventoryHelper.Filter
{
    [XmlType("BaseFilter")]
    public class BaseFilter : IFilter
    {

        public override Boolean ItemPassesFilter(Entity Item)
        {
            if (Item == null) return false;

            ItemBase itemBase = Item.GetComponent<ItemBase>();
            ItemMods itemMods = Item.GetComponent<ItemMods>();
            ItemSockets itemSockets = Item.GetComponent<ItemSockets>();
            ItemClass itemClass;
            GameController.Instance.Files.ItemClasses.contents.TryGetValue(GameController.Instance.Files.BaseItemTypes.Translate(Item.Path).ClassName, out itemClass);
            if (itemBase == null || itemMods == null) return false;

            if(itemClass.ClassName.ToLower().Equals(Name.ToLower()))
            {
                if(Mods.Count > 0)
                {
                    var correctMods = 0;
                    foreach(FilterMod mod in Mods)
                    { 
                        switch(mod.Name.ToLower())
                        {
                            case "combinedresist":
                                var resist = 0;
                                itemMods.ItemMods.Where(a => a.Name.ToLower().Contains("resist")).ToList().ForEach(a => resist += a.Value1);
                                if(resist >= mod.Value)
                                {
                                    correctMods++;
                                }
                                break;
                            case "sockets":
                                if (itemSockets.NumberOfSockets >= mod.Value) correctMods++;
                                break;
                            case "links":
                                if (itemSockets.LargestLinkSize >= mod.Value) correctMods++;
                                break;
                            case "colors":
                                correctMods++;
                                break;
                            default:
                                if (itemMods.ItemMods.FirstOrDefault(itemmod => itemmod.Name.ToLower().Contains(mod.Name.ToLower()) && itemmod.Value1 >= mod.Value) != null) correctMods++;
                                break;
                        }
                        if((100/Mods.Count*correctMods) >= 75)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
