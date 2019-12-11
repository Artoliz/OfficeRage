using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float _stamina = 100;
    private int _life = 100;

    public Slider Stamina;
    public Slider Health;

    private void Awake()
    {
        Health.value = _life;
        Stamina.value = _stamina;
    }

    private void Update()
    {
        if (_stamina < 100)
        {
            _stamina += 0.1f;
            Stamina.value = _stamina;
        }
    }

    public void RemoveOnHP()
    {
        _life -= 1;
        Health.value = _life;
        if (_life <= 0)
        {
            // Player supposed to be dead...
        }
    }

    public void RemoveStamina(int minus)
    {
        _stamina -= minus;
        Stamina.value = _stamina;
    }

    public float GetStamina()
    {
        return _stamina;
    }
}
