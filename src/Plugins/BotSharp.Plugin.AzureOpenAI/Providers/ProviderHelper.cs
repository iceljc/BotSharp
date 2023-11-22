using Azure.AI.OpenAI;
using Azure;
using System;
using BotSharp.Plugin.AzureOpenAI.Settings;
using BotSharp.Abstraction.Conversations.Models;
using System.Collections.Generic;
using BotSharp.Abstraction.Agents.Enums;

namespace BotSharp.Plugin.AzureOpenAI.Providers;

public class ProviderHelper
{
    public static OpenAIClient GetClient(string model, AzureOpenAiSettings settings)
    {
        if (model.Contains("gpt-4") || model.Contains("gpt4"))
        {
            var client = new OpenAIClient(new Uri(settings.GPT4.Endpoint), new AzureKeyCredential(settings.GPT4.ApiKey));
            return client;
        }
        else
        {
            var client = new OpenAIClient(new Uri(settings.Endpoint), new AzureKeyCredential(settings.ApiKey));
            return client;
        }
    }

    public static List<RoleDialogModel> GetChatSamples(List<string> lines)
    {
        var samples = new List<RoleDialogModel>();

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }
            var role = line.Substring(0, line.IndexOf(' ') - 1).Trim();
            var content = line.Substring(line.IndexOf(' ') + 1).Trim();

            var message = new RoleDialogModel(role, content);

            // comments
            if (role == "##")
            {
                continue;
            }

            if (role == AgentRole.Assistant)
            {
                var elements = content.Split("|");
                var text = elements[0];
                message = new RoleDialogModel(role, text);

                if (elements.Length > 1)
                {
                    var functionName = elements[1];
                    var functionArgs = elements[2];
                    message.FunctionName = functionName;
                    message.FunctionArgs = functionArgs;
                }
            }

            samples.Add(message);
        }

        return samples;
    }
}
