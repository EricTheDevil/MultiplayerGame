using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using System.Linq;

[BoltGlobalBehaviour]
public class NetworkCallback : GlobalEventListener
{
    private static List<BoltEntity> players = new List<BoltEntity>();
    public static int ready = 0;
    BoltEntity currentPlayer;

    public static List<BoltEntity> GetPlayers()
    {
        return players;
    }
 
    public override void OnEvent(PlayerMan evnt)
    {
        players.Add(evnt.Players);
        Debug.Log(players.Count);
    }


    public override void SceneLoadLocalDone(string map, IProtocolToken token)
    {
        if (!BoltNetwork.IsClient)
        {    
            currentPlayer = BoltNetwork.Instantiate(BoltPrefabs.NetworkPlayer_1, PlayerInstance.instance.transform.position, Quaternion.identity);
            var log = PlayerMan.Create();
            log.Players = currentPlayer;
            log.Send();    
        }
    }
    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
    {
        if (!BoltNetwork.IsServer)
        {
            currentPlayer = BoltNetwork.Instantiate(BoltPrefabs.NetworkPlayer_1, PlayerInstance.instance.transform.position, Quaternion.identity);
            var log = PlayerMan.Create();
            log.Players = currentPlayer;
            log.Send();
        }
    }
    public static List<bool> GetBool()
    {
        List<bool> playerCheck = new List<bool>();
        playerCheck.Clear();
        for (int i = 0; i < players.Count; i++)
        {
            playerCheck.Add(players[i].GetComponent<NetworkPlayer>().isFinished);
        }
        return playerCheck;
    }
    public static List<int> GetScore()
    {                                 
        List<int> scoreList = new List<int>();
        scoreList.Clear();
        for (int i = 0; i < players.Count; i++)
        {
            scoreList.Add(players[i].GetComponent<NetworkPlayer>().points);
        }

        return scoreList;
    }
}