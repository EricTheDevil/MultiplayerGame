﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hitpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        PlayerInstance.instance.isFinished = true;

        //SceneManager.UnloadSceneAsync(2);  
    }
   
}
