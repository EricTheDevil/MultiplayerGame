using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minigame_ski : MonoBehaviour
{
    public GameObject player;
    public TMP_Text distance;

    public Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        distance.text = (Mathf.Abs(player.transform.position.x - startPos.position.x)).ToString();         
    }
}
