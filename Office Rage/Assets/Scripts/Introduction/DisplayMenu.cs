﻿using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    #region PrivateVariables

    private Camera _camera;
    private bool _isCameraNotNull;

    private MeshCollider _meshCollider;

    #endregion

    #region PublicVariables


    public bool isMenuDisplayed;
    
    public static DisplayMenu Instance;
    
    public SpriteRenderer menu;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        Instance = this;
        
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;

        _meshCollider = gameObject.transform.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (_isCameraNotNull && PowerComputer.Instance.computerIsOn)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
            {
                if (hit.collider == _meshCollider)
                {
                    if (!isMenuDisplayed)
                    {
                        isMenuDisplayed = true;
                        menu.enabled = true;
                    }
                    else
                    {
                        isMenuDisplayed = false;
                        menu.enabled = false;
                    }
                }
            }
        }
        else
        {
            isMenuDisplayed = false;
            menu.enabled = false;
        }
    }

    #endregion
}