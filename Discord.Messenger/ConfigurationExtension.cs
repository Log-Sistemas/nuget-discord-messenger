using Microsoft.Extensions.DependencyInjection;

namespace LogSistemas.Nuget.Discord.Messenger
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Configuration in case message sending fail
        /// </summary>
        /// <param name="this"></param>
        /// <param name="exceptionFallbackWebhookUrl">If message fail, it will send another message to this webhook</param>
        /// <returns></returns>
        public static IServiceCollection AddDiscordMessenger(
            this IServiceCollection @this,
            string exceptionFallbackWebhookUrl,
            string defaultAuthorName = null)
        {
            Config.ExceptionFallbackUrl = exceptionFallbackWebhookUrl;
            Config.DefaultAuthorName = defaultAuthorName;
            return @this;
        }
    }
}