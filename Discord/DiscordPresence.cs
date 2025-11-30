using UnityEngine;
using Discord;

public class DiscordPresence : MonoBehaviour
{
    public long clientId = 1444670811943342293;

    private Discord.Discord discord;
    private long startTimestamp;
    private bool discordEnabled = true; // si falla, se desactiva solo

    void Start()
    {
        try
        {
            discord = new Discord.Discord(clientId, (ulong)Discord.CreateFlags.Default);

            startTimestamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();

            // Espera 1 segundo para que Discord registre la app
            Invoke(nameof(UpdatePresence), 1f);
        }
        catch
        {
            Debug.LogWarning("Discord no está disponible. Rich Presence desactivado.");
            discordEnabled = false; // desactivar rich presence
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

    void UpdatePresence()
    {
        if (!discordEnabled) return;

        try
        {
            var activityManager = discord.GetActivityManager();

            var activity = new Activity
            {
                Details = "Jugando a The Shooter",
                State = "EDisfrutando del juego multijugador",
                Timestamps = { Start = startTimestamp },
                Assets =
                {
                    LargeImage = "logo_1024x1024", // nombre del asset SIN .png
                    LargeText = "The Shooter"
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
        {
            discord.Dispose();
        }
    }
}
