using Bolt;
using UnityEngine;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject cubePrefab;

    public override void SceneLoadLocalDone(string scene)
    {
        Vector3 spawnPos = new Vector3(0, 0.5f, 25);
        BoltNetwork.Instantiate(cubePrefab, spawnPos, Quaternion.identity);
    }
}
