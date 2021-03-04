﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using System.Linq;

[BoltGlobalBehaviour]
public class NetworkCallback : GlobalEventListener
{
    private static List<BoltEntity> players = new List<BoltEntity>();
    BoltEntity currentPlayer;

    void Start()
    {
        currentPlayer = BoltNetwork.Instantiate(BoltPrefabs.NetworkPlayer, PlayerInstance.instance.transform.position, Quaternion.identity);
        players.Add(currentPlayer);
    }
    public static List<BoltEntity> GetPlayers()
    {
        return players;
    }
    /*
    public static void AddPlayer(BoltEntity player)
    {
        IPlayer a;
        a.
        //If the player is already contained in the list the event won't do anything
        //This is because the event will be fired for all active players when a new player is created
        if (players.Any(p => p.playerEntity == player) == true)
            return;

        if (player.TryFindState<IPlayer>(out IPlayer playerState))
        {
            players.Add(playerState);

            LogConnectedPlayers();
        }
    }
    */
}