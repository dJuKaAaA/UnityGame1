using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bulletParticles;
    [SerializeField] private ParticleSystem _muzzleFlashParticles;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _shotCooldown = 0.5f;
    [SerializeField] private float _damage = 10f;
    
    public float Range => _range;
    private float _bulletVelocity = 1000f;
    public float Damage => _damage;
    private bool _canShoot = true;
    
    private void Start() 
    {
        SetBulletSpeed();
    }

    public void Shoot(Vector3 direction)
    {
        if (_canShoot)
        {
            StartCoroutine(ShootingCorouotine(direction));
        }
    }

    private IEnumerator ShootingCorouotine(Vector3 direction)
    {
        // placeholder solution
        if (direction != Vector3.zero)
        {
            _bulletParticles.transform.rotation = Quaternion.LookRotation(direction);
        }
        
        _bulletParticles.Play();
        _muzzleFlashParticles.Play();
        _canShoot = false;
        yield return new WaitForSeconds(_shotCooldown);
        _canShoot = true;
    }
    
    private void SetBulletSpeed()
    {
        var particleMainSettings = _bulletParticles.main;
        particleMainSettings.startSpeed = _bulletVelocity;
    }

    public Vector3 GetMuzzlePosition()
    {
        return _bulletParticles.transform.position;
    }
}
