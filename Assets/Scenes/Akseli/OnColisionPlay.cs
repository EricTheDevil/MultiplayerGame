using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColisionPlay : MonoBehaviour
{

    public AudioSource hitsound;
    // Start is called before the first frame update
    void Start()
    {
        hitsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        hitsound.Play();
    }
}
