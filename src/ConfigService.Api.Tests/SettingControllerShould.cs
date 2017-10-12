// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Api.Controllers;
using ConfigService.Api.ViewModels;
using ConfigService.Model;
using ConfigService.Repository.InMemory;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ConfigService.Api.Tests
{
    public class SettingControllerShould
    {
        private readonly Mock<ILogger<SettingsRepository>> _repoLogger;
        private readonly SettingsRepository _inMemRepo;
        private readonly Mock<ILogger<SettingsController>> _controllerLogger;
        private readonly SettingsController _controller;

        public SettingControllerShould()
        {
            //Arrange
            _repoLogger = new Mock<ILogger<SettingsRepository>>();
            _inMemRepo = new SettingsRepository(_repoLogger.Object);

            _controllerLogger = new Mock<ILogger<SettingsController>>();
            _controller = new SettingsController(_inMemRepo, _controllerLogger.Object);
        }

        [Fact]
        public void Return_all_settings()
        {
            //Arrange
            //Act
            var response = _controller.Get();
            var viewResult = Assert.IsType<OkObjectResult>(response).Value;
            var settings = (IList<Setting>)viewResult;

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            _inMemRepo.GetListOf().Count.Should().Be(settings.Count);
        }

        [Fact]
        public void Return_one_setting()
        {
            //Arrange
            //Act
            var response = _controller.GetById(1);
            var viewResult = Assert.IsType<OkObjectResult>(response).Value;
            var setting = (Setting)viewResult;
            var checkSetting = _inMemRepo.GetListOf(c => c.Id == 1).SingleOrDefault();

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            checkSetting.Should().NotBe(null);
            checkSetting?.Id.Should().Be(setting.Id);
        }

        [Fact]
        public void Return_notfound_getting_an_unknown_setting_id()
        {
            //Arrange
            //Act
            var response = _controller.GetById(-1);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void Add_a_setting()
        {
            //Arrange
            //Arrange
            var origCount = _inMemRepo.GetListOf().Count;

            var newSetting = new SettingFromPost()
            {
                CustomerId = Guid.NewGuid(),
                SettingTypeId = 1,
                SettingValue = "Test Value"
            };

            //Act
            var response = _controller.Post(newSetting);
            var viewResult = Assert.IsType<CreatedAtRouteResult>(response).Value;
            var setting = (Setting)viewResult;

            // Assert
            var checkSetting = _inMemRepo.GetListOf(c => c.Id == setting.Id).SingleOrDefault();
            Assert.NotNull(checkSetting);
            Assert.Equal(checkSetting.Id, setting.Id);
            Assert.Equal(origCount + 1, _inMemRepo.GetListOf().Count);
        }

        [Fact]
        public void Delete_a_setting()
        {
            //Arrange
            //Arrange

            var origCount = _inMemRepo.GetListOf().Count;

            var newSetting = new SettingFromPost()
            {
                CustomerId = Guid.NewGuid(),
                SettingTypeId = 1,
                SettingValue = "Test Value"
            };

            //Act
            // Add the record to delete
            var postAction = _controller.Post(newSetting);
            var addViewResult = Assert.IsType<CreatedAtRouteResult>(postAction).Value;
            var setting = (Setting)addViewResult;

            // Now delete the record
            var deleteAction = _controller.DeleteById(setting.Id);
            Assert.IsType<NoContentResult>(deleteAction);

            // Assert
            var checkSetting = _inMemRepo.GetListOf(c => c.Id == setting.Id).SingleOrDefault();
            Assert.Null(checkSetting);
            Assert.Equal(origCount, _inMemRepo.GetListOf().Count);
        }

        [Fact]
        public void Return_notfound_deleting_an_unknown_setting_id()
        {
            //Arrange
            //Act
            var response = _controller.DeleteById(int.MaxValue);

            // Assert
            response.Should().BeOfType<NotFoundResult>();
        }
    }
}
