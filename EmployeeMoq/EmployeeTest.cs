using EmployeeAPI.Controllers;
using EmployeeAPI.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeMoq
{
    public class EmployeeTest
    {
        [Fact]
        public void NormalGetEmployee_ReturnsEqual()
        {
            //Arrange
            IEmployeeService employeeService = new EmployeeService();
            var controller = new EmployeeController(employeeService);

            //Act
            var action=controller.GetEmployee(1);
            var response = (OkObjectResult)action.Result;
            var dtoResponse = (Employee)response.Value;

            //Assert
            dtoResponse.Id.Should().Be(1);
            Assert.Equal(1, dtoResponse.Id);
        }


        [Fact]
        public void GetEmployees_ReturnsCountAs2()
        {
            //Arrange
            IEmployeeService employeeService = new EmployeeService();
            var controller = new EmployeeController(employeeService);

            //Act
            var employees = controller.GetEmployees();

            //Assert
            employees.Should().HaveCount(2);

        }

        [Fact]
        public void GetEmployee_VerifyEmployeeServiceCalled()
        {
            //Arrange
            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>())).Returns(new Employee
            {
                Id = 3,
                Name = "pqr"
            });
            var controller = new EmployeeController(mockEmployeeService.Object);

            //Act
            var employee = controller.GetEmployee(2);
            var response = (OkObjectResult)employee.Result;
            var dtoResponse = (Employee)response.Value;

            //Assert
            dtoResponse.Id.Should().Be(3);
            mockEmployeeService.Verify(x => x.GetEmployee(2), Times.Once, "Get Employee method of IEMployee Service is not called");
        }

        [Fact]
        public void GetEmployee_ThrowExceptionWhenIdIsLessThanOrEqualsTo0()
        {
            //Arrange
            IEmployeeService employeeService = new EmployeeService();
            var controller = new EmployeeController(employeeService);

            //Assert
            Assert.Throws<Exception>(() => controller.GetEmployee(0));
        }
    }
}
