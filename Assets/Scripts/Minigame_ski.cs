using Bolt;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame_ski : EntityBehaviour<IMinigame>
{
    public GameObject player;
    public TMP_Text distance;
    public Collider finish;
    public Transform startPos;
    int timer = 0;
    List<int> timeValues = new List<int>();
    List<int> sortedTime = new List<int>();
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (NetworkCallback.GetPlayers().Count == 1)
        {
            Debug.LogError(NetworkCallback.GetPlayers()[1].GetComponent<NetworkPlayer>().isFinished);
        }

        if (PlayerInstance.instance.GetComponent<Unit>().isReady == true)
        {
            PlayerInstance.instance.GetComponent<Unit>().unitScore = timer;
            if (NetworkCallback.GetPlayers()[0].GetComponent<NetworkPlayer>().isFinished == true && NetworkCallback.GetPlayers()[1].GetComponent<NetworkPlayer>().isFinished == true)
            {
                Debug.LogError("WOW");
            }
            for (int i = 0; i < NetworkCallback.GetPlayers().Count; i++)
            {
                Debug.Log(NetworkCallback.GetPlayers().Where(c => !c.GetComponent<NetworkPlayer>().isReady).Count() == NetworkCallback.GetPlayers().Count);
                if(NetworkCallback.GetPlayers().Where(c => !c.GetComponent<NetworkPlayer>().isReady).Count() == NetworkCallback.GetPlayers().Count)
                {
                    timeValues.Add(NetworkCallback.GetPlayers()[i].GetComponent<NetworkPlayer>().points);
                    timeValues.Sort();  
                }
            }
        }
        else
        {
            timer++;
        }  
        distance.text = "Time :"+ timer.ToString();
    }
}
