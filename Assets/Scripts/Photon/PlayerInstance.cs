using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    private static PlayerInstance _instance;
    public static PlayerInstance instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerInstance>();
            }
            return _instance;
        }
    }

    public Vector3 SpawnPosition()
    {
        return transform.position;
    }
}
