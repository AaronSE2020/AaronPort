using LostArtifactQuest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    // Define the IsGlowing Property
    public bool IsGlowing { get; set; } = false;

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IActionResult> OnPostGenerateClueAsync()
    {
        var newClue = new Clue
        {
            Id = 1, // Set the Clue properties as needed
            Description = "A mysterious clue",
            Location = "Enchanted Forest", // Specify a location
            Hint = "Beneath the oldest oak tree, a secret awaits", // Provide a hint
            MultimediaUrl = "https://example.com/multimedia/clue1.mp4", // Add a multimedia clue
            Difficulty = DifficultyLevel.Medium
        };

        newClue.AddPossibleAnswer("Answer1");
        newClue.AddPossibleAnswer("Answer2");

        var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
        await newClue.SaveToBlobStorageAsync(connectionString);

        TempData["ResultMessage"] = "Clue created and saved successfully!";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostFetchClueAsync(int clueId)
    {
        var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
        var existingClue = new Clue { Id = clueId };
        await existingClue.RetrieveFromBlobStorageAsync(connectionString);

        // Display the retrieved clue and multimedia content if available
        // Use clueArea to display the clue
        TempData["ClueDescription"] = existingClue.Description;
        TempData["ClueHint"] = existingClue.Hint;

        return RedirectToPage();
    }
    public async Task<IActionResult> OnPostAnswerClueAsync(int clueId, string userAnswer)
    {
        var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
        var existingClue = new Clue { Id = clueId };
        await existingClue.RetrieveFromBlobStorageAsync(connectionString);

        if (existingClue.CheckAnswer(userAnswer))
        {
            TempData["ResultMessage"] = "Congratulations! You answered correctly.";

            // Play success sound effect
            PlaySuccessSound();

            // Activate visual effects
            IsGlowing = true;


        }
        else
        {
            TempData["ResultMessage"] = "Oops, that's not the right answer. Try again.";

            // Play error sound effect
            PlayErrorSound();

            // Activate visual effects
            IsGlowing = false;
        }

        return RedirectToPage();
    }

    // ...

    private void PlaySuccessSound()
    {
        // Play a success sound effect (implement as needed)
    }

    private void PlayErrorSound()
    {
        // Play an error sound effect (implement as needed)
    }
}
