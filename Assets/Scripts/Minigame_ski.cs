using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame_ski : MonoBehaviour
{
    public GameObject player;
    public TMP_Text distance;
    int playerCount= 0;
    public Transform startPos;
    bool allFalse = false;
    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInstance.instance.isFinished = false;

    }

    // Update is called once per frame
    void Update()
    {       /*
               Debug.LogError(TutorialPlayerObjectRegistry.players[0].character.GetComponent<NetworkPlayer>().isReady) ;
        if (TutorialPlayerObjectRegistry.players.Count == 2) {
            Debug.LogError(TutorialPlayerObjectRegistry.players[1].character.GetComponent<NetworkPlayer>().isReady);
        }
        if (TutorialPlayerObjectRegistry.players[0].character);
        */

        if (PlayerInstance.instance.isFinished == true)
        {
            PlayerInstance.instance.trophies = timer;
            if(NetworkCallback.GetPlayers()[0].GetComponent<NetworkPlayer>().isReady)
            { 
                if (NetworkCallback.GetBool().TrueForAll(x => x) == true)
                {
                    //PlayerInstance.instance.isReady = false;
                    List<int> scoreList = NetworkCallback.GetScore().OrderBy(l => l).ToList<int>();        

                    if(scoreList[0] != 0)
                    {          
                        if(PlayerInstance.instance.trophies == scoreList[0])
                        {
                            RoundSystem.Instance.MoveForward(2);
                            PlayerInstance.instance.isReady = false;
                            SceneManager.UnloadSceneAsync(2);
                        }
                        else if(PlayerInstance.instance.trophies == scoreList[1])
                        {
                            RoundSystem.Instance.MoveForward(1);
                            PlayerInstance.instance.isReady = false;
                            SceneManager.UnloadSceneAsync(2);
                        }
                        else
                        {
                            PlayerInstance.instance.isReady = false;
                            SceneManager.UnloadSceneAsync(2);
                        }
                    }
                }
            }

        }
        else
        {
            timer++;
        }
        distance.text = timer.ToString();         
    }
}
