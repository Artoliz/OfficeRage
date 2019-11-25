using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnClick : MonoBehaviour
{
    public float Force;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Force));
        }
    }
}
