using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Weapon _currentWeapon;
    [SerializeField] Transform _lookTarget;
    
    private PlayerRoll _playerRoll;

    private void Start() 
    {
        _playerRoll = GetComponent<PlayerRoll>();
    }

    private void Update() 
    {
        if (Input.GetKey(KeyCode.Mouse0) && !_playerRoll.IsRolling)
        {
            _currentWeapon.Shoot(GetShotDirection());
        }
    }

    private Vector3 GetShotDirection()
    {
        Vector3 shotDirection = Vector3.zero;
        Ray cameraToMouseRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, transform.position.z));
        if (Physics.Raycast(cameraToMouseRay, out RaycastHit hitData, _currentWeapon.Range))
        {
            shotDirection = hitData.point - _currentWeapon.GetMuzzlePosition();
        }
        else  // if it does not find a target, target position is calculated based on weapon range and camera direction
        {
            Vector3 targetPosition = cameraToMouseRay.direction * _currentWeapon.Range + Camera.main.transform.position;
            shotDirection = targetPosition - transform.position;
        }
        return shotDirection;
    }
}
