using System;
using Unity.Mathematics;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject targetObject; // The object the compass normally points to
    public float detectionRadius = 5f; // Radius to detect nearby objects
    public float spinSpeed = 360f;     // Speed of the spinning in degrees per second

    void Update()
    {
        // Check for nearby objects
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, detectionRadius);
        bool enemyNearby = false;

        // Iterate through the nearby objects
        foreach (Collider obj in nearbyObjects)
        {
            if (obj.CompareTag("Spooky Tree")) // Check if the object is an enemy
            {
                enemyNearby = true;
                break;
            }
        }

        if (enemyNearby)
        {
            // Spin the needle wildly
            transform.localRotation *= Quaternion.Euler(0, spinSpeed * Time.deltaTime, 0);
        }
        else
        {
            // Normal compass behavior
            Vector3 target = targetObject.transform.position;
            Vector3 relativeTarget = transform.parent.InverseTransformPoint(target);
            float needleRotation = Mathf.Atan2(relativeTarget.x, relativeTarget.z) * Mathf.Rad2Deg + 20f;
            transform.localRotation = Quaternion.Euler(0, needleRotation - 180f, 0);
        }
    }
}
