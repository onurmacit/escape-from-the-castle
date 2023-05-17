using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    public Image redKey;
    public Image blueKey;
    public static KeyUI instance;
    public Transform iconTransform;
    private Camera mainCamera;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public Vector3 GetIconPosition(Vector3 target)
    {
        Vector3 uiPos = iconTransform.position;
        uiPos.z = (target - mainCamera.transform.position).z;
        Vector3 result = mainCamera.ScreenToWorldPoint(uiPos);
        return result;
    }
}