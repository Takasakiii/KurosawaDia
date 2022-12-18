using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using KurosawaDia.Extensions;
using KurosawaDia.Services;
using KurosawaDia.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

var client = new DiscordSocketClient();
var interactions = new InteractionService(client);

services.AddSingleton<IDiscordClient>(client);
services.AddSingleton<InteractionService>(interactions);

#region AddProjectDependencies
services.AddConfig();
services.AddEvents();
services.AddScoped<ActivatorService>();
#endregion

var dependencies = services.BuildServiceProvider();

var assembly = Assembly.GetExecutingAssembly();
await interactions.AddModulesAsync(assembly, dependencies);


dependencies.GetRequiredService<ActivatorService>()?.ActiveAllServices();

await client.LoginAsync(
    TokenType.Bot,
    dependencies
        .GetRequiredService<IBotConfigService>()?
        .BotToken
            ?? throw new Exception("Bot Token isn't defined"));

await client.StartAsync();
await Task.Delay(Timeout.Infinite);