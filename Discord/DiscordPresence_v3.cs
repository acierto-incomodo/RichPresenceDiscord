using UnityEngine;
using Discord;

public class DiscordPresence : MonoBehaviour
{
    [Header("Discord Settings")]
    public long clientId = 1234567890123456789;
    public bool enableRichPresence = true; // Activar/desactivar

    [Header("Large Image Settings")]
    public string largeImage = "photoLarge"; // Nombre del asset grande (sin .png)
    public string largeText = "textLarge";

    [Header("Small Image Settings")]
    public string smallImage = "photoSmall";
    public string smallText = "textSmall";

    [Header("Gameplay Info")]
    public string details = "detailsText";
    public string state = "stateText";

    private Discord.Discord discord;
    private long startTimestamp;
    private bool discordEnabled = true;

    // Para que solo haya una instancia
    private static DiscordPresence instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya hay una instancia, copiamos la configuración de esta escena
            instance.ApplyInspectorSettings(this);
            Destroy(gameObject); // destruimos el objeto duplicado
        }
    }

    void Start()
    {
        if (!enableRichPresence) return;

        try
        {
            discord = new Discord.Discord(clientId, (ulong)Discord.CreateFlags.Default);
            startTimestamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
            Invoke(nameof(UpdatePresence), 1f); // espera 1 segundo para registrar la app
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

    // Método para actualizar la configuración desde otra instancia
    private void ApplyInspectorSettings(DiscordPresence source)
    {
        clientId = source.clientId;
        enableRichPresence = source.enableRichPresence;
        largeImage = source.largeImage;
        largeText = source.largeText;
        smallImage = source.smallImage;
        smallText = source.smallText;
        details = source.details;
        state = source.state;

        // Actualizamos la actividad si ya se inicializó Discord
        if (discord != null)
            UpdatePresence();
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

    // Método público para cambiar estado dinámicamente desde código
    public void SetState(string newDetails, string newState)
    {
        details = newDetails;
        state = newState;
        UpdatePresence();
    }

    void OnApplicationQuit()
    {
        if (discordEnabled && discord != null)
            discord.Dispose();
    }
}
