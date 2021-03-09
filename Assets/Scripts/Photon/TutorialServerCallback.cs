using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialServerCallback : EntityBehaviour<IPlayerMananger>
{
    /*
    private static List<BoltEntity> players = new List<BoltEntity>();
    static int count = 0;
    public static TutorialServerCallback Instance { get; set; }
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
    public static void AddPlayer(BoltEntity aye)
    {
        players.Add(aye);

     
        count++;
    }
    public override void Attached()
    {
        state.AddCallback("Player[]", GetPlayers);
    }
    public void GetPlayers(Bolt.IState state, string path, Bolt.ArrayIndices indices)
    {
        IPlayerMananger actorState = (IPlayerMananger)state;
        
        for (int i = 0; i < players.Count; i++)
        {
            players[i]= actorState.Player[i]  ;

            Debug.LogError(actorState.Player[i]);
        }
    }
    public override void SimulateOwner()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if(state.Player[i] == null)
            {
                state.Player[i] = players[i];
            }
        }
    }
    */
}
