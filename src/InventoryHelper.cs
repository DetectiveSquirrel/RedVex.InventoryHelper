using PoeHUD.Models.Enums;
using PoeHUD.Plugins;
using PoeHUD.Poe.Elements;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RedVex.InventoryHelper
{
    public class InventoryHelper : BaseSettingsPlugin<Settings>
    {
        public static Filter.FilterContainer FilterContainer = new Filter.FilterContainer();
        public override void Initialise()
        {
            FilterContainer.Filters.Add(new Filter.NameFilter() { Name = "Coral Ring", Mods = new List<Filter.FilterMod>() { new Filter.FilterMod() { Name = "Life", Value = 20 } } });
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Filter.FilterContainer));
            xmlSerializer.Serialize(new StreamWriter("test.xml"), FilterContainer);
        }

        public override void Render()
        {
            if (GameController.InGame && GameController.Game.IngameState.IngameUi.InventoryPanel.IsVisible)
            {
                var b = GameController.Game.IngameState.IngameUi.InventoryPanel[InventoryIndex.PlayerInventory];
                if (b.VisibleInventoryItems == null) return;
                foreach (NormalInventoryItem e in b.VisibleInventoryItems)
                {
                    if (e == null) continue;
                    var drawn = false;
                    var GameTooltip = GUI.Tooltip.GetRectangleF();
                    GameTooltip.Width += 15;
                    GameTooltip.Height += 15;
                    GameTooltip.Y -= 15;
                    GameTooltip.X -= 15;
                    var myPos = e.GetClientRect();
                    var myCenter = new Point((int)(myPos.Left + myPos.Width / 2), (int)(myPos.Top + myPos.Height / 2));
                    foreach (Filter.IFilter sett in FilterContainer.Filters)
                    {
                        if (sett.ItemPassesFilter(e.Item) && !drawn)
                        {
                            if (!GameTooltip.Contains(myCenter))
                            {
                                Graphics.DrawBox(myPos, Settings.GoodColor);
                                drawn = true;
                            }
                        }
                    }
                    if (!drawn && (e.Item.Path.Contains("Currency") || e.Item.Path.Contains("QuestItems")))
                    {
                        if (!GameTooltip.Contains(myCenter))
                        {
                            Graphics.DrawFrame(myPos, 4, Settings.GoodColor);
                            drawn = true;
                        }
                    }
                    else if (!drawn && (e.Item.Path.Contains("Map")))
                    {
                        if (!GameTooltip.Contains(myCenter))
                        {
                            Graphics.DrawFrame(myPos, 4, Settings.MapsBorderColor);
                            drawn = true;
                        }
                    }
                    else
                    {
                        if (!drawn)
                        {
                            if (!GameTooltip.Contains(myCenter))
                            {
                                Graphics.DrawBox(myPos, Settings.VendorColor);
                                drawn = true;
                            }
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
