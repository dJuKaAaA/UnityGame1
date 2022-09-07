using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bulletParticles;
    [SerializeField] private float _range = 100f;
    public float Range => _range;
    private float _bulletVelocity = 100f;
    
    private void Start() 
    {
        SetBulletSpeed();
    }

    public void Shoot(Vector3 direction)
    {
        _bulletParticles.transform.rotation = Quaternion.LookRotation(direction);
        _bulletParticles.Play();
    }
    
    private void SetBulletSpeed()
    {
        var particleMainSettings = _bulletParticles.main;
        particleMainSettings.startSpeed = _bulletVelocity;
    }
}
