using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _shootingCooldown = 1f;
    [SerializeField] private float _aimOffset = 2f;
    private bool _canShoot = true;
    private AimDirectionService _aimDirectionService = new AimDirectionService();
    private EnemyAI _ai;


    private void Start() 
    {
        _ai = GetComponent<EnemyAI>();
    }

    private void Update() 
    {
        if (_ai.Provoked)
        {
            Ray toTargetRay = new Ray(transform.position, GetAimOffsetTargetDirection());
            _aimDirectionService.UpdateDirections(toTargetRay, _weapon.GetMuzzlePosition(), _ai.AggroRange);
            _weapon.transform.rotation = Quaternion.LookRotation(_target.position - transform.position);  // looks at direction of target
            if (_canShoot)
            {
                StartCoroutine(Shoot(_aimDirectionService.TargetDirection));
            }
        }
    }

    private Vector3 GetAimOffsetTargetDirection()
    {
        Vector3 targetDirection = _target.position - transform.position;
        targetDirection.x += Random.Range(-_aimOffset, _aimOffset);
        targetDirection.y += Random.Range(-_aimOffset, _aimOffset);
        targetDirection.z += Random.Range(-_aimOffset, _aimOffset);
        return targetDirection;
    }

    private IEnumerator Shoot(Vector3 targetDirection)
    {
        _canShoot = false;
        _weapon.Shoot(targetDirection);
        yield return new WaitForSeconds(_shootingCooldown);
        _canShoot = true;
    }

}
