
# DiscordPresence for Unity

## English

**DiscordPresence** is a Unity C# script that integrates the Discord Game SDK to show Rich Presence for your game.

### Features

-   Show game status (Details & State) on Discord.
    
-   Display large and small images with hover text.
    
-   Automatically tracks playtime.
    
-   Works across multiple scenes with different configurations.
    
-   Safe: if Discord is not running, it won’t crash the game.
    
-   Fully configurable from the Unity Inspector.
    

### How it works

1.  Add the `DiscordPresence` script to a GameObject in your scene (e.g., `DiscordManager`).
    
2.  Configure your **Client ID**, images, and text from the Inspector.
    
3.  The first instance is kept alive between scenes (`DontDestroyOnLoad`).
    
4.  If you add another DiscordPresence in a new scene, it will **update the existing instance** with the new Inspector settings.
    
5.  Optionally, you can call `SetState("Details", "State")` from any script to update Rich Presence dynamically.
    

### Inspector Settings

|Field|Description|
|--|--|
|  Client ID|Your Discord application Client ID  |
|Enable Rich Presence|Enable or disable Discord Rich Presence|
|Large Image|Name of the large image asset (without extension)|
|Large Text|Text when hovering over the large image|
|Small Image|Name of the small image asset (optional and without extension)|
|Small Text|Text when hovering over the small image (optional)|
|Details|Main text shown in Discord|
|State|Subtext shown in Discord|

Official documentation of DiscordGame SDK by Discord: https://discord.com/developers/docs/developer-tools/game-sdk
Used SDK: [Discord Game SDK v3.2.1](https://dl-game-sdk.discordapp.net/3.2.1/discord_game_sdk.zip)


----------

## Español

**DiscordPresence** es un script de Unity C# que integra el Discord Game SDK para mostrar Rich Presence en Discord.

### Funcionalidades

-   Muestra el estado del juego (Details & State) en Discord.
    
-   Permite mostrar imágenes grandes y pequeñas con texto al pasar el cursor.
    
-   Controla automáticamente el tiempo de juego.
    
-   Funciona en varias escenas con diferentes configuraciones.
    
-   Seguro: si Discord no está abierto, no cierra ni rompe el juego.
    
-   Totalmente configurable desde el Inspector de Unity.
    

### Cómo funciona

1.  Añade el script `DiscordPresence` a un GameObject en tu escena (por ejemplo `DiscordManager`).
    
2.  Configura tu **Client ID**, imágenes y textos desde el Inspector.
    
3.  La primera instancia se mantiene entre escenas (`DontDestroyOnLoad`).
    
4.  Si añades otro DiscordPresence en otra escena, **actualiza la instancia existente** con la nueva configuración del Inspector.
    
5.  Opcionalmente, puedes llamar a `SetState("Detalles", "Estado")` desde cualquier script para actualizar el Rich Presence dinámicamente.
    

### Configuración en el Inspector
|Field|Description|
|--|--|
|  Client ID|Client ID de tu aplicación de Discord  |
|Enable Rich Presence|Activar o desactivar Rich Presence|
|Large Image|Nombre del asset de la imagen grande (sin extensión)|
|Large Text|Texto al pasar el cursor sobre la imagen grande|
|Small Image|Nombre del asset de imagen pequeña (opcional y sin extensión)|
|Small Text|Texto al pasar el cursor sobre la imagen pequeña (opcional)|
|Details|Texto principal que se muestra en Discord|
|State|Subtexto que se muestra en Discord|

---

Documentación oficial de DiscordGame SDK por Discord: https://discord.com/developers/docs/developer-tools/game-sdk
SDK utilizado: [Discord Game SDK v3.2.1](https://dl-game-sdk.discordapp.net/3.2.1/discord_game_sdk.zip)

---
By StormGamesStudios