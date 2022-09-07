using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDirectionService
{
    private Vector3 _targetPosition = Vector3.zero;
    public Vector3 TargetPosition => _targetPosition;
    
    private Vector3 _targetDirection = Vector3.zero;
    public Vector3 TargetDirection => _targetDirection;

    public void UpdateDirections(Vector3 screenPosition, Vector3 gameObjectPosition, float maxDistance)
    {
            screenPosition.z = Camera.main.nearClipPlane + 1;
            Ray cameraToMouseRay = Camera.main.ScreenPointToRay(screenPosition);
            Vector3 cameraToPlayerVector = gameObjectPosition - Camera.main.transform.position;

            if (Physics.Raycast(cameraToMouseRay, out RaycastHit hitData, maxDistance) && hitData.transform.position != gameObjectPosition)
            {
                _targetPosition = hitData.point;
            }
            else
            {
                _targetPosition = cameraToMouseRay.direction * maxDistance + Camera.main.transform.position;
            }
            _targetDirection = Vector3.Normalize(_targetPosition - gameObjectPosition);
    }
}
