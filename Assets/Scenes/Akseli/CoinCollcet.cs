using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollcet : MonoBehaviour
{
    public GameObject collector;
  

    public void Start()
    {
     
    }

    void OnTriggerEnter(Collider other)
    {
        DataHolder.Points += 1;
      
        //collector.GetComponent<CoinCollector>().addCoin();
        Destroy(gameObject);
    }
  
}
