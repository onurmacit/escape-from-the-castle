using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour

{
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public float turnSpeed;
    Animator playerAnim;
    public int level = 15;
    public TextMeshProUGUI levelText;
    private Transform playerTransform;
    private Camera mainCamera;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerTransform = transform;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        Level();

        Upgrade objectInteraction = FindObjectOfType<Upgrade>();

        if (objectInteraction != null)
        {
            objectInteraction.OnObjectInteracted += IncreaseLevelBy5;
        }
    }

    private void IncreaseLevelBy5()
    {
        level += 5;
        levelText.text = "Lv." + level.ToString();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            StartRunAnim();
            PlayerMovement();
        }
        else
        {
            StartIdleAnim();
        }
        UpdateLevelTextPosition();
    }

    private void PlayerMovement()
    {
        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;
        Vector3 addedPos = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0, vertical * speed * Time.fixedDeltaTime);
        playerTransform.position += addedPos;

        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.fixedDeltaTime);
    }

    private void StartRunAnim()
    {
        playerAnim.SetBool("isIdleOn", false);
        playerAnim.SetBool("isRunningOn", true);
    }

    private void StartIdleAnim()
    {
        playerAnim.SetBool("isIdleOn", true);
        playerAnim.SetBool("isRunningOn", false);
    }

    private void StartAttackAnim()
    {
        playerAnim.SetBool("isRunningOn", false);
        playerAnim.SetBool("isAttackOn", true);
    }

    private void StopAttackAnim()
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

    private void UpdateLevelTextPosition()
    {
        levelText.rectTransform.position = mainCamera.WorldToScreenPoint(playerTransform.position);
    }

    public void Level()
    {
        levelText.text = "Lv." + level.ToString();
        UpdateLevelTextPosition();
    }
}