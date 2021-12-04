using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneBehavior : MonoBehaviour
{
    protected void UpdatePosition(Vector3 targetPosition, float speed)
    {
        Vector3 direction = targetPosition - transform.position;
        direction = direction.normalized;
        transform.position += direction * Time.deltaTime * speed;
    }
}
