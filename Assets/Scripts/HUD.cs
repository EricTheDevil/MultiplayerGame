using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text nameText;
    public Text roundText;
        
    // Start is called before the first frame update
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetRound(int round)
    {
        roundText.text = roundText.ToString();
    }
}
