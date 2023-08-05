using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject playerText;
    public int level = 15;
    public TextMeshProUGUI levelText;
    Animator playerAnim;

    private Transform playerTransform;
    private Transform enemyTransform;
    private Sequence patrolSequence;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = transform;

        StartRunAnim();
        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
             patrolSequence = DOTween.Sequence();
            patrolSequence.Append(enemyTransform.DOMoveZ(0f, 5f));
            patrolSequence.Append(enemyTransform.DORotate(new Vector3(0f, -180f, 0f), 1f));
            
            patrolSequence.Append(enemyTransform.DOMoveZ(-28.45f, 5f).OnComplete(() => ResetRotation()));
            patrolSequence.AppendInterval(1f); 
            yield return patrolSequence.WaitForCompletion();
        }
    }
private void ResetRotation()
    {
        enemyTransform.DORotate(new Vector3(0f, 0f, 0f), 1f);
    }
    

    void StartRunAnim()
    {
        playerAnim.SetBool("isIdleOn", false);
        playerAnim.SetBool("isRunningOn", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
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

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Lv." + level.ToString();
        levelText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}