using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollcet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        DataHolder.Points += 1;
        Destroy(gameObject);
    }
}
