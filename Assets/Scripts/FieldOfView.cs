using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private Animator enemyAnim;
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    private void Start()
    {
        enemyAnim = GetComponent<Animator>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Vector3 directionToPlayer = playerRef.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer <= radius)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= angle / 2f)
            {
                if (!Physics.SphereCast(transform.position, 0.2f, directionToPlayer, out RaycastHit hit, distanceToPlayer, obstructionMask))
                {
                    StartAttackAnim();
                    canSeePlayer = true;
                    return;
                }
            }
        }

        
        // Player not detected or obstructed
        canSeePlayer = false;
    }

    private void StartAttackAnim()
    {
        enemyAnim.SetBool("isRunningOn", false);
        enemyAnim.SetBool("isAttackOn", true);
    }
}
