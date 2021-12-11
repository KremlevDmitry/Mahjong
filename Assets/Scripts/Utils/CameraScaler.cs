using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    private Camera _camera = default;

    [SerializeField]
    private float _defaultSize = 10f;
    [SerializeField]
    private float _targetAspect = 9/16f;


    private void SetScaler()
    {
        if (_targetAspect > (float)Screen.width / Screen.height)
        {
            _camera.orthographicSize = _defaultSize * (_targetAspect / _camera.aspect);
        }
    }


    private void Awake()
    {
        _camera = GetComponent<Camera>();

        SetScaler();
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetScaler();
    }
#endif
}
