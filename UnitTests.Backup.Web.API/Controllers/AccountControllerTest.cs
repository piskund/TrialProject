using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;
using Backup.Web.API;
using Backup.Web.API.Controllers;
using Backup.Web.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using UnitTests.Backup.Web.API.AutoFixture;
using UrlHelper = System.Web.Mvc.UrlHelper;

namespace UnitTests.Backup.Web.API.Controllers
{

    [TestClass]
    public class AccountControllerTest
    {

        //[TestMethod]
        //public void ChangePassword_Post_ReturnsRedirectOnSuccess()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    var model = new ChangePasswordBindingModel()
        //    {
        //        OldPassword = "goodOldPassword",
        //        NewPassword = "goodNewPassword",
        //        ConfirmPassword = "goodNewPassword"
        //    };

        //    // Act
        //    var result = controller.ChangePassword(model);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //    //Assert.AreEqual("ChangePasswordSuccess", ((RedirectToRouteResult)result).RouteValues["action"]);
        //}

        //[TestMethod]
        //public void ChangePassword_Post_ReturnsViewIfChangePasswordFails()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    var model = new ChangePasswordBindingModel()
        //    {
        //        OldPassword = "goodOldPassword",
        //        NewPassword = "badNewPassword",
        //        ConfirmPassword = "badNewPassword"
        //    };

        //    // Act
        //    var result = controller.ChangePassword(model);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    //ViewResult viewResult = (ViewResult)result;
        //    //Assert.AreEqual(model, viewResult.ViewData.Model);
        //    //Assert.AreEqual("The current password is incorrect or the new password is invalid.", controller.ModelState[""].Errors[0].ErrorMessage);
        //}

        //[TestMethod]
        //public void ChangePassword_Post_ReturnsViewIfModelStateIsInvalid()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    var model = new ChangePasswordBindingModel()
        //    {
        //        OldPassword = "goodOldPassword",
        //        NewPassword = "goodNewPassword",
        //        ConfirmPassword = "goodNewPassword"
        //    };
        //    controller.ModelState.AddModelError("", "Dummy error message.");

        //    // Act
        //    var result = controller.ChangePassword(model);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    //ViewResult viewResult = (ViewResult)result;
        //    //Assert.AreEqual(model, viewResult.ViewData.Model);
        //}

        //[TestMethod]
        //public void LogOn_Post_ReturnsRedirectOnSuccess_WithoutReturnUrl()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    LogOnModel model = new LogOnModel()
        //    {
        //        UserName = "someUser",
        //        Password = "goodPassword",
        //        RememberMe = false
        //    };

        //    // Act
        //    ActionResult result = controller.LogOn(model, null);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //    RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
        //    Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
        //    Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        //    Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignIn_WasCalled);
        //}

        //[TestMethod]
        //public void LogOn_Post_ReturnsRedirectOnSuccess_WithReturnUrl()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    LogOnModel model = new LogOnModel()
        //    {
        //        UserName = "someUser",
        //        Password = "goodPassword",
        //        RememberMe = false
        //    };

        //    // Act
        //    ActionResult result = controller.LogOn(model, "/someUrl");

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectResult));
        //    RedirectResult redirectResult = (RedirectResult)result;
        //    Assert.AreEqual("/someUrl", redirectResult.Url);
        //    Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignIn_WasCalled);
        //}

        //[TestMethod]
        //public void LogOn_Post_ReturnsViewIfModelStateIsInvalid()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    LogOnModel model = new LogOnModel()
        //    {
        //        UserName = "someUser",
        //        Password = "goodPassword",
        //        RememberMe = false
        //    };
        //    controller.ModelState.AddModelError("", "Dummy error message.");

        //    // Act
        //    ActionResult result = controller.LogOn(model, null);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    ViewResult viewResult = (ViewResult)result;
        //    Assert.AreEqual(model, viewResult.ViewData.Model);
        //}

        //[TestMethod]
        //public void LogOn_Post_ReturnsViewIfValidateUserFails()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    LogOnModel model = new LogOnModel()
        //    {
        //        UserName = "someUser",
        //        Password = "badPassword",
        //        RememberMe = false
        //    };

        //    // Act
        //    ActionResult result = controller.LogOn(model, null);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    ViewResult viewResult = (ViewResult)result;
        //    Assert.AreEqual(model, viewResult.ViewData.Model);
        //    Assert.AreEqual("The user name or password provided is incorrect.", controller.ModelState[""].Errors[0].ErrorMessage);
        //}

        //[TestMethod]
        //public void OnActionExecuting_SetsViewData()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();

        //    // Act
        //    ((IActionFilter)controller).OnActionExecuting(null);

        //    // Assert
        //    Assert.AreEqual(10, controller.ViewData["PasswordLength"]);
        //}

        //[TestMethod]
        //public void Register_Get_ReturnsView()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();

        //    // Act
        //    ActionResult result = controller.Register();

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //}

        //[TestMethod]
        //public void Register_Post_ReturnsRedirectOnSuccess()
        //{
        //    // Arrange
        //    AccountController controller = GetAccountController();
        //    RegisterModel model = new RegisterModel()
        //    {
        //        UserName = "someUser",
        //        Email = "goodEmail",
        //        Password = "goodPassword",
        //        ConfirmPassword = "goodPassword"
        //    };

        //    // Act
        //    ActionResult result = controller.Register(model);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        //    RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
        //    Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
        //    Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        //}

        //[TestMethod]
        //public void Register_Post_ReturnsViewIfRegistrationFails()
        //{
        //    // Arrange
        //    var controller = GetAccountController();
        //    var model = new RegisterBindingModel()
        //    {
        //        Email = "goodEmail",
        //        Password = "goodPassword",
        //        ConfirmPassword = "goodPassword"
        //    };

        //    // Act
        //    var result = controller.Register(model).Result;

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    ViewResult viewResult = (ViewResult)result;
        //    Assert.AreEqual(model, viewResult.ViewData.Model);
        //    Assert.AreEqual("Username already exists. Please enter a different user name.", controller.ModelState[""].Errors[0].ErrorMessage);
        //}

        //[TestMethod]
        //public void Register_Post_ReturnsIfModelStateIsValid()
        //{
        //    // Arrange
        //    var userManager = new Mock<ApplicationUserManager>();
        //    userManager.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>()))
        //    var controller = GetAccountController();
        //    var model = new RegisterBindingModel()
        //    {
        //        Email = "goodEmail",
        //        Password = "goodPassword",
        //        ConfirmPassword = "goodPassword"
        //    };

        //    // Act
        //    var result = controller.Register(model).Result;

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        //    var invalidResult = (InvalidModelStateResult)result;
        //    Assert.AreEqual(controller.ModelState, invalidResult.ModelState);
        //}

        [TestMethod]
        public void Register_Post_ReturnsInvalidModelStateResultIfModelStateIsInvalid()
        {
            // Arrange
            var controller = GetAccountController();
            var model = new RegisterBindingModel()
            {
                Email = "goodEmail",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };
            controller.ModelState.AddModelError("", "Dummy error message.");

            // Act
            var result = controller.Register(model).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
            var invalidResult = (InvalidModelStateResult)result;
            Assert.AreEqual(controller.ModelState, invalidResult.ModelState);
        }

        [TestMethod]
        public void GetUserInfo_ReturnsUserInfoModel()
        {
            // Arrange
            var controller = GetAccountController();

            // Act
            var result = controller.GetUserInfo();

            // Assert
            Assert.IsInstanceOfType(result, typeof(UserInfoViewModel));
        }

        private static AccountController GetAccountController(Fixture fixture = null)
        {
            return AutofixtureHelpers.GetMockedApiController<AccountController>(fixture);
        }
    }
}
