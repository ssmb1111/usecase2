# UseCase2

## Description

This application is a simple backend service designed to integrate with Stripe using the Stripe.NET library. It provides API endpoints to query balance information and balance transactions from Stripe. By utilizing ASP.NET controllers, the application is able to cleanly and efficiently handle incoming HTTP requests, process them through the Stripe.NET library, and return the appropriate data or error messages to the client. In essence, it serves as a bridge between the user or frontend application and Stripe, simplifying the interaction with the Stripe system.

Under the hood, the app uses dependency injection for essential services like `BalanceService` and `BalanceTransactionService` from the Stripe.NET library. Exception handling is a notable feature; the application distinguishes between Stripe-specific exceptions and general exceptions, thereby offering more clarity to users when issues arise. By handling these exceptions, the application can provide a more user-friendly experience and robust performance.

## Running the Application Locally

1. Ensure you have ASP.NET Core SDK installed on your machine.
2. Clone the repository to your local machine.
3. Navigate to the project directory in your terminal or command prompt.
4. Set up your Stripe API key by adding it to your environment variables or your app settings.
5. Run the command `dotnet restore` to restore necessary packages.
6. Run the command `dotnet run` to start the application.
7. The application should now be running on the URL displayed in a console.

## Example URLs

1. **Fetching Balance**: To retrieve the current balance, you can make a `GET` request to:
    ```
    <url>/api/stripe/balance
    ```

2. **Fetching Balance Transactions with Pagination**: To get balance transactions, use the following URL structure with query parameters for pagination:
    ```
    <url>/api/stripe/balance-transactions?Limit=10&Offset=20
    ```
    This example fetches balance transactions, starting after the 20th transaction and limiting the results to 10 items. Adjust the `Limit` and `Offset` parameters as needed.