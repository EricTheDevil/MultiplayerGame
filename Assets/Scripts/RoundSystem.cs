using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundState { START, WAITING, WON, LOST, PLAYERONE, PLAYERTWO }
public class RoundSystem : MonoBehaviour
{
    public RoundState state;
    // Start is called before the first frame update
    void Start()
    {
        state = RoundState.WAITING;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
