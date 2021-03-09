using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : EntityBehaviour<IPlayer>
{
    [Header("Stats")]
    public int points = 0;
    private GameObject localVRPlayer;
    public bool isReady;
    public bool isFinished;
    public bool isVictory = false;
    // Start is called before the first frame update
    public int GetPoints()
    {
        return points;
    }
    public void AddPoints(int num)
    {
        points += num;
    }
    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
        
        if (entity.IsOwner == true)
        {
            /*
            if (localVRPlayer != null)
                playerController = localVRPlayer.GetComponent<PlayerController>();

            //localVRPlayer = CreateLocalVRPlayer();
            /*
            networkPlayerParts.networkPlayer = this;
            networkPlayerParts.networkPlayerHead = CreateNetworkPlayerHead(localVRPlayer.hmdTransforms[0]);
            networkPlayerParts.networkPlayerRightHand = CreateNetworkPlayerHand(localVRPlayer.rightHand.transform, BoltPrefabs.NetworkPlayerHandRight_Variant_);
            networkPlayerParts.networkPlayerLeftHand = CreateNetworkPlayerHand(localVRPlayer.leftHand.transform, BoltPrefabs.NetworkPlayerHandLeft_Original_);

            networkPlayerParts.networkPlayerRightHand.InitializeNetworkHand(localVRPlayer.rightHand);
            networkPlayerParts.networkPlayerLeftHand.InitializeNetworkHand(localVRPlayer.leftHand);

            if (BoltNetwork.IsServer == true)
                state.IsTeacher = true;

            state.PlayerName = PlayerManager.Instance.PlayerName;

            playerStatusCanvas.gameObject.SetActive(false);
            */
        }
        state.AddCallback("isReady", CheckIsReady);
        state.AddCallback("isFinished", CheckIsFinished);
        state.AddCallback("isVictory", CheckIsVictory);
        state.AddCallback("Trophies", CheckIsPoints);
    }
    void CheckIsVictory() //ALL
    {
        isVictory = state.isVictory;
    }
    void CheckIsReady() //ALL
    {
        isReady = state.isReady;
    }
    void CheckIsPoints() //ALL
    {
        points = (int)state.Trophies;
    }

    void CheckIsFinished() //ALL
    {
        isFinished = state.isFinished;
    }
    // Update is called once per frame
    public override void SimulateOwner()
    {
        if (!entity.IsOwner)
        {
            transform.position = state.Transform.Transform.position;
            isReady = state.isReady;
            points = (int)state.Trophies;

            isVictory = state.isVictory;
            isFinished = state.isFinished;
        }
        else
        {
            state.isReady = PlayerInstance.instance.isReady;
            isReady = state.isReady;

            state.isFinished = PlayerInstance.instance.isFinished;
            isFinished = state.isFinished;

            state.isVictory = PlayerInstance.instance.isVictory;
            isVictory = state.isVictory;

            state.Trophies = PlayerInstance.instance.trophies;
            points = (int)state.Trophies;
            state.Transform.Transform.position = PlayerInstance.instance.transform.position;
        }
    }
}
