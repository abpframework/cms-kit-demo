using System.Text;
using Microsoft.Extensions.AI;
using OpenAI;
using Volo.Abp.DependencyInjection;

namespace CmsKitDemo.Utils;

public class AiCommentSummarizer : ITransientDependency
{
    private readonly IConfiguration _configuration;

    public AiCommentSummarizer(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<string> SummarizeAsync(string[] commentTexts)
    {
        // Get the model and key from the configuration
        var aiModel = _configuration["ModelName"];
        var apiKey = _configuration["OpenAIKey"];

        // Create the IChatClient
        var client = new OpenAIClient(apiKey)
            .GetChatClient(aiModel)
            .AsIChatClient();

        var promptBuilder = new StringBuilder();

        promptBuilder.AppendLine(
            @"There are comments from different users of our website about an image.
We want to summarize the comments into a single comment.
Return a single comment with a maximum of 512 characters. Comments are separated by a newline character and given below."
        );
        promptBuilder.AppendLine();
        
        foreach (var commentText in commentTexts)
        {
            promptBuilder.AppendLine("User comment:");
            promptBuilder.AppendLine(commentText);
            promptBuilder.AppendLine();
        }
        
        // Submit the prompt and print out the response
        var response = await client.GetResponseAsync(
            promptBuilder.ToString(),
            new ChatOptions { MaxOutputTokens = 1024 }
        );

        return response.Text;
    }
}