using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUI
{
    public static class ThemeColors
    {
        public static Color PrimaryColor { get; set; }
        public static Color SecondaryColor { get; set; }

        public static List<string> ColorList = new List<string>()
        {
            "#3F51B5",
            "#009688",
            "#FF5722",
            "#607D8B",
            "#FF9800",
            "#9C27B0",
            "#2196F3",
            "#EA676C",
            "#E41A4A",
            "#5978BB",
            "#018790",
            "#0E3441",
            "#00B0AD",
            "#721D47",
            "#EA4833",
            "#EF937E",
            "#F37521",
            "#A12059",
            "#126881",
            "#8BC240",
            "#364D5B",
            "#C7DC5B",
            "#0094BC",
            "#E4126B",
            "#43B76E",
            "#7BCFE9",
            "#B71C46"
        };

        public static List<string> ColorList2 = new List<string>()
        {
            "#99b433",
            "#00a300",
            "#1e7145",
            "#00aba9",
            "#ff0097",
            "#9f00a7",
            "#7e3878",
            "#603cba",
            "#1d1d1d",
            "#00aba9",
            "#eff4ff",
            "#2d89ef",
            "#2b5797",
            "#ffc40d",
            "#e3a21a",
            "#da532c",
            "#ee1111",
            "#a20025",
            "#b91d47",






        };


        // create my own palette
        public static List<string> ColorList3 = new List<string>()
        {
            "#FF5733",
            "#33FF57",
            "#3357FF",
            "#F1C40F",
            "#8E44AD",
            "#E74C3C",
            "#2ECC71",
            "#3498DB",
            "#1ABC9C",
            "#9B59B6",
            "#E67E22",
            "#34495E",
            "#16A085",
            "#27AE60",
            "#2980B9",
            "#8E44AD",
            "#2C3E50",
            "#D35400",
            "#C0392B",
            "#7F8C8D"
        };

    public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            // If correction factor is less than 0, darken color
            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }

            // If correction factor is greater than 0, lighten color
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }

    }
}
