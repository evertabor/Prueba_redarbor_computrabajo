using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CleanArchitectrure.Application.UseCases.Employees.Commands.CreateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.DeleteEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetAllEmployeeQuery;
using CleanArchitectrure.Application.UseCases.Employees.Queries.GetByIdEmployeeQuery;
using CleanArchitectrure.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitectrure.Tests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EmployeeController _controller;
        private readonly Mock<IMapper> _mapperMock;

        public EmployeeControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new EmployeeController(_mediatorMock.Object, _mapperMock.Object);

        }

        [Fact]
        public async Task GetAllAsync_ValidResponse_ReturnsOkResult()
        {
            var expectedResponse = new BaseResponse<IEnumerable<EmployeeDto>>
            {
                succcess = true,
                Message = "Query succeed!"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllEmployeeQuery>(), default))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.GetAllAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<BaseResponse<IEnumerable<EmployeeDto>>>(okResult.Value);

            Assert.True(actualResponse.succcess);
        }


        [Fact]
        public async Task GetIdAsync_InvalidId_ReturnsBadRequest()
        {
            int employeeId = 99;
            // Arrange
            var expectedResponse = new BaseResponse<EmployeeDto>
            {
                succcess = false,
                Data = null,
                Message = $"El empleado con el Id ({employeeId}) no existe"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetByIdEmployeeQuery>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetIdAsync(employeeId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResponse = Assert.IsType<BaseResponse<EmployeeDto>>(badRequestResult.Value);

            Assert.False(actualResponse.succcess);
            Assert.Null(actualResponse.Data);
        }


        [Fact]
        public async Task InsertAsync_ValidCommand_ReturnsOkResult()
        {
            var employeeDto = new EmployeeDto
            {
                Name = "Ever Yesid Acevedo Taborda",
                Email = "ever.acevedo@redabor.com",
                CompanyId = 1,
                Fax = "874636326",
                Password = "Temporal01#",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "3102618639",
                Username = "evertabor"
            };

            var expectedResponse = new BaseResponse<bool>
            {
                succcess = true,
                Data = true,
                Message = "Create succeed!"
            };

 
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), default))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.InsertAsync(employeeDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<BaseResponse<bool>>(okResult.Value);

            Assert.True(actualResponse.succcess);
            Assert.Equal(expectedResponse.Message, actualResponse.Message);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_ReturnsOkResult()
        {
            // Arrange
            var expectedResponse = new BaseResponse<bool>
            {
                succcess = true,
                Data = true,
                Message = "Delete succeed!"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteEmployeeCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.DeleteAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<BaseResponse<bool>>(okResult.Value);

            Assert.True(actualResponse.succcess);
            Assert.True(actualResponse.Data);
            Assert.Equal("Delete succeed!", actualResponse.Message);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ReturnsNotFound()
        {
            int employeeId = 1;

            // Arrange
            var expectedResponse = new BaseResponse<bool>
            {
                succcess = true,
                Data = true,
                Message = $"El empleado con el Id ({employeeId}) no existe"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteEmployeeCommand>(), default))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.DeleteAsync(1);

            var notFoundResult = Assert.IsType<OkObjectResult>(result);
            var responseApi = Assert.IsType<BaseResponse<bool>>(notFoundResult.Value);

            Assert.Equal($"El empleado con el Id ({employeeId}) no existe", responseApi.Message);
        }

        [Fact]
        public async Task UpdateAsync_ValidRequest_ReturnsOkResult()
        {
            var employeeUpdate = new EmployeeDto
            {
                Id = 1,
                Name = "Ever Yesid ACTUALIZADO Taborda",
                Email = "ever.acevedo@redabor.com",
                CompanyId = 1,
                Fax = "874636326",
                Password = "Temporal01#",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "3102618639",
                Username = "evertabor"
            };

            var expectedResponse = new BaseResponse<bool>
            {
                succcess = true,
                Data = true,
                Message = "Update succeed!"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateEmployeeCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.UpdateAsync(employeeUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResponse = Assert.IsType<BaseResponse<bool>>(okResult.Value);

            Assert.True(actualResponse.succcess);
            Assert.True(actualResponse.Data);
            Assert.Equal("Update succeed!", actualResponse.Message);
        }
    }
}