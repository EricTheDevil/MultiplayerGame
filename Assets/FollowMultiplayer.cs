using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class FollowMultiplayer : EntityBehaviour<ICubeState>
{
    [SerializeField] GameObject localPlayer;

    public override void Attached()
    {
        if (entity.IsOwner)
        {
            localPlayer = RoundSystem.Instance.playerOne;

            state.SetTransforms(state.CubeTransform, localPlayer.transform);

            transform.position = state.CubeTransform.Transform.position;
        }
        else
        {
            
        }
    }

    public override void SimulateOwner()
    {
        transform.position = state.CubeTransform.Transform.position;

        if (entity.IsOwner)
        {
            state.CubeTransform.Transform.position = transform.position;
        }
        else
        {
            transform.position = state.CubeTransform.Transform.position;
        }
    }
}