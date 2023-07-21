namespace LogSistemas.Nuget.Discord.Messenger
{
    public class DiscordException : Exception
    {
        public DiscordException(Exception inner, object content)
        {
            Inner = inner;
            Content = content;
        }

        public Exception Inner { get; }
        public object Content { get; }
    }
}