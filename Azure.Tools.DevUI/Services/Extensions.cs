using Azure.AI.Projects;
using Azure.Identity;
using CustomDevAI.SimpleAgent.Configuration;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenAI.Responses;

#pragma warning disable OPENAI001

namespace CustomDevAI.SimpleAgent.Services;

public static class Extensions
{
    public static HostApplicationBuilder AddAgents(this HostApplicationBuilder builder)
    {
        builder.AddServices();
        
        builder.Services.AddSingleton<AIProjectClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<AgentSettings>>().Value;
            var endpoint = settings.ProjectEndpoint;

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new InvalidOperationException(
                    $"Bitte '{AgentSettings.SectionName}:ProjectEndpoint' in appsettings setzen.");
            }
            
            return new AIProjectClient(new Uri(endpoint), new DefaultAzureCredential());
        });
        builder.Services.AddAIAgent("Weather Agent", (provider, name) =>
        {
            var client = provider.GetRequiredService<AIProjectClient>();
            var settings = provider.GetRequiredService<IOptions<AgentSettings>>().Value;
            var agent = client.AsAIAgent(
                settings.ModelDeployment,
                settings.Instructions,
                name,
                tools:
                [
                    AIFunctionFactory.Create(provider.GetRequiredService<WeatherToolService>().GetWeather),
                    //new WebSearchTool().AsAITool()
                ]);
            return agent;
        });
        return builder;
    }

    public static HostApplicationBuilder AddServices(this HostApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<AgentSettings>()
            .Bind(builder.Configuration.GetSection(AgentSettings.SectionName));

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddSingleton<IWeatherLookupService, OpenMeteoWeatherLookupService>();
        builder.Services.AddSingleton<WeatherToolService>();
        return builder;
    }
}