using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    [Obsolete("Use IEventHandler instead.")]
    public interface IClickHandler
    {
        void Handle(object sender);
    }
}
