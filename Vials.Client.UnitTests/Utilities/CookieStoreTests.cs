using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Vials.Client.Utilities;
using Shouldly;
using Vials.Shared.Objects;
using Newtonsoft.Json;
using System.Linq;

namespace Vials.Client.UnitTests.Utilities
{
    public class CookieStoreTests
    {
        [Test]
        public async Task WillReturnCookie()
        {
            var access = new DummyCookieAccess() { 
                Value = "cookie={\"VialSet\":{\"Vials\":[{\"Colors\":[3,1]},{\"Colors\":[6,2]},{\"Colors\":[]}]}};SameSite=Strict"
            };

            var store = new CookieStore(access);

            var vialset = (await store.Get()).VialSet;

            vialset.Vials.Count().ShouldBe(3);
            vialset.Vials.First().Colors.ShouldBe(new Color[] { Color.Blue, Color.Red });
            vialset.Vials.Skip(1).First().Colors.ShouldBe(new Color[] { Color.Green, Color.Yellow });
            vialset.Vials.Skip(2).First().Colors.ShouldBeEmpty();
        }

        [Test]
        public async Task NoCookieWillReturnNull()
        {
            var access = new DummyCookieAccess()
            {
                Value = null
            };

            var store = new CookieStore(access);

            (await store.Get()).ShouldBeNull();
        }

        [Test]
        public async Task WillStoreCookie()
        {
            var access = new DummyCookieAccess();

            var store = new CookieStore(access);

            await store.Store(new ApplicationCookie() { 
                VialSet = new VialSet()
                {
                    Vials = new Vial[] { 
                        new Vial() { Colors = new Color[] { Color.Blue, Color.Red } },
                        new Vial() { Colors = new Color[] { Color.Green, Color.Yellow } },
                        new Vial() { Colors = new Color[] { } }
                    }
                }
            });

            access.Value.ShouldBe("cookie={\"VialSet\":{\"Vials\":[{\"Colors\":[3,1]},{\"Colors\":[6,2]},{\"Colors\":[]}]},\"History\":null};SameSite=Strict");
        }

        [Test]
        public async Task NullWillClearCookie()
        {
            var access = new DummyCookieAccess()
            {
                Value = "cookie={};SameSite=Strict"
            };

            var store = new CookieStore(access);

            await store.Store(null);

            access.Value.ShouldBeNull();
        }

        [Test]
        public async Task RoundTripWillWork()
        {
            var access = new DummyCookieAccess();

            var store = new CookieStore(access);

            await store.Store(new ApplicationCookie());
            access.Value.ShouldNotBeNull();

            var cookie = await store.Get();
            cookie.ShouldNotBeNull();
        }
    }

    public class DummyCookieAccess : ICookieAccess
    {
        public string Value { get; set; }

        public Task<string> Get()
        {
            return Task.FromResult(Value);
        }

        public Task Set(string value)
        {
            Value = value;
            return Task.CompletedTask;
        }
    }
}
