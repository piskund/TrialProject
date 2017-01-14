// -------------------------------------------------------------------------------------------------------------
//  AutofixtureHelpers.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTests.Backup.Web.API.AutoFixture
{
    /// <summary>
    /// Auxilary methods for unit tests.
    /// </summary>
    public static class AutofixtureHelpers
    {
        /// <summary>
        /// Gets the mocked API controller.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture">The fixture.</param>
        /// <returns>An api controller.</returns>
        public static T GetMockedApiController<T>(IFixture fixture = null) where T : ApiController
        {
            if (fixture == null)
            {
                fixture = new Fixture();
            }
            fixture.Customize(new AutoMoqCustomization());
            fixture.Customize(new HttpRequestContextWithoutClientCertificateCustomization());
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockRouteData = new Mock<RouteData>();

            fixture.Register(() => mockHttpContext.Object);
            fixture.Register(() => mockRouteData.Object);
            fixture.Register(() => new RequestContext(mockHttpContext.Object, mockRouteData.Object));
            var controller = fixture.Create<T>();

            return controller;
        }
    }
}