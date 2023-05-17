using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCode : MonoBehaviour
{
    public KeyUI keyUI;
    public float rotationSpeed = 10f;
    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        keyUI = FindObjectOfType<KeyUI>();

    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        if (collected)
        {
            Vector3 targetPos = KeyUI.instance.GetIconPosition(transform.position);

            if (Vector2.Distance(transform.position, targetPos) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
            }

            else
            {
                gameObject.SetActive(false);
                if (!keyUI.blueKey.gameObject.activeSelf)
                {
                    keyUI.blueKey.gameObject.SetActive(true);
                }
                else if (!keyUI.redKey.gameObject.activeSelf)
                {
                    keyUI.redKey.gameObject.SetActive(true);
                }
            }
        }
    }
    public void SetCollected()
    {
        collected = true;
    }
}