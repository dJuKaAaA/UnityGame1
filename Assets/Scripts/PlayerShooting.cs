using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Weapon _currentWeapon;
    
    private AimDirectionService _aimDirectionService;
    private PlayerRoll _playerRoll;

    private void Start() 
    {
        _aimDirectionService = new AimDirectionService(transform);
        _playerRoll = GetComponent<PlayerRoll>();
    }

    private void Update() 
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;
        Ray cameraToMouseRay = Camera.main.ScreenPointToRay(screenPosition);

        _aimDirectionService.UpdateDirections(cameraToMouseRay, _currentWeapon.GetMuzzlePosition(), _currentWeapon.Range);
        
        HandleAimBodyRotation();
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!_playerRoll.IsRolling)
            {
                _currentWeapon.Shoot(_aimDirectionService.TargetDirection);
            }
        }
    }

    private void HandleAimBodyRotation()
    {
        // temporary solution
        // end product should have limited space where the aim cursor can go
        // mouse is free to move within that space but not beyond
        if (_aimDirectionService.TargetPosition.z > transform.position.z + 1)
        {
            _currentWeapon.transform.localRotation = Quaternion.LookRotation(_aimDirectionService.TargetPosition - transform.position);
            RotateUpperBody(_aimDirectionService.TargetPosition - transform.position);
        }
    }
    
    private void RotateUpperBody(Vector3 direction)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Player Body")
            {
                foreach (Transform bodyPart in child.transform)
                {
                    if (bodyPart.tag == "Player Upper Body")
                    {
                        bodyPart.localRotation = Quaternion.LookRotation(direction);
                        bodyPart.localEulerAngles = new Vector3(
                            bodyPart.localEulerAngles.z, bodyPart.localEulerAngles.y, -bodyPart.localEulerAngles.x
                        );

                        float yAngleInRadians = Mathf.Deg2Rad * bodyPart.localEulerAngles.y;
                        if (Mathf.Cos(yAngleInRadians) < Mathf.PI / 4 && Mathf.Cos(yAngleInRadians) > 0)
                        {
                            // figure out how to dynamically determine this number
                            float correctionValue = (Mathf.Sin(yAngleInRadians) > 0) ? -(40 - 1.685f) : (40 - 1.685f);  
                            RotateLowerBody(bodyPart.localEulerAngles.y + correctionValue);
                        }
                    }
                }
            }
        }
    }

    private void RotateLowerBody(float rotationAngle)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Player Body")
            {
                foreach (Transform bodyPart in child.transform)
                {
                    if (bodyPart.tag == "Player Lower Body")
                    {
                        bodyPart.localEulerAngles = new Vector3(
                            bodyPart.localEulerAngles.x, rotationAngle, bodyPart.localEulerAngles.z
                        );
                    }
                }
            }
        }
    }
}
