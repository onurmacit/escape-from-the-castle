using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JoystickControl : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public float turnSpeed;
    Animator playerAnim;
    private int level = 7;
    public TextMeshProUGUI levelText;
    void Start()
    {
        playerAnim = GetComponent<Animator>();

        levelText.text = "Lv." + level.ToString();

        levelText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);

        Upgrade objectInteraction = FindObjectOfType<Upgrade>();

        if (objectInteraction != null)
        {
            objectInteraction.OnObjectInteracted += IncreaseLevelBy5;
        }

        void IncreaseLevelBy5()
        {
            level += 5;
            levelText.text = "Lv." + level.ToString();
        }
    }
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            JoystickMovement();
        }

        levelText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    private bool isMoving = false;

    private void JoystickMovement()
    {
        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;
        Vector3 addedPos = new Vector3(x: horizontal * speed * Time.deltaTime, y: 0, z: vertical * speed * Time.deltaTime);
        transform.position += addedPos;

        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(a: transform.rotation, b: Quaternion.LookRotation(direction), t: turnSpeed * Time.deltaTime);

        if (addedPos.magnitude > 0)
        {
            StartRunAnim();
            isMoving = true;
        }
        else if (isMoving)
        {
            StartIdleAnim();
            isMoving = false;
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.GetComponent<CollectableCode>().SetCollected();
        }
    }
}
