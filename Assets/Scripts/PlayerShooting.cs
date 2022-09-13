using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Weapon _currentWeapon;
    
    private PlayerRoll _playerRoll;

    private void Start() 
    {
        _playerRoll = GetComponent<PlayerRoll>();
    }

    private void Update() 
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!_playerRoll.IsRolling)
            {
                _currentWeapon.Shoot(Vector3.zero);
            }
        }
    }
}
