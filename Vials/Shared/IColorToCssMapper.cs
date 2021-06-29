using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    // TODO : move to client
    public interface IColorToCssMapper
    {
        string Map(int level, Color color);
    }
}
