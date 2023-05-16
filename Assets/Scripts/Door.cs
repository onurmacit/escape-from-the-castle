using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public HingeJoint hinge;
    JointMotor motor;
    public float velaocity;
    public float angle;
    void Start()
    {
        motor = hinge.motor;
    }


    void Update()
    {
        angle = hinge.angle;

        motor.targetVelocity = -angle;
        hinge.motor = motor;
    }
}
