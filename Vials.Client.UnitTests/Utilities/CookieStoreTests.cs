using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Vials.Client.Utilities;
using Shouldly;

namespace Vials.Client.UnitTests.Utilities
{
    public class CookieStoreTests
    {
        [Test]
        public async Task WillReturnCookie()
        {
            var access = new DummyCookieAccess() { 
                Value = "cookie={}; SameSite=Strict"
            };

            var store = new CookieStore(access);

            (await store.Get()).ShouldNotBeNull();
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

            await store.Store(new ApplicationCookie());

            access.Value.ShouldBe("cookie={};SameSite=Strict");
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
