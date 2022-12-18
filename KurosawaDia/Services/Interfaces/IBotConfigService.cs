namespace KurosawaDia.Services.Interfaces;

public interface IBotConfigService
{
    string BotToken { get; }
    ulong MainGuild { get; }
}
