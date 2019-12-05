﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponentInParent<People>().IsDead())
            return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Punched!");
            // Remove 1 PV to Player
        }
    }
}