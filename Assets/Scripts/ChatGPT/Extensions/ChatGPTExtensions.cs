using System.Linq;

public static class ChatGPTExtensions
{
    public const string KEYWORD_USING = "using UnityEngine";
    public const string KEYWORD_PUBLIC_CLASS = "public class";
    public static readonly string[] filters = { "C#", "c#", "csharp", "CSHARP" };

    public static ChatGPTResponse CodeCleanUp(this ChatGPTResponse chatGPTResponse)
    {
        // Check if Choices is null or empty
        if (chatGPTResponse.Choices == null || !chatGPTResponse.Choices.Any())
        {
            return chatGPTResponse;
        }

        var choice = chatGPTResponse.Choices.FirstOrDefault();
        var message = choice?.Message?.Content ?? string.Empty;

        // Apply filters
        filters.ToList().ForEach(f =>
        {
            message = message.Replace(f, string.Empty);
        });

        // Split due to explanations
        var codeLines = message.Split("```");

        // Extract code that contains the keywords
        var codeLine = codeLines.FirstOrDefault(c => c.Contains(KEYWORD_USING) || c.Contains(KEYWORD_PUBLIC_CLASS));
        message = codeLine?.Trim() ?? string.Empty;

        // Modify the original message content with the filtered message
        chatGPTResponse.Choices[0].Message.Content = message;

        return chatGPTResponse;
    }
}