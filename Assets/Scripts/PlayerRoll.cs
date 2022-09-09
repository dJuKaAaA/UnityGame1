using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private float _rollIntensity = 1000f;
    [SerializeField] private float _rollDuration = 1f;
    [SerializeField] private float _maxSpeedWhileRolling = 10f;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private PlayerMovement _playerMovement;

    private bool _isRolling = false;
    public bool IsRolling => _isRolling;
    private Vector3 _rollVelocityVector;
    private float _originalColliderRadius; 

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() 
    {
        HandleRoll();
    }

    private void HandleRoll()
    {
        if (!_isRolling && Input.GetKeyDown(KeyCode.LeftControl) && !_playerMovement.InAir)
        {
            Vector3 rollDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                rollDirection.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rollDirection.x = 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                rollDirection.z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rollDirection.z = -1;
            }
            if (rollDirection != Vector3.zero)
            {
                StartCoroutine(RollCoroutine(rollDirection));
            }
        }
        if (_isRolling)
        {
            if (Vector3.Magnitude(_rigidbody.velocity) > _maxSpeedWhileRolling) 
            {
                _rigidbody.velocity = Vector3.Normalize(_rigidbody.velocity) * _maxSpeedWhileRolling;
            }
            else 
            {
                _rigidbody.AddRelativeForce(_rollVelocityVector, ForceMode.VelocityChange);
            }
        }
    }

    private IEnumerator RollCoroutine(Vector3 rollDirection)
    {
        _isRolling = true;
        rollDirection = Vector3.Normalize(rollDirection);
        _rollVelocityVector = rollDirection * _rollIntensity * Time.deltaTime;
        yield return new WaitForSeconds(_rollDuration);
        _isRolling = false;
    }
}
