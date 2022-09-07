using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Weapon _currentWeapon;
    private AimDirectionService _aimDirectionService;

    private void Start() 
    {
        _aimDirectionService = new AimDirectionService(transform);
    }

    private void Update() 
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;
        Ray cameraToMouseRay = Camera.main.ScreenPointToRay(screenPosition);

        _aimDirectionService.UpdateDirections(cameraToMouseRay, _currentWeapon.GetMuzzlePosition(), _currentWeapon.Range);
        _currentWeapon.transform.rotation = Quaternion.LookRotation(_aimDirectionService.TargetPosition - transform.position);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _currentWeapon.Shoot(_aimDirectionService.TargetDirection);
        }
    }
}
