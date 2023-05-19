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
    public int level = 15;
    public TextMeshProUGUI levelText;
   
    void Start()
    {
        playerAnim = GetComponent<Animator>();

        Level();

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
            StartRunAnim();
            JoystickMovement();
        }
        else
        {
            StartIdleAnim();
        }
        levelText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
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
    void StartAttackAnim()
    {
        playerAnim.SetBool("isRunningOn", false);
        playerAnim.SetBool("isAttackOn", true);
    }

    void StopAttackAnim()
    {
        playerAnim.SetBool("isAttackOn", false);
        playerAnim.SetBool("isRunningOn", true);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Collectable"))
        {
            other.GetComponent<CollectableCode>().SetCollected();
        }

        if (other.CompareTag("Cone"))
        {
            StartAttackAnim();               
        }

        if (other.CompareTag("Enemy"))
        {
            StopAttackAnim();
        }
    }
    public void Level()
    {
        levelText.text = "Lv." + level.ToString();
        levelText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}