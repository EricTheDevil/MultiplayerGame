using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataCollector : MonoBehaviour
{

    public float totalTime;
    public float savedTime;
    public float currentTime;
    public string startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            

        }
        if (Input.GetMouseButtonDown(2))
        {
            
        }

    }
}
