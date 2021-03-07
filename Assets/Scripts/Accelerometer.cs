using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    /*public bool isFlat = true;
    private Rigidbody rigid;*/


    // Start is called before the first frame update
    void Start()
    {
        //rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);

        /*Vector3 tilt = Input.acceleration;
        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;

        rigid.AddForce(tilt);*/
    }
}
