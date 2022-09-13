using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVerticalThirdPersonRotation : MonoBehaviour
{
    [SerializeField] private float _cameraRotationIntensity = 50f;
    
    private void Update()
    {
        Vector3 rotation = transform.eulerAngles;
        float yMouseMovement = Input.GetAxis("Mouse Y");
        if (yMouseMovement != 0) 
        {
            rotation.x -= Input.GetAxis("Mouse Y") * Time.deltaTime * _cameraRotationIntensity;
        }
        if (HasReachedRotationLimit(yMouseMovement)) { return; }
        transform.eulerAngles = rotation;
    }

    private bool HasReachedRotationLimit(float yMouseMovement)
    {
        return ((transform.localEulerAngles.x > 180) ? (transform.localEulerAngles.x- 360) : (transform.localEulerAngles.x)) > 45 && yMouseMovement < 0 || 
            ((transform.localEulerAngles.x > 180) ? (transform.localEulerAngles.x- 360) : (transform.localEulerAngles.x)) < -45 && yMouseMovement > 0;
    }
}
