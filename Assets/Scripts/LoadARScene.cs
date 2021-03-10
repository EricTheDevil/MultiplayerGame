using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadARScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadAR()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
    public void Unload()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
