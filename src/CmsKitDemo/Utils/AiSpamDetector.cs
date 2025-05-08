using System.Text;
using Microsoft.Extensions.AI;
using OpenAI;
using Volo.Abp.DependencyInjection;

namespace CmsKitDemo.Utils;

public class AiSpamDetector : ITransientDependency
{
    private readonly IConfiguration _configuration;

    public AiSpamDetector(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> IsSpamAsync(string commentText)
    {
        // Get the model and key from the configuration
        var aiModel = _configuration["ModelName"];
        var apiKey = _configuration["OpenAIKey"];

        if (aiModel.IsNullOrEmpty() || apiKey.IsNullOrEmpty())
        {
            return false;
        }

        // Create the IChatClient
        var client = new ChatClientBuilder(
                new OpenAIClient(apiKey)
                    .GetChatClient(aiModel)
                    .AsIChatClient()
            ).UseFunctionInvocation()
            .Build();
        
        var isSpamResult = false;
        void SpamResultCallback(bool isSpam) => isSpamResult = isSpam;
         
        var promptBuilder = new StringBuilder();

        promptBuilder.AppendLine(
            @"Here is a user comment about an image in our website.
You should determine if the comment is spam or not. Call the SpamResultCallback method with a boolean value."
        );
        promptBuilder.AppendLine();
        
        promptBuilder.AppendLine(commentText);
        
        await client.GetResponseAsync(promptBuilder.ToString(),
            new ChatOptions
            {
                Tools = [AIFunctionFactory.Create(SpamResultCallback)]
            });

        return isSpamResult;
    }
}