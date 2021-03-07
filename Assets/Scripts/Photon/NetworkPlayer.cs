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
        isReady = state.isReady;
        isFinished = state.isFinished;
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
        state.AddCallback("isFinished", IsFinishedChanged);

        state.AddCallback("isReady", IsReadyChanged);
        state.AddCallback("Trophies", IsPointsChanged);


    }
    void IsReadyChanged()
    {
        isReady = state.isReady;
    }
    void IsFinishedChanged()
    {
        isFinished = state.isFinished;
    }
    void IsPointsChanged()
    {
        points = (int)state.Trophies;
    }
    // Update is called once per frame
    public override void SimulateOwner()
    {
        isReady = state.isReady;
        if (!entity.IsOwner)
        {
            transform.position = state.Transform.Transform.position;
            isFinished = state.isFinished;
            isReady = state.isReady;
            points = (int)state.Trophies;
        }
        else
        {
            state.isReady = PlayerInstance.instance.GetComponent<Unit>().isReady;
            points = PlayerInstance.instance.GetComponent<Unit>().unitScore;
            isReady = state.isReady;

            state.isFinished = PlayerInstance.instance.GetComponent<Unit>().isFinished;
            isFinished = state.isFinished;

            state.Trophies = points;
            state.Transform.Transform.position = PlayerInstance.instance.transform.position;
        }
    }
}
