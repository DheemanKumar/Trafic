using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class score : MonoBehaviour
{
    // Static variable to store epoch
    public static long epoch;

    // File path to save the game data
    private static string filePath;

    private void Awake()
    {
        filePath = "gameData.dat";
        LoadGameData();
    }

    // Function to save the game data to a file
    public static void SaveGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(filePath);

        GameData data = new GameData();
        data.epoch = epoch;

        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    // Function to load the game data from a file
    public static void LoadGameData()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(fileStream);
            fileStream.Close();

            epoch = data.epoch;
        }
        else
        {
            Debug.LogWarning("No game data found.");
        }
    }

    // Function to reset the game data
    public static void ResetGameData()
    {
        epoch = 0;
        SaveGameData();
    }
}

// Serializable class to store game data
[System.Serializable]
public class GameData
{
    public long epoch;
}
