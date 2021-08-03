using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Vials.Client.Utilities;
using Vials.Shared.Extensions;
using Shouldly;
using Vials.Shared.Objects;
using Newtonsoft.Json;
using System.Linq;

namespace Vials.Client.UnitTests.Utilities
{
    public class MoveTrackerTests
    {
        [Test]
        public void TrackerWillKeepTrack()
        {
            var tracker = new MoveTracker();

            tracker.Stack(1, 2);
            tracker.Stack(3, 4);
            tracker.Stack(5, 6);

            tracker.GetStack().IsEqualTo(new int[] { 1, 2, 3, 4, 5, 6 });
        }
    }
}
