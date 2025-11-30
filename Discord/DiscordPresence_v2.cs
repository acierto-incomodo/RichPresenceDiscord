using UnityEngine;
using Discord;

public class DiscordPresence : MonoBehaviour
{
    [Header("Discord Settings")]
    public long clientId = 1234567890123456789; // Tu Client ID
    public bool enableRichPresence = true; // Para activar/desactivar todo

    [Header("Large Image Settings")]
    public string largeImage = "photoLarge"; // Nombre del asset grande (sin .png)
    public string largeText = "textLarge";     // Texto al pasar cursor

    [Header("Small Image Settings")]
    public string smallImage = "photoSmall";   // Nombre del asset pequeño (opcional)
    public string smallText = "textSmall";    // Texto al pasar cursor del pequeño

    [Header("Gameplay Info")]
    public string details = "detailsText"; // Texto de estado
    public string state = "stateText";           // Subtexto opcional
    private Discord.Discord discord;
    private long startTimestamp;
    private bool discordEnabled = true;

    void Start()
    {
        if (!enableRichPresence) return;

        try
        {
            discord = new Discord.Discord(clientId, (ulong)Discord.CreateFlags.Default);
            startTimestamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            Invoke(nameof(UpdatePresence), 1f); // Espera 1s para que Discord registre la app
        }
        catch
        {
            Debug.LogWarning("Discord no está disponible. Rich Presence desactivado.");
            discordEnabled = false;
        }
    }

    void Update()
    {
        if (!discordEnabled) return;

        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Debug.LogWarning("Discord dejó de responder. Rich Presence desactivado.");
            discordEnabled = false;
        }
    }

    public void UpdatePresence()
    {
        if (!discordEnabled) return;

        try
        {
            var activityManager = discord.GetActivityManager();

            var activity = new Activity
            {
                Details = details,
                State = state,
                Timestamps = { Start = startTimestamp },
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText,
                    SmallImage = smallImage,
                    SmallText = smallText
                }
            };

            activityManager.UpdateActivity(activity, result =>
            {
                if (result == Result.Ok)
                    Debug.Log("Rich Presence actualizado.");
            });
        }
        catch
        {
            Debug.LogWarning("No se pudo actualizar Rich Presence. Desactivado.");
            discordEnabled = false;
        }
    }

    void OnApplicationQuit()
    {
        if (discordEnabled && discord != null)
            discord.Dispose();
    }
}
