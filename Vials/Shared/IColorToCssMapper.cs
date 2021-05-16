using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface IColorToCssMapper
    {
        string Map(int level, Color color);
    }
}
