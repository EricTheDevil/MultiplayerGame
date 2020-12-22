using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;
    [SerializeField] public GameObject player;
    public static GameManager instance
    {
        get
        {
            if (gameManagerInstance == null)
            {
                gameManagerInstance = FindObjectOfType<GameManager>();
            }
            return gameManagerInstance;
        }
    }
}
