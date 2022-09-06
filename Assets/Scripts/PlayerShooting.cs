using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shotSparkParticle;
    [SerializeField] private float _range = 100f;
    private bool _fired = false;
    Vector3 _firedFromPosition;
    Vector3 _hitPosition;

    private void Update() 
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;

        Ray cameraToMouseRay = Camera.main.ScreenPointToRay(screenPosition);
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(cameraToMouseRay, out RaycastHit hitData, _range)) 
        {
            // _shotSparkParticle.transform.position = hitData.point;
            _shotSparkParticle.Play();
            _shotSparkParticle.transform.position = transform.position;
            _firedFromPosition = transform.position;
            _hitPosition = hitData.point;
            _fired = true;
        }


        if (_fired) { MoveSpark(); }
    }

    private void MoveSpark()
    {
        _shotSparkParticle.transform.position += Vector3.Normalize(_hitPosition - _firedFromPosition) * 10 * Time.deltaTime;
        if (Vector3.Magnitude(_shotSparkParticle.transform.position - _firedFromPosition) >= _range) 
        {
            _shotSparkParticle.Stop();
            _fired = false;
        }
    }
}
