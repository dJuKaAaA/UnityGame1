using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shotSparkParticle;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _shotVelocity = 10f;
    private bool _fired = false;
    Vector3 _firedFromPosition;
    Vector3 _targetPosition;
    Vector3 _shotDirection;
    float _maxDistance;  // will be in weapon class when its created

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = Camera.main.nearClipPlane + 1;
            Ray cameraToMouseRay = Camera.main.ScreenPointToRay(screenPosition);

            Vector3 cameraToPlayerVector = transform.position - Camera.main.transform.position;

            _targetPosition = cameraToMouseRay.direction * _range + Camera.main.transform.position;
            _shotDirection = Vector3.Normalize(cameraToMouseRay.direction * _range - cameraToPlayerVector);
            _firedFromPosition = transform.position;
            _maxDistance = Vector3.Magnitude(_targetPosition - transform.position);

            _shotSparkParticle.Play();
            _shotSparkParticle.transform.position = transform.position;
            _firedFromPosition = transform.position;
            _fired = true;
        }

        if (_fired) { MoveSpark(); }
    }

    private void MoveSpark()
    {
        _shotSparkParticle.transform.position += _shotDirection * _shotVelocity * Time.deltaTime;
        float distanceFromTarget = Vector3.Magnitude(_shotSparkParticle.transform.position - _firedFromPosition);
        if (distanceFromTarget >= _maxDistance) 
        {
            _shotSparkParticle.Stop();
            _fired = false;
        }
    }
}
