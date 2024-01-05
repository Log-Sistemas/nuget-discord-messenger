using Discord;

namespace LogSistemas.Nuget.Discord.Messenger
{
    public class DiscordWebhookMessageBuilder
    {
        private const int _max_description_lenght = 4096 - 6; //6 is from triple quotes
        private const int _max_title_lenght = 256;
        private const int _max_author_lenght = 256;
        private const int _max_footer_lenght = 2048;
        private const int _max_fieldname_lenght = 256;
        private const int _max_fieldvalue_lenght = 1024;
        private EmbedBuilder embedBuilder = new();

        public DiscordWebhookMessageBuilder()
        {
            if (!string.IsNullOrWhiteSpace(Config.DefaultAuthorName))
                SetAuthor(Config.DefaultAuthorName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="title">Max lenght: 256</param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetTitle(string title)
        {
            embedBuilder.WithTitle(Helper.MaxLenght(title, _max_title_lenght));
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="title">Max lenght: 256</param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetTitleInformation(string title)
        {
            SetTitle($":information_source: {title}");
            embedBuilder.Color = new Color(0, 186, 255);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="title">Max lenght: 256</param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetTitleWarning(string title)
        {
            SetTitle($":warning: {title}");
            embedBuilder.Color = new Color(255, 204, 0);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="title">Max lenght: 256</param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetTitleError(string title)
        {
            SetTitle($":x: {title}");
            embedBuilder.Color = new Color(255, 0, 0);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="title">Max lenght: 256</param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetTitleSuccess(string title)
        {
            SetTitle($":white_check_mark: {title}");
            embedBuilder.Color = new Color(85, 177, 85);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="description">Max lenght: 4096</param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetDescription(string description)
        {
            embedBuilder.WithDescription(Helper.MaxLenght(description, _max_description_lenght));
            return this;
        }

        public DiscordWebhookMessageBuilder SetUrl(string url)
        {
            embedBuilder.WithUrl(url);
            return this;
        }

        public DiscordWebhookMessageBuilder SetThumbnailUrl(string thumbnailUrl)
        {
            embedBuilder.WithThumbnailUrl(thumbnailUrl);
            return this;
        }

        public DiscordWebhookMessageBuilder SetImageUrl(string imageUrl)
        {
            embedBuilder.WithImageUrl(imageUrl);
            return this;
        }

        public DiscordWebhookMessageBuilder SetCurrentTimestamp()
        {
            embedBuilder.WithTimestamp(DateTimeOffset.UtcNow);
            return this;
        }

        public DiscordWebhookMessageBuilder SetTimestamp(DateTimeOffset dateTimeOffset)
        {
            embedBuilder.WithTimestamp(dateTimeOffset);
            return this;
        }

        public DiscordWebhookMessageBuilder SetColor(int red, int green, int blue)
        {
            embedBuilder.WithColor(red, green, blue);
            return this;
        }

        public DiscordWebhookMessageBuilder SetColor(Color color)
        {
            embedBuilder.WithColor(color);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">Max lenght: 256</param>
        /// <param name="iconUrl"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetAuthor(string name, string iconUrl = null, string url = null)
        {
            embedBuilder.WithAuthor(Helper.MaxLenght(name, _max_author_lenght), iconUrl, url);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text">Max lenght: 2048</param>
        /// <param name="iconUrl"></param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder SetFooter(string text, string iconUrl = null)
        {
            embedBuilder.WithFooter(Helper.MaxLenght(text, _max_footer_lenght), iconUrl);
            return this;
        }

        /// <summary>
        /// field with code formatting in message
        /// </summary>
        /// <param name="name">Max lenght: 256</param>
        /// <param name="value">Max lenght: 1024</param>
        /// <param name="inline"></param>
        /// <returns></returns>
        public DiscordWebhookMessageBuilder AddField(string name, object value, bool inline = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = "Campo não informado";
            if (value is null)
                value = "Valor não informado";

            embedBuilder.AddField(
                name: Helper.MaxLenght(name, _max_fieldname_lenght),
                value: Helper.MaxLenght($"```{value}```", _max_fieldvalue_lenght),
                inline: inline);
            return this;
        }

        public Embed Build()
        {
            return embedBuilder.Build();
        }
    }
}