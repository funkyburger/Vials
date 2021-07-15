using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Vials.Client.Service;
using Vials.Client.Utilities;
using Shouldly;
using System.Threading.Tasks;
using Moq;
using Vials.Client.Exceptions;

namespace Vials.Client.UnitTests.Service
{
    public class CookieServiceTests
    {
        [Test]
        public async Task UserDidNotConsentIfNoCookieIsStored()
        {
            var storeMock = new Mock<ICookieStore>();

            var service = new CookieService(storeMock.Object);

            (await service.DidUserConsent()).ShouldBe(false);
        }

        [Test]
        public async Task UserDidConsentIfDefaultCookieIsStored()
        {
            var storeMock = new Mock<ICookieStore>();
            storeMock.Setup(s => s.Get()).ReturnsAsync(new ApplicationCookie());

            var service = new CookieService(storeMock.Object);

            (await service.DidUserConsent()).ShouldBe(true);
        }

        [Test]
        public async Task UserConsentIsMarkedByStoringDefaultCookie()
        {
            var storeMock = new Mock<ICookieStore>();
            storeMock.Setup(s => s.Store(It.IsAny<ApplicationCookie>()))
                .Callback<ApplicationCookie>(
                    c => storeMock.Setup(s => s.Get())
                        .ReturnsAsync(c));

            var service = new CookieService(storeMock.Object);

            (await service.DidUserConsent()).ShouldBe(false);
            await service.MarkUserDidConsent();
            (await service.DidUserConsent()).ShouldBe(true);
        }

        [Test]
        public async Task SettingCookieWithoutConsentThrowsException()
        {
            var exceptionThrown = false;
            var storeMock = new Mock<ICookieStore>();
            storeMock.Setup(s => s.Store(It.IsAny<ApplicationCookie>()))
                .Callback<ApplicationCookie>(
                    c => storeMock.Setup(s => s.Get())
                        .ReturnsAsync(c));

            var service = new CookieService(storeMock.Object);

            (await service.DidUserConsent()).ShouldBe(false);

            try
            {
                await service.SetCookie(new ApplicationCookie());
            }
            catch(CookieConsentExcpetion e)
            {
                exceptionThrown = true;
            }

            exceptionThrown.ShouldBeTrue();
        }

        [Test]
        public async Task SettingCookieAfterConsentDoesntThrowException()
        {
            var storeMock = new Mock<ICookieStore>();
            storeMock.Setup(s => s.Store(It.IsAny<ApplicationCookie>()))
                .Callback<ApplicationCookie>(
                    c => storeMock.Setup(s => s.Get())
                        .ReturnsAsync(c));

            var service = new CookieService(storeMock.Object);

            (await service.DidUserConsent()).ShouldBe(false);
            await service.MarkUserDidConsent();
            await service.SetCookie(new ApplicationCookie());
        }
    }
}
