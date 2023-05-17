using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDoor : MonoBehaviour
{
    public HingeJoint hinge;
    JointMotor motor;
    public float velaocity;
    public float angle;
    public KeyUI keyUI;

    void Start()
    {
        motor = hinge.motor;


        hinge.useMotor = false;
        hinge.useLimits = false;
        hinge.useSpring = false;
        hinge.useMotor = false;
        hinge.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        keyUI = FindObjectOfType<KeyUI>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("BlueCube") && keyUI.blueKey.gameObject.activeSelf)
        {
            Color blueKeyColor = keyUI.blueKey.color;
            blueKeyColor.a = 0f;
            keyUI.blueKey.color = blueKeyColor;
        }
    }
    void Update()
    {
        angle = hinge.angle;

        motor.targetVelocity = -angle;

        if (keyUI != null && keyUI.blueKey.gameObject.activeSelf)
        {
            hinge.useMotor = true;
            hinge.useLimits = true;
            hinge.useSpring = true;
            hinge.useMotor = true;
            hinge.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        hinge.motor = motor;
    }
}