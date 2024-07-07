using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform rotationTarget;
    [SerializeField] private float rotationSpeed = 1f;

    // current target of the platform
    private Vector3 target;
    private Quaternion targetRotation;

    void Start()
    {
        target = endPoint.position;
        targetRotation = Quaternion.LookRotation(endPoint.position - rotationTarget.position, Vector3.up);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // if the platform has reached the target, switch the target to the another point
        if (transform.position == target)
        {
            if (target == endPoint.position)
            {
                target = startPoint.position;
            }
            else
            {
                target = endPoint.position;
            }
            targetRotation *= Quaternion.Euler(0, 180, 0);
        }

        // rotate the platform towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}