using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject playerText;
    public Transform enemy;
    public int level = 15;
    public TextMeshProUGUI levelText;
    Animator playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        StartRunAnim();
        Gitme();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Lv." + level.ToString();
        levelText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    public void Gitme()
    {
        enemy.DOMoveZ(0f, 5f).OnComplete(() =>
    {   
        enemy.DORotate(new Vector3(0f, -180f, 0f), 1f).OnComplete(() =>
        {
            enemy.DORotate(new Vector3(0f, -180f, 0f), 1f).SetDelay(1f).OnComplete(() =>
            {    
                enemy.DORotate(new Vector3(0f, -0.01f, 0f), 1f).SetDelay(3f);
            });
        });
        enemy.DOMoveZ(-28.45f, 5f).SetDelay(1f).OnComplete(() =>
        {    
            Invoke("Gitme", 1f);
        });
    });
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
        if (other.CompareTag("Player"))
        {
            JoystickControl playerScript = other.GetComponent<JoystickControl>();
            if (playerScript != null && level > playerScript.level)
            {
                Destroy(other.gameObject);
                playerText.SetActive(false);
            }
            else
            {
                levelText.enabled = false;
                Destroy(gameObject);
            }
        }
    }
}
