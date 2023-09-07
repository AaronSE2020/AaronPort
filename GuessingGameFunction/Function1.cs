using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.Design;

namespace GuessingGameFunction
{
    public static class GuessTheNumber
    {
        private static readonly Random random = new Random();
        private static readonly int targetNumber = random.Next(1, 101);

        [FunctionName("GuessTheNumber")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request for the guessing game.");

            string guess = req.Query["guess"];
            int guessedNumber;

            if (int.TryParse(guess, out guessedNumber))
            {
                if (guessedNumber == targetNumber)
                {
                    return new OkObjectResult($"Congratulations! You guessed it. The number was {targetNumber}.");
                }
                else if (guessedNumber < targetNumber)
                {
                    return new OkObjectResult($"Too low! Try Again.");
                }
                else
                {
                    return new OkObjectResult($"Too high! Try again.");
                }
            } else
            {
                return new BadRequestObjectResult("Please provide a valid number as a guess.");
            }

        }






    }
 }

