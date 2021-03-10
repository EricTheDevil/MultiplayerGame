using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.SceneManagement;


public class SaveSystem : MonoBehaviour
{

    float startTime;
    public float saveTime;
    public Text texti;
    public float data;
    void Start()
    {

        

       
    }


    public void SaveData()
    {
        saveTime = startTime;
        loaddata();
        float savethis = saveTime + data;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.json";
        FileStream steam = new FileStream(path, FileMode.Create);
        Debug.LogError(savethis + "savetime");
        formatter.Serialize(steam, savethis);
        steam.Close();


    }

    public float loaddata()
    {
        string path = Application.persistentDataPath + "/user.json";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream steam = new FileStream(path, FileMode.Open);

            string gottendata = formatter.Deserialize(steam) as string;

            steam.Close();

            if(gottendata != null)
            {
                data = float.Parse(gottendata, CultureInfo.InvariantCulture);
            }
            else
            {
                data = 0;
            }

            print("gottendata" + gottendata);

            return data;

        }
        else
        {
            Debug.LogError("save file not found" + path);
            return 0;
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
            //SaveData();
        }
        if (Input.GetMouseButtonDown(2))
        {
          //  loaddata();
            
           // print(data);
            //texti.text = startTime.ToString();
        }
       // startTime = Time.time;

    }

}
