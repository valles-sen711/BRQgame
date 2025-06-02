using UnityEngine;

public static class AudioBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        if (Object.FindAnyObjectByType<AudioManager>() == null)
        {
            GameObject audioManagerPrefab = Resources.Load<GameObject>("AudioManager");
            if (audioManagerPrefab != null)
            {
                GameObject audioManagerInstance = Object.Instantiate(audioManagerPrefab);
                Object.DontDestroyOnLoad(audioManagerInstance);
            }
            else
            {
                Debug.LogError("AudioManager prefab not found in Resources folder!");
            }
        }
    }
}
