using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.AddUSBKey();
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0));
    }
}
