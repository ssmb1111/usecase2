using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Text.Json;
using UseCase2.Models;

namespace UseCase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly BalanceService _balanceService;
        private readonly BalanceTransactionService _balanceTransactionService;

        public StripeController(BalanceService balanceService, BalanceTransactionService balanceTransactionService, JsonSerializerOptions jsonOptions)
        {
            _jsonOptions = jsonOptions;
            _balanceService = balanceService;
            _balanceTransactionService = balanceTransactionService;
        }

        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            try
            {
                var balance = _balanceService.Get();

                // Extracting the stripeResponse.content
                var content = balance.StripeResponse.Content;

                // Parse the content into our class
                var parsedBalance = JsonSerializer.Deserialize<StripeBalanceResponse>(content, _jsonOptions);
                return Ok(parsedBalance);
            }
            catch (StripeException e)
            {
                // Handle Stripe-specific exceptions
                return BadRequest(new { error = e.Message });
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("balance-transactions")]
        public IActionResult GetBalanceTransactions([FromQuery] PaginationDto pagination)
        {
            var options = new BalanceTransactionListOptions
            {
                Limit = pagination.Limit,
                StartingAfter = pagination.Offset
            };

            StripeList<BalanceTransaction> balanceTransactions;

            try
            {
                balanceTransactions = _balanceTransactionService.List(options);
                var parsedBalanceTransactions = JsonSerializer.Deserialize<StripeBalanceTransactionsResponse>(balanceTransactions.StripeResponse.Content, _jsonOptions);
                return Ok(parsedBalanceTransactions);
            }
            catch (StripeException e)
            {
                // Handle the exception appropriately (e.g., log it, return an error response, etc.)
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}