using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public static Dictionary<string, string> LoadUsers(string fileName, string path)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");
        if (File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
        }
        else
        {
            Debug.Log("File not found: " + filePath);
        }
        return data;
    }
    public static void SaveUsers(string fileName, Dictionary<string, string> data)
    {
        string jsonText = JsonConvert.SerializeObject(data, Formatting.Indented);
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");
        File.WriteAllText(filePath, jsonText);
    }
}
