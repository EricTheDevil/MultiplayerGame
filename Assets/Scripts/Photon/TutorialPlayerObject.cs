using UnityEngine;
using System.Collections.Generic;

public class TutorialPlayerObject
{
    public BoltEntity character;
    public BoltConnection connection;

    public bool IsServer
    {
        get { return connection == null; }
    }

    public bool IsClient
    {
        get { return connection != null; }
    }
    public void GetCharacter(BoltEntity chara)
    {
        chara = character;
    }
    public void Spawn()
    {
        if (PlayerInstance.instance != null)
        {
            if (IsServer)
            {
                //character = BoltNetwork.Instantiate(BoltPrefabs.NetworkPlayer, RoundSystem.Instance.spawnRoom.position, Quaternion.identity);
                character.TakeControl();
            }
            else
            {
                //character = BoltNetwork.Instantiate(BoltPrefabs.NetworkPlayer_1, RoundSystem.Instance.spawnRoom.position, Quaternion.identity);
                character.AssignControl(connection);
            }
        }

        // teleport entity to a random spawn position
    }
}