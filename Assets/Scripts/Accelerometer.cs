using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float tiltX = Input.acceleration.x;
        float tiltZ = Input.acceleration.x;

        transform.Translate(tiltX * moveSpeed * Time.deltaTime, 0f, tiltZ * moveSpeed * Time.deltaTime);
    }
}
