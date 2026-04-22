using CustomDevAI.SimpleAgent.Services;
using Microsoft.Agents.AI.DevUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CustomDevUI.DevUI_Example;

var builder = WebApplication.CreateBuilder(args);
builder.AddAgents();
builder.AddDevUI();
builder.AddOpenAIResponses();
builder.AddOpenAIConversations();
builder.AddDevUI();

var app = builder.Build();

app.MapOpenAIConversations();
app.MapOpenAIResponses();

// maps to http://localhost:5000/devui/
app.MapDevUI();
app.Run();
