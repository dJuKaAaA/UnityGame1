using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _aimOffset = 2f;
    
    private EnemyAI _ai;


    private void Start() 
    {
        _ai = GetComponent<EnemyAI>();
    }

    private void Update() 
    {
        if (_ai.Provoked)
        {
            _weapon.transform.rotation = Quaternion.LookRotation(_target.position - transform.position);  // looks at direction of target
            _weapon.Shoot(GetTargetDirectionWithOffset());
        }
    }

    private Vector3 GetTargetDirectionWithOffset()
    {
        Vector3 targetDirection = _target.position - _weapon.GetMuzzlePosition();
        targetDirection.x += Random.Range(-_aimOffset, _aimOffset);
        targetDirection.y += Random.Range(-_aimOffset, _aimOffset);
        targetDirection.z += Random.Range(-_aimOffset, _aimOffset);
        return targetDirection;
    }
}
