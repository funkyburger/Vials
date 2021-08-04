﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Utilities
{
    public interface IColorStackFactory
    {
        IEnumerable<Color> GenerateStack(int numberOfColors, int seed);
    }
}