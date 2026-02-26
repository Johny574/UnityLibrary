using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Serializer  
{
    public static string AppRelativeSavePath { get; private set; }
    static Serializer() {
    #if UNITY_EDITOR
        AppRelativeSavePath = Path.Combine(Application.dataPath, "Save");
    #else
        AppRelativeSavePath = Path.Combine(Application.persistentDataPath, "Save");
    #endif
    }

    public static JsonSerializerSettings Settings { 
        get =>  new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };
    }

    public static void SaveFile<T>(T obj, string filename, SaveSlot slot) {
        FileWriter.CreateDirectory(AppRelativeSavePath);
        FileWriter.CreateFile(Path.Join(AppRelativeSavePath, filename));

        using (FileStream filestream = File.Open(Path.Join(AppRelativeSavePath, filename), FileMode.Truncate)) {
            using (StreamWriter writer = new StreamWriter(filestream)) {
                string serializedObj = JsonConvert.SerializeObject(obj, Settings);
                writer.Write(serializedObj);
                writer.Flush();
            }
        }
    }
      
    public static T LoadFile<T>(string file, SaveSlot slot) {
        FileWriter.CreateDirectory(AppRelativeSavePath);
        var saveFilePath = Path.Join(AppRelativeSavePath, file);
        FileWriter.CreateFile(saveFilePath);            
        FileStream fileStream = File.Open(saveFilePath, FileMode.Open);
        using (StreamReader reader = new StreamReader(fileStream)) {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }

    public static void DeleteSave(SaveSlot slot)
    {
        if (!Directory.Exists(Path.Join(AppRelativeSavePath, slot.ToString())))
        {
            return;
        }
        Directory.Delete(Path.Join(AppRelativeSavePath, slot.ToString()), true);
    }

    public enum SaveSlot
    {
        AutoSave,
        Slot1,
        Slot2,
    }
}

public class SerializeException : Exception
{
    public SerializeException(string message) : base(message) { }
}
