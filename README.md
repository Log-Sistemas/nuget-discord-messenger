﻿# Discord messenger

[![Nuget](https://img.shields.io/nuget/v/LogSistemas.Nuget.Discord.Messenger?style=for-the-badge&logo=nuget&labelColor=%23004880&color=%23004880)](https://www.nuget.org/packages/LogSistemas.Nuget.Discord.Messenger/) 
![Nuget](https://img.shields.io/nuget/dt/LogSistemas.Nuget.Discord.Messenger?style=for-the-badge&logo=nuget&labelColor=%23004880&color=%23004880)

This is a C# wrapper for sending discord message with webhook.

I was build with dotnet 6.0.

### Some benefits

- Faster to use then [discord.net](https://github.com/discord-net/Discord.Net/tree/dev/samples/WebhookClient) (he is used under the hood)
- Pre-configured customizations to make message more elegant
- Exception handling when sending message with fallback webhook url

## Usage

### Configuration

Optional

```C#	
services.AddDiscordMessenger(
    exceptionFallbackWebhookUrl: "https://discord.com/api/webhooks/...",
    defaultAuthorName: "Backoffice API");
```

`exceptionFallbackWebhookUrl`: In case o fail any message, it will try to send a message to this webhook to notify this error

### Example

```C#	
await new DiscordWebhookBuilder()
    .AddMessageCard(builder =>
        builder
            .SetColor(74, 224, 233)
            .SetTitle(":bell: Notificação realizada com sucesso")
            .SetDescription("Esta notificação foi enviada através de uma API")
            .AddField("Linguagem", "C#")
            .AddField("Nuget", "Sim")
            .SetAuthor("API", "https://objectstorage.sa-saopaulo-1.oraclecloud.com/p/VdqjKFYi4Xw9nBkUShBkzAxmM9-bkeQzBuJNMtSSnrRyzxKOPUy_kyjZpwnBPq51/n/grzlql2zrphm/b/domynus-cdn/o/kerootica/images/logossimple.png", "https://testapp.kerootica.com.br")
            .SetFooter("Desenvolvido com café", "https://cdn.icon-icons.com/icons2/738/PNG/512/coffee_icon-icons.com_63177.png")
            .SetImageUrl("https://objectstorage.sa-saopaulo-1.oraclecloud.com/p/VdqjKFYi4Xw9nBkUShBkzAxmM9-bkeQzBuJNMtSSnrRyzxKOPUy_kyjZpwnBPq51/n/grzlql2zrphm/b/domynus-cdn/o/kerootica/images/wallpaper-web.jpg")
            .SetThumbnailUrl("https://cdn.pixabay.com/photo/2015/12/16/17/41/bell-1096280_1280.png")
            .SetImageUrl("https://objectstorage.sa-saopaulo-1.oraclecloud.com/p/VdqjKFYi4Xw9nBkUShBkzAxmM9-bkeQzBuJNMtSSnrRyzxKOPUy_kyjZpwnBPq51/n/grzlql2zrphm/b/domynus-cdn/o/kerootica/images/wallpaper-web.jpg"))
    .SendMessageAsync(webhookurl);
```

This message will generate the following discord message: 

![Notification sample](https://github.com/Log-Sistemas/nuget-discord-messenger/assets/32015491/c8c9f034-c998-4ef1-99c7-5f3d15692d7e)

### Title customization

You can use emoticons in any part of message.
There are few pre-configured titles with color and emoticon:

- SetTitleInformation
- SetTitleWarning
- SetTitleError
- SetTitleSuccess
