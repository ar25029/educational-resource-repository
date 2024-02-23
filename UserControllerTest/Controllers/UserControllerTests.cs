using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserController.Controllers;
using UserController.data;
using UserController.Models.RequestModel;
using UserController.Models.ResponseModel;
using UserController.Services;

namespace UserControllerTest.Controllers
{
 
    public class UserControllerTests
    {
        [Fact]
        public async Task Register_ValidModel_ReturnsOkResult()
        {
           
            // Arrange
            var model = new RegisterModel
            {
                Username = "test",
                Role = "User",
                Email = "test12@example.com",
                Password = "password",
                Standard = 7,
                Roll = 32,
               
            };

            var mockUserServices = new Mock<IUserServices>();
            mockUserServices.Setup(s => s.CreateUser(It.IsAny<RegisterModel>())).ReturnsAsync(new RegisterResponseModel());

            var mockConfiguration = new Mock<IConfiguration>();

            //Inmemory database for testing
            var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new UserDbContext(options))
            {
                var controller = new UserControllers(mockUserServices.Object, mockConfiguration.Object, context);

                // Act
                var result = await controller.Register(model);

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }

           
        }

        //[Fact]
        //public async Task Register_InvalidModelState_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var model = new RegisterModel
        //    {
        //        // Provide invalid model data here
        //    };

        //    var controller = new UserController();

        //    controller.ModelState.AddModelError("key", "error message");

        //    // Act
        //    var result = await controller.Register(model);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public async Task Register_ExistingEmail_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var model = new RegisterModel
        //    {
        //        // Provide model data with existing email
        //    };

        //    var mockUserServices = new Mock<IUserServices>();
        //    mockUserServices.Setup(s => s.CreateUser(It.IsAny<RegisterModel>())).ReturnsAsync((RegisterResponseModel)null); // Simulate existing email

        //    var controller = new UserController(mockUserServices.Object);

        //    // Act
        //    var result = await controller.Register(model);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public async Task Register_ExistingUsername_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var model = new RegisterModel
        //    {
        //        // Provide model data with existing username
        //    };

        //    var mockUserServices = new Mock<IUserServices>();
        //    mockUserServices.Setup(s => s.CreateUser(It.IsAny<RegisterModel>())).ReturnsAsync((RegisterResponseModel)null); // Simulate existing username

        //    var controller = new UserController(mockUserServices.Object);

        //    // Act
        //    var result = await controller.Register(model);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public async Task Register_UserCreationSuccess_ReturnsOkResult()
        //{
        //    // Arrange
        //    var model = new RegisterModel
        //    {
        //        // Provide valid model data here
        //    };

        //    var mockUserServices = new Mock<IUserServices>();
        //    mockUserServices.Setup(s => s.CreateUser(It.IsAny<RegisterModel>())).ReturnsAsync(new RegisterResponseModel());

        //    var controller = new UserController(mockUserServices.Object);

        //    // Act
        //    var result = await controller.Register(model);

        //    // Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}

    }
}
