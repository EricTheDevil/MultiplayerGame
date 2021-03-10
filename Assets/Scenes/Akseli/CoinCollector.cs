using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public float coins = 0;
    public TMP_Text collected;

    // Update is called once per frame
    void Update()
    {

        collected.text = ("bones :" + coins.ToString());
    }

    public void addCoin()
    {
        coins += 1;
    }
}
