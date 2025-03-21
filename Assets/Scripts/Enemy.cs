using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public GameObject playerText;
    public TextMeshProUGUI levelText;
    public int level = 12;
    public Transform enemy;
    public void Start()
    {
        Turn();
    }

    void FixedUpdate()
    {
        levelText.text = "Lv." + level.ToString();
        levelText.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    public void Turn()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(enemy.DORotate(new Vector3(0, 360, 0), 1f));
        sequence.AppendInterval(2f);
        sequence.Append(enemy.DORotate(new Vector3(0, 180, 0), 1f));
        sequence.AppendInterval(2f);
        sequence.SetLoops(-1);
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
}
