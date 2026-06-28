using UnityEngine;
using Discord;
using System;

public class DiscordSingleton : MonoBehaviour
{
    public static DiscordSingleton Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //static void SpawnDiscordManager()
    //{
    //  var prefab = Resources.Load<GameObject>("DiscordManager");
    //  var obj = Instantiate(prefab);
    //  DontDestroyOnLoad(obj);
    //}

    Discord.Discord discord;
    const long appId = 1518267353832357918;

    void Start()
    {
        discord = new Discord.Discord(appId, (ulong)Discord.CreateFlags.NoRequireDiscord);
        Debug.Log("discord ready!");
        SetActivity();
    }

    void OnDisable()
    {
        discord.Dispose();
    }

    void Update()
    {
        discord.RunCallbacks();
    }

    void SetActivity(string status = null)
    {
        ActivityManager activityManager = discord.GetActivityManager();
        Activity activity = new()
        {
            Details = status,
            //State = "playing without gravity",
            Timestamps =
            {
                Start = (long)(ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            }
        };
        
        activityManager.UpdateActivity(activity, (res) => Debug.Log("discord activity updated!"));
    }
}