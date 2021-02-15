using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class SaveSystem : MonoBehaviour
{

    System.DateTime startTime;
    public System.TimeSpan saveTime;
    public Text texti;
    public string data;
    void Start()
    {

        startTime = System.DateTime.Now;

       
    }


    public void SaveData()
    {
        saveTime = System.DateTime.Now - startTime;
        string savethis = saveTime.ToString();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.json";
        FileStream steam = new FileStream(path, FileMode.Create);
        Debug.LogError(savethis + "savetime");
        formatter.Serialize(steam, savethis);
        steam.Close();


    }

    public string loaddata()
    {
        string path = Application.persistentDataPath + "/user.json";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream steam = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(steam) as string;
            
            steam.Close();

            return data;

        }
        else
        {
            Debug.LogError("save file not found" + path);
            return null;
        }


    }
 
    void OnApplicationQuit()
    {
        SaveData();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SaveData();
        }
        if (Input.GetMouseButtonDown(2))
        {
            loaddata();
            string thiss = data;
            print(data);
            texti.text = thiss;
        }

       
    }

}
