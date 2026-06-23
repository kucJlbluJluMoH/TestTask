using System;
using System.IO;
using UnityEngine;


public static class SaveSystem
{
    private static string RootPath =>
    #if UNITY_EDITOR
            Directory.GetCurrentDirectory();
    #else
            Application.persistentDataPath;
    #endif

    private static string GetPath(string key)
    {
        return Path.Combine(RootPath, "saves", $"{key}.json");
    }

    public static bool Save<T>(string key, T data)
    {
        if (string.IsNullOrEmpty(key))
        {
            Debug.LogError("[SaveSystem] Key cannot be null or empty.");
            return false;
        }

        try
        {
            string path = GetPath(key);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);

            Debug.Log($"[SaveSystem] Saved '{key}' to {path}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] Failed to save '{key}': {e.Message}");
            return false;
        }
    }

    public static T Load<T>(string key, T defaultValue = default)
    {
        if (string.IsNullOrEmpty(key))
        {
            Debug.LogError("[SaveSystem] Key cannot be null or empty.");
            return defaultValue;
        }

        string path = GetPath(key);

        if (!File.Exists(path))
        {
            Debug.LogWarning($"[SaveSystem] No save found for '{key}', returning default.");
            return defaultValue;
        }

        try
        {
            string json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogWarning($"[SaveSystem] Save file for '{key}' is empty, returning default.");
                return defaultValue;
            }

            T result = JsonUtility.FromJson<T>(json);
            Debug.Log($"[SaveSystem] Loaded '{key}' from {path}");
            return result;
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] Failed to load '{key}': {e.Message}");
            return defaultValue;
        }
    }

    public static bool Exists(string key)
    {
        return !string.IsNullOrEmpty(key) && File.Exists(GetPath(key));
    }

    public static bool Delete(string key)
    {
        string path = GetPath(key);

        if (!File.Exists(path))
        {
            Debug.LogWarning($"[SaveSystem] Nothing to delete for '{key}'.");
            return false;
        }

        try
        {
            File.Delete(path);
            Debug.Log($"[SaveSystem] Deleted save '{key}'.");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] Failed to delete '{key}': {e.Message}");
            return false;
        }
    }
}
