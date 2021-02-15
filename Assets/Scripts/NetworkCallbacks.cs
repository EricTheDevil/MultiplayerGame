using Bolt;
using UnityEngine;

public class NetworkCallbacks : GlobalEventListener
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] GameObject spawnPos;

    public override void SceneLoadLocalDone(string scene)
    {
        
        BoltNetwork.Instantiate(cubePrefab, spawnPos.transform.position, Quaternion.identity);
    }
}
