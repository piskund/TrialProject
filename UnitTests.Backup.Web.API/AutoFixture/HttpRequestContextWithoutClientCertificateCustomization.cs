// -------------------------------------------------------------------------------------------------------------
//  HttpRequestContextWithoutClientCertificateCustomization.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System.Web.Http.Controllers;
using Ploeh.AutoFixture;

namespace UnitTests.Backup.Web.API.AutoFixture
{
    public class HttpRequestContextWithoutClientCertificateCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<HttpRequestContext>(c => c.Without(x => x.ClientCertificate));
        }
    }
}