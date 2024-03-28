using System;
using UnityEngine;

public class JSONSaveConfig 
{
    private const string DEFAULT_SAVE_FILE_NAME = "data.json";
    private const string DEFAULT_ENCRYPTION_SECRET = "encryption-secret-default";

    public string SaveFileName = DEFAULT_SAVE_FILE_NAME;

    public bool AutoSaveData = true;

    public bool ScrambleSaveData = true;

    public string EncryptionSecret = DEFAULT_ENCRYPTION_SECRET;

    public string SaveFilePath = null;

    public Action OnLoadError;

    internal string GetSaveFilePath()
    {
        return string.IsNullOrEmpty(SaveFilePath) ? Application.persistentDataPath : SaveFilePath;
    }

    public static JSONSaveConfig GetConfig()
    {
        return new JSONSaveConfig
        {
            SaveFileName = "data.json",
            AutoSaveData = true,
            ScrambleSaveData = false,
            EncryptionSecret = "123",
        };
    }

}
