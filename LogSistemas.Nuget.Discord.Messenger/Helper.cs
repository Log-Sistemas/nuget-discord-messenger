namespace LogSistemas.Nuget.Discord.Messenger
{
    public static class Helper
    {
        public static string FormatMessage(string message, int maxLenght)
        {
            if (message is null)
                return null;

            message = MaxLenght(message, maxLenght);

            if (!string.IsNullOrWhiteSpace(message))
                message = $"```{message}```";

            return message;
        }

        public static string MaxLenght(string message, int maxLenght)
        {
            if (message is null)
                return null;

            if (message.Length > maxLenght)
                message = $"{message.Substring(0, maxLenght)} ...";

            return message;
        }
    }
}