using System.Text;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class JSONSave
{

    private class JSONSaveInitException : Exception
    {
        public JSONSaveInitException(string message) : base(message)
        { }
    }

    private const string INIT_EXCEPTION_MESSAGE = "Error, you must call JSONSave.Start(JSONSaveConfig config) before trying to get or set saved data.";

    private static JSONSaveConfig _config;

    public static bool ShowInitWarning = true;

    private static JSONSaveFileModel _latestData;
    private static StringBuilder _sb = new StringBuilder();
    
    const string String_Empty = "";

    #region Init

    public static void Start(JSONSaveConfig config)
    {
        _config = config;
        _latestData = null;
        _latestData = GetSaveFile();
    }

    private static void CheckForInit()
    {
        if (_config == null)
        {
            throw new JSONSaveInitException(INIT_EXCEPTION_MESSAGE);
        }
    }

    #endregion


    #region Public Get, Set and util

    public static void SetString(string key, string value = String_Empty)
    {
        AddDataToSaveFile(key, value);
    }

    public static string GetString(string key, string defaultValue = String_Empty)
    {
        return (string)GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetInt(string key, int value = default(int))
    {
        AddDataToSaveFile(key, value);
    }

    public static int GetInt(string key, int defaultValue = default(int))
    {
        return (int)GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetFloat(string key, float value = default(float))
    {
        AddDataToSaveFile(key, value);
    }

    public static float GetFloat(string key, float defaultValue = default(float))
    {
        return (float)GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetBool(string key, bool value = default(bool))
    {
        AddDataToSaveFile(key, value);
    }

    public static bool GetBool(string key, bool defaultValue = default(bool))
    {
        return (bool)GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetVector2(string key, Vector2 value)
    {
        AddDataToSaveFile(key, value);
    }

    public static Vector2 GetVector2(string key, Vector2 defaultValue = default(Vector2))
    {
        return (Vector2)GetDataFromSaveFile(key, defaultValue);
    }

    public static float GetVector2x(string key, float defaultValue = default(float))
    {
        return GetVector2(key, new Vector2(defaultValue, 0)).x;
    }

    public static float GetVector2y(string key, float defaultValue = default(float))
    {
        return GetVector2(key, new Vector2(0, defaultValue)).y;
    }

    public static void SetVector3(string key, Vector3 value)
    {
        AddDataToSaveFile(key, value);
    }

    public static Vector3 GetVector3(string key, Vector3 defaultValue = default(Vector3))
    {
        return (Vector3)GetDataFromSaveFile(key, defaultValue);
    }

    public static float GetVector3x(string key, float defaultValue = default(float))
    {
        return GetVector3(key, new Vector3(defaultValue, 0, 0)).x;
    }

    public static float GetVector3y(string key, float defaultValue = default(float))
    {
        return GetVector3(key, new Vector3(0, defaultValue, 0)).y;
    }

    public static float GetVector3z(string key, float defaultValue = default(float))
    {
        return GetVector3(key, new Vector3(0, 0, defaultValue)).z;
    }

    public static bool HasKey(string key)
    {
        return GetSaveFile().HasKey(key);
    }

    public static bool HasKeyForString(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, string.Empty);
    }

    public static bool HasKeyForInt(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(int));
    }

    public static bool HasKeyForFloat(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(float));
    }

    public static bool HasKeyForBool(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(bool));
    }

    public static void DeleteKey(string key)
    {
        GetSaveFile().DeleteKey(key);
        SaveSaveFile();
    }

    public static void DeleteString(string key)
    {
        GetSaveFile().DeleteString(key);
        SaveSaveFile();
    }

    public static void DeleteInt(string key)
    {
        GetSaveFile().DeleteInt(key);
        SaveSaveFile();
    }

    public static void DeleteFloat(string key)
    {
        GetSaveFile().DeleteFloat(key);
        SaveSaveFile();
    }

    public static void DeleteBool(string key)
    {
        GetSaveFile().DeleteBool(key);
        SaveSaveFile();
    }

    public static void DeleteVector2(string key)
    {
        GetSaveFile().DeleteVector2(key);
        SaveSaveFile();
    }

    public static void DeleteVector3(string key)
    {
        GetSaveFile().DeleteVector3(key);
        SaveSaveFile();
    }

    public static void DeleteAll()
    {
        WriteToSaveFile(JsonUtility.ToJson(new JSONSaveFileModel()));
        _latestData = new JSONSaveFileModel();
    }

    public static void OverwriteLocalSaveFile(string data)
    {
        WriteToSaveFile(data);
        _latestData = null;
        _latestData = GetSaveFile();
    }


    #endregion



    #region Read data

    private static JSONSaveFileModel GetSaveFile()
    {
        CheckForInit();
        CheckSaveFileExists();
        if (_latestData == null)
        {
            var saveFileText = File.ReadAllText(GetSaveFilePath());
            if (_config.ScrambleSaveData)
            {
                saveFileText = DataScrambler(saveFileText);
            }
            try
            {
                _latestData = JsonUtility.FromJson<JSONSaveFileModel>(saveFileText);
            }
            catch (ArgumentException e)
            {
                Debug.LogException(new Exception("JSONSave Error loading save file: " + e.Message));
                if (_config.OnLoadError != null)
                {
                    _config.OnLoadError.Invoke();
                }
                else
                {
                    DeleteAll();
                }
            }
        }
        return _latestData;
    }

    public static string GetSaveFilePath()
    {
        CheckForInit();
        return Path.Combine(_config.GetSaveFilePath(), _config.SaveFileName);
    }

  

    public static string GetSaveFileAsJson()
    {
        CheckForInit();
        CheckSaveFileExists();
        return JsonUtility.ToJson(GetSaveFile());
    }

    private static object GetDataFromSaveFile(string key, object defaultValue)
    {
        return GetSaveFile().GetValueForKey(key, defaultValue);
    }

    #endregion


    #region write data

    private static void AddDataToSaveFile(string key, object value)
    {
        CheckForInit();
        GetSaveFile().UpdateOrAddData(key, value);
        SaveSaveFile();
    }

    public static void Save()
    {
        CheckForInit();
        SaveSaveFile(true);
    }

    private static void SaveSaveFile(bool manualSave = false)
    {
        if (_config.AutoSaveData || manualSave)
        {
            WriteToSaveFile(JsonUtility.ToJson(GetSaveFile()));
        }
    }
    private static void WriteToSaveFile(string data)
    {
        var tw = new StreamWriter( GetSaveFilePath());
        if (_config.ScrambleSaveData)
        {
            data = DataScrambler(data);
        }
        tw.Write(data);
        tw.Close();
    }

    #endregion


    #region File Utils

    private static void CheckSaveFileExists()
    {
        if (!DoesSaveFileExist())
        {
            CreateNewSaveFile();
        }
    }

    private static bool DoesSaveFileExist()
    {
        return File.Exists(GetSaveFilePath());
    }

    private static void CreateNewSaveFile()
    {
        WriteToSaveFile(JsonUtility.ToJson(new JSONSaveFileModel()));
    }

    private static string DataScrambler(string data)
    {
        _sb.Clear();

        for (int i = 0; i < data.Length; i++)
        {
            _sb.Append((char)(data[i] ^ _config.EncryptionSecret[i % _config.EncryptionSecret.Length]));
        }
        return _sb.ToString();
    }

    #endregion
}

