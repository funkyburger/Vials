using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public class ColorToCssMapper : IColorToCssMapper
    {
        public string Map(int level, Color color)
        {
            var cssLevel = CssLevel(level);
            var cssColor = color != default ? $"{cssLevel}-{color.ToString().ToLower()}" : string.Empty;

            return $"{cssLevel}-tier {cssColor}";
        }

        private string CssLevel(int level)
        {
            if(level == 0)
            {
                return "bottom";
            }
            else if (level < 4)
            {
                return "regular";
            }

            throw new Exception($"Unknown vial level (level : {level}).");
        }
    }
}
