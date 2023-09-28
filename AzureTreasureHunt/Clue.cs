using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Storage.Blobs;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;


namespace AzureTreasureHunt
{
	public class Clue
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public DifficultyLevel Difficulty { get; set; }

		public List<string> PossibleAnswers { get; set; }
		public Clue()
		{
			PossibleAnswers = new List<string>();
		}

		public bool CheckAnswer(string givenAnswer)
		{
			return PossibleAnswers.Any(answer => answer.Equals(givenAnswer, "StringFromStorageContainer"));
		}

		public override string ToString()
		{
			return $"Id: {Id}, Description: {Description}, Location: {Location}, Difficulty: {Difficulty}";
		}

		public async Task SaveToBlobStorageAsync()
		{
			string connectionString = "YourActualAzureBlobStorageConnectionStringHere";
			string containerName = "YourContainerNameHere";

			var blobServiceClient = new BlobServiceClient(connectionString);
			var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

			// Create the container if it doesn't exist
			await blobContainerClient.CreateIfNotExistsAsync();

			var blobClient = blobContainerClient.GetBlobClient($"{Id}.json");

			// Serialize this object to JSON

			var json = JsonSerializer.Serialize(this);

			using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
		}
	}
	public enum DifficultyLevel
	{
		Easy,
		Medium,
		Hard
	}

	
}
	


