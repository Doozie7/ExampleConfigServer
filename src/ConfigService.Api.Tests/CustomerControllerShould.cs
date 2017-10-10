using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Api.Controllers;
using ConfigService.Api.ViewModels;
using ConfigService.Model;
using ConfigService.Repository.InMemory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ConfigService.Api.Tests
{
    public class CustomerControllerShould
    {
        [Fact]
        public void Return_all_customers()
        {
            //Arrange
            var repoLogger = new Mock<ILogger<CustomersRepository>>();
            var inMemRepo = new CustomersRepository(repoLogger.Object);

            var controllerLogger = new Mock<ILogger<CustomersController>>();
            var controller = new CustomersController(inMemRepo, controllerLogger.Object);

            //Act
            var response = controller.Get();
            var viewResult = Assert.IsType<OkObjectResult>(response).Value;
            var customers = (IList<Customer>)viewResult;

            // Assert
            Assert.Equal(typeof(OkObjectResult), response.GetType());
            Assert.Equal(inMemRepo.GetListOf().Count, customers.Count);
        }

        [Fact]
        public void Return_one_customer()
        {
            //Arrange
            var repoLogger = new Mock<ILogger<CustomersRepository>>();
            var inMemRepo = new CustomersRepository(repoLogger.Object);

            var controllerLogger = new Mock<ILogger<CustomersController>>();
            var controller = new CustomersController(inMemRepo, controllerLogger.Object);

            //Act
            var response = controller.GetById(new Guid("65e51c54-21c5-41e8-8e22-21500379b275"));
            var viewResult = Assert.IsType<OkObjectResult>(response).Value;
            var customer = (Customer)viewResult;

            // Assert
            Assert.Equal(typeof(OkObjectResult), response.GetType());
            var checkCustomer = inMemRepo.GetListOf(c => c.Id == new Guid("65e51c54-21c5-41e8-8e22-21500379b275"))
                .SingleOrDefault();
            Assert.NotNull(checkCustomer);
            Assert.Equal(checkCustomer.Id, customer.Id);
        }

        [Fact]
        public void Return_notfound_getting_an_unknown_customer_id()
        {
            //Arrange
            var repoLogger = new Mock<ILogger<CustomersRepository>>();
            var inMemRepo = new CustomersRepository(repoLogger.Object);

            var controllerLogger = new Mock<ILogger<CustomersController>>();
            var controller = new CustomersController(inMemRepo, controllerLogger.Object);

            //Act
            var response = controller.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void Add_a_customer()
        {
            //Arrange
            var repoLogger = new Mock<ILogger<CustomersRepository>>();
            var inMemRepo = new CustomersRepository(repoLogger.Object);

            var origCount = inMemRepo.GetListOf().Count;

            var controllerLogger = new Mock<ILogger<CustomersController>>();
            var controller = new CustomersController(inMemRepo, controllerLogger.Object);

            var newCustomer = new CustomerFromPost
            {
                Name = "BritishMicro",
                Description = "Test Record",
                Enabled = true
            };

            //Act
            var response = controller.Post(newCustomer);
            var viewResult = Assert.IsType<CreatedAtRouteResult>(response).Value;
            var customer = (Customer)viewResult;

            // Assert
            var checkCustomer = inMemRepo.GetListOf(c => c.Id == customer.Id).SingleOrDefault();
            Assert.NotNull(checkCustomer);
            Assert.Equal(checkCustomer.Id, customer.Id);
            Assert.Equal(origCount + 1, inMemRepo.GetListOf().Count);
        }

        [Fact]
        public void Delete_a_customer()
        {
            //Arrange
            var repoLogger = new Mock<ILogger<CustomersRepository>>();
            var inMemRepo = new CustomersRepository(repoLogger.Object);

            var origCount = inMemRepo.GetListOf().Count;

            var controllerLogger = new Mock<ILogger<CustomersController>>();
            var controller = new CustomersController(inMemRepo, controllerLogger.Object);

            var newCustomer = new CustomerFromPost
            {
                Name = "Record To Delete",
                Description = "Test Record",
                Enabled = true
            };

            //Act
            // Add the record to delete
            var postAction = controller.Post(newCustomer);
            var addViewResult = Assert.IsType<CreatedAtRouteResult>(postAction).Value;
            var customer = (Customer)addViewResult;

            // Now delete the record
            var deleteAction = controller.DeleteById(customer.Id);
            Assert.IsType<NoContentResult>(deleteAction);

            // Assert
            var checkCustomer = inMemRepo.GetListOf(c => c.Id == customer.Id).SingleOrDefault();
            Assert.Null(checkCustomer);
            Assert.Equal(origCount, inMemRepo.GetListOf().Count);
        }

        [Fact]
        public void Return_notfound_deleting_an_unknown_customer_id()
        {
            //Arrange
            var repoLogger = new Mock<ILogger<CustomersRepository>>();
            var inMemRepo = new CustomersRepository(repoLogger.Object);

            var controllerLogger = new Mock<ILogger<CustomersController>>();
            var controller = new CustomersController(inMemRepo, controllerLogger.Object);

            //Act
            var response = controller.DeleteById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
