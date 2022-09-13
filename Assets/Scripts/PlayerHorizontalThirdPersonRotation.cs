using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalThirdPersonRotation : MonoBehaviour
{
    [SerializeField] private float _cameraRotationIntensity = 50f;
    
    void Update()
    {
        Vector3 rotation = transform.eulerAngles;
        if (Input.GetAxis("Mouse X") != 0)
        {
            rotation.y += Input.GetAxis("Mouse X") * Time.deltaTime * _cameraRotationIntensity;
        }
        transform.eulerAngles = rotation;
    }
}
