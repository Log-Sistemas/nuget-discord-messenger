using Discord;
using Discord.Webhook;

using Newtonsoft.Json;

namespace LogSistemas.Nuget.Discord.Messenger
{
    public class DiscordWebhookBuilder
    {
        private readonly List<Embed> _embeds = new();
        private string _text;

        /// <summary>
        /// This is a message without any formatting. It is send before message card.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public DiscordWebhookBuilder AddText(string message)
        {
            _text = message;
            return this;
        }

        public DiscordWebhookBuilder AddMessageCard(Action<DiscordWebhookMessageBuilder> builder)
        {
            DiscordWebhookMessageBuilder message = new();
            builder.Invoke(message);

            _embeds.Add(message.Build());

            return this;
        }

        /// <summary>
        /// Send a message to webhook url.
        /// </summary>
        /// <remarks>
        /// If sending fail, it will check fallback url in <see cref="Config.ExceptionFallbackUrl"/>.
        /// <para>It will raise DiscordException if all attempts fails if <see cref="Config.SurpressException"/> is false</para>
        /// </remarks>
        /// <param name="webhookUrl"></param>
        /// <param name="surpressIfException">it will raise <see cref="DiscordException"/> in case message fail</param>
        /// <returns></returns>
        /// <exception cref="DiscordException">In case fallbackUrl not working or not configured</exception>
        public async Task SendMessageAsync(string webhookUrl, bool surpressIfException = true)
        {
            if (string.IsNullOrWhiteSpace(webhookUrl))
                return;

            if (!_embeds.Any())
                return;

            try
            {
                using DiscordWebhookClient webhook = new(webhookUrl);
                await webhook.SendMessageAsync(_text, embeds: _embeds);
            }
            catch (Exception e)
            {
                DiscordExceptionContent content = new()
                {
                    WebhookIds = webhookUrl.Replace("https://discord.com/api/webhooks/", ""),
                    ExceptionMessage = e.Message,
                    Message = MapCardsInformations(_embeds)
                };
                string msg = JsonConvert.SerializeObject(content);

                if (!string.IsNullOrEmpty(Config.ExceptionFallbackUrl))
                {
                    try
                    {
                        Embed fallbackEmbed = new DiscordWebhookMessageBuilder()
                            .SetTitleError("Dados")
                            .SetDescription(msg)//Description is the field with more length - 4096
                            .SetThumbnailUrl("https://uploads.sitepoint.com/wp-content/uploads/2015/12/1450973046wordpress-errors.png")
                            .SetFooter("Tratamento fallback url")
                            .Build();
                        using DiscordWebhookClient fallbackWebhook = new(Config.ExceptionFallbackUrl);
                        await fallbackWebhook.SendMessageAsync("Não foi possível realizar o envio de uma notificação, dados abaixo", embeds: new List<Embed> { fallbackEmbed });
                    }
                    catch (Exception ex)
                    {
                        if (!surpressIfException)
                            throw new DiscordException(inner: ex, content: content);
                    }
                }
                else if (!surpressIfException)
                    throw new DiscordException(inner: e, content: content);
            }
        }

        private static string MapCardsInformations(List<Embed> cards)
        {
            if (!cards.Any())
                return string.Empty;

            //Mapping to make the message smaller, description can have only 4096 length
            IEnumerable<string> cardsMessages = cards.Select(x =>
            {
                string msg = $"{x.Title} | {x.Description}";
                IEnumerable<string> fields = x.Fields.Select(y => $"{y.Name} = {y.Value.Replace("`", "")}");

                return $"{msg} | {String.Join(", ", fields)}";
            });
            string message = String.Join(", ", cardsMessages);
            return message;
        }
    }
}