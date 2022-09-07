using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Weapon _currentWeapon;
    private AimDirectionService _aimDirectionService = new AimDirectionService();

    private void Update() 
    {
        Vector3 screenPosition = Input.mousePosition;
        _aimDirectionService.UpdateDirections(screenPosition, transform.position, _currentWeapon.Range);
        _currentWeapon.transform.rotation = Quaternion.LookRotation(_aimDirectionService.TargetDirection);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _currentWeapon.Shoot(_aimDirectionService.TargetDirection);
        }
    }
}
