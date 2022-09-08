using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private bool _isRolling = false;
    [SerializeField] private float _rollIntensity = 1000f;
    [SerializeField] private float _rollDuration = 1f;
    [SerializeField] private float _maxSpeedWhileRolling = 10f; 

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private PlayerMovement _playerMovement;

    public bool IsRolling => _isRolling;
    private Vector3 _rollVelocityVector;
    private float _originalColliderRadius;

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _playerMovement = GetComponent<PlayerMovement>();
        _originalColliderRadius = _collider.radius;
    }

    private void Update() 
    {
        GenerateRoll();
    }

    private void GenerateRoll()
    {
        if (!_isRolling && Input.GetKeyDown(KeyCode.LeftShift) && !_playerMovement.InAir)
        {
            StartCoroutine(RollCoroutine());
        }
        if (_isRolling)
        {
            if (Vector3.Magnitude(_rigidbody.velocity) > _maxSpeedWhileRolling) 
            {
                _rigidbody.velocity = Vector3.Normalize(_rigidbody.velocity) * _maxSpeedWhileRolling;
                return;
            }
            _rigidbody.AddRelativeForce(_rollVelocityVector);
        }
    }

    private IEnumerator RollCoroutine()
    {
        _isRolling = true;
        _collider.radius = 0;
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
        rollDirection = Vector3.Normalize(rollDirection);
        _rollVelocityVector = rollDirection * _rollIntensity * Time.deltaTime;
        yield return new WaitForSeconds(_rollDuration);
        _isRolling = false;
        _collider.radius = _originalColliderRadius;
    }
}
