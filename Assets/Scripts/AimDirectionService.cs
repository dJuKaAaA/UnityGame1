using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDirectionService
{
    private Transform _aimingObject;
    public AimDirectionService(Transform aimingObject)
    {
        _aimingObject = aimingObject;
    }

    private Vector3 _targetPosition = Vector3.zero;
    public Vector3 TargetPosition => _targetPosition;
    
    private Vector3 _targetDirection = Vector3.zero;
    public Vector3 TargetDirection => _targetDirection;

    public void UpdateDirections(Ray targetRay, Vector3 originPosition, float maxDistance)
    {
            Vector3 cameraToPlayerVector = originPosition - Camera.main.transform.position;

            if (Physics.Raycast(targetRay, out RaycastHit hitData, maxDistance) && hitData.transform.position != _aimingObject.position)
            {
                _targetPosition = hitData.point;
            }
            else
            {
                _targetPosition = targetRay.direction * maxDistance + Camera.main.transform.position;
            }
            _targetDirection = Vector3.Normalize(_targetPosition - originPosition);
    }
}
