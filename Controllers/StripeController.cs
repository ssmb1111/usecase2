using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Text.Json;
using UseCase2.Models;

[Route("api/[controller]")]
[ApiController]
public class StripeController : ControllerBase
{
    [HttpGet("balance")]
    public IActionResult GetBalance()
    {
        try
        {
            var balanceService = new BalanceService();
            var balance = balanceService.Get();
            
            // Extracting the stripeResponse.content
            var content = balance.StripeResponse.Content;

            // Parse the content into our class
            var parsedBalance = JsonSerializer.Deserialize<StripeBalanceResponse>(content);
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
}