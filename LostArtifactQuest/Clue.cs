using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace LostArtifactQuest
{
public class Clue
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        public string Location { get; set; }
        public string MultimediaUrl { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<string> PossibleAnswers { get; set; }

        public Clue()
        {
            PossibleAnswers = new List<string>();
        }

        public void AddPossibleAnswer(string answer)
        {
            PossibleAnswers.Add(answer);
        }

        public bool CheckAnswer(string givenAnswer)
        {
            return PossibleAnswers.Exists(answer => answer.Equals(givenAnswer, StringComparison.OrdinalIgnoreCase));
        }

        public async Task SaveToBlobStorageAsync(string connectionString)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient("clues");
            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient($"{Id}.json");
            var json = JsonSerializer.Serialize(this);

            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        public async Task RetrieveFromBlobStorageAsync(string connectionString)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient("clues");
            var blobClient = blobContainerClient.GetBlobClient($"{Id}.json");

            BlobDownloadInfo blobContent = await blobClient.DownloadAsync();
            //BlobDownloadInfo blobContent = await blobClient.OpenReadAsync();
            using var streamReader = new StreamReader(blobContent.Content);

            var clue = JsonSerializer.Deserialize<Clue>(await streamReader.ReadToEndAsync());

            Id = clue.Id;
            Description = clue.Description;
            Hint = clue.Hint;
            Location = clue.Location;
            MultimediaUrl = clue.MultimediaUrl;
            Difficulty = clue.Difficulty;
            PossibleAnswers = clue.PossibleAnswers;
        }
    }
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}
