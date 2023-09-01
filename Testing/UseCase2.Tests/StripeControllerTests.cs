using Microsoft.AspNetCore.Mvc;
using Moq;
using Stripe;
using System.Net;
using System.Text.Json;
using UseCase2.Controllers;
using UseCase2.Models;

namespace UseCase2.Tests
{
    [TestClass]
    public class StripeControllerTests
    {
        private readonly Mock<BalanceService> _balanceServiceMock;
        private readonly Mock<BalanceTransactionService> _balanceTransactionServiceMock;
        private readonly JsonSerializerOptions _jsonOptions;

        public StripeControllerTests()
        {
            _balanceServiceMock = new Mock<BalanceService>();
            _balanceTransactionServiceMock = new Mock<BalanceTransactionService>();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [TestMethod]
        public void GetBalance_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var fakeBalanceResponse = new Balance // Set up your mock Balance object
            {
                StripeResponse = new StripeResponse(HttpStatusCode.OK, null, "{\r\n    \"object\": \"balance\",\r\n    \"available\": [\r\n        {\r\n            \"amount\": 507567172,\r\n            \"currency\": \"pln\",\r\n            \"source_types\": {\r\n                \"card\": 507567172\r\n            }\r\n        }\r\n    ],\r\n    \"connect_reserved\": [\r\n        {\r\n            \"amount\": 0,\r\n            \"currency\": \"pln\"\r\n        }\r\n    ],\r\n    \"livemode\": false,\r\n    \"pending\": [\r\n        {\r\n            \"amount\": 128246,\r\n            \"currency\": \"pln\",\r\n            \"source_types\": {\r\n                \"card\": 128246\r\n            }\r\n        }\r\n    ]\r\n}")
            };
            _balanceServiceMock.Setup(s => s.Get(null)).Returns(fakeBalanceResponse);

            var controller = new StripeController(_balanceServiceMock.Object, _balanceTransactionServiceMock.Object, _jsonOptions);

            // Act
            var result = controller.GetBalance();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetBalance_ReturnsBadRequest_WhenStripeExceptionOccurs()
        {
            // Arrange
            _balanceServiceMock.Setup(s => s.Get(null)).Throws(new StripeException("Error"));

            var controller = new StripeController(_balanceServiceMock.Object, _balanceTransactionServiceMock.Object, _jsonOptions);

            // Act
            var result = controller.GetBalance();

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetBalanceTransactions_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var fakeTransactionList = new StripeList<BalanceTransaction>
            {
                StripeResponse = new StripeResponse(HttpStatusCode.OK, null, "{\r\n  \"object\": \"list\",\r\n  \"data\": [\r\n    {\r\n      \"id\": \"txn_3NlVHeJX9HHJ5byc14sjLQoH\",\r\n      \"object\": \"balance_transaction\",\r\n      \"amount\": 1000,\r\n      \"availableOn\": 0,\r\n      \"created\": 1693566792,\r\n      \"currency\": \"pln\",\r\n      \"description\": null,\r\n      \"exchangeRate\": null,\r\n      \"fee\": 116,\r\n      \"feeDetails\": [],\r\n      \"net\": 884,\r\n      \"reportingCategory\": null,\r\n      \"source\": \"py_3NlVHeJX9HHJ5byc1LHVqnhm\",\r\n      \"status\": \"pending\",\r\n      \"type\": \"payment\"\r\n    }\r\n  ],\r\n  \"hasMore\": false,\r\n  \"url\": \"/v1/balance_transactions\"\r\n}")
            };
            _balanceTransactionServiceMock.Setup(s => s.List(It.IsAny<BalanceTransactionListOptions>(), null)).Returns(fakeTransactionList);

            var controller = new StripeController(_balanceServiceMock.Object, _balanceTransactionServiceMock.Object, _jsonOptions);

            // Act
            var result = controller.GetBalanceTransactions(new PaginationDto { Limit = 10, Offset = "offset_value" });

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetBalanceTransactions_ReturnsBadRequest_WhenStripeExceptionOccurs()
        {
            // Arrange
            _balanceTransactionServiceMock.Setup(s => s.List(It.IsAny<BalanceTransactionListOptions>(), null)).Throws(new StripeException("Error"));

            var controller = new StripeController(_balanceServiceMock.Object, _balanceTransactionServiceMock.Object, _jsonOptions);

            // Act
            var result = controller.GetBalanceTransactions(new PaginationDto { Limit = 10, Offset = "offset_value" });

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}