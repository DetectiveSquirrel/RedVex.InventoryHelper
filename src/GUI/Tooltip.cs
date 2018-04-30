using PoeHUD.Controllers;
using PoeHUD.Poe;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedVex.InventoryHelper.GUI
{
    public class Tooltip
    {
        public static Element GetTooltip()
        {
            Element ttip = null;
            if((ttip = GameController.Instance.Game.IngameState.UIHoverTooltip.Tooltip) != null)
            {
                return ttip;
            }

            return null;
        }
        public static RectangleF GetRectangleF()
        {
            return GetTooltip().GetClientRect();
        }

        public static Rectangle GetRectangle()
        {
            var rectangleF = GetRectangleF();
            return new Rectangle((Int32)rectangleF.X, (Int32)rectangleF.Y, (Int32)rectangleF.Width, (Int32)rectangleF.Height);
        }
    }
}
