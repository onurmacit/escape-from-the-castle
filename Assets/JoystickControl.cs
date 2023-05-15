using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public float turnSpeed;

    Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            JoystickMovement();
        }
    }

    private void JoystickMovement()
    {
        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;
        Vector3 addedPos = new Vector3(x: horizontal * speed * Time.deltaTime, y: 0, z: vertical * speed * Time.deltaTime);
        transform.position += addedPos;



        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(a: transform.rotation, b: Quaternion.LookRotation(direction), t: turnSpeed * Time.deltaTime);
    }

    void StartRunAnim()
    {
        playerAnim.SetBool("isIdleOn", false);
        playerAnim.SetBool("isRunningOn", true);
    }

    void StartIdleAnim()
    {
        playerAnim.SetBool("isIdleOn", true);
        playerAnim.SetBool("isRunningOn", false);
    }
}
