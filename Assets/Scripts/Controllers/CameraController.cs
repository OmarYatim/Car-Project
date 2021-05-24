using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ObjectToFollow;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followspeed = 10;
    [SerializeField] private float lookspeed = 10;


    void LookAtTarget()
    {
        Vector3 lookDirection = ObjectToFollow.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation ,rot, lookspeed * Time.deltaTime);
    }

    void MoveToTarget()
    {
        Vector3 car_velocity = rb.velocity;
        Vector3 car_local_valocity = ObjectToFollow.InverseTransformDirection(car_velocity);
        Vector3 targetPos;
        if(car_local_valocity.z >= -1)
                targetPos = ObjectToFollow.position + 
                    ObjectToFollow.forward *  offset.z + 
                    ObjectToFollow.right * offset.x +
                    ObjectToFollow.up * offset.y;
        else
            targetPos = ObjectToFollow.position - 
                    ObjectToFollow.forward * offset.z +
                    ObjectToFollow.right * offset.x +
                    ObjectToFollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPos, followspeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }
}
