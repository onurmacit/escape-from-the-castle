using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedDoor : MonoBehaviour
{
    public HingeJoint hinge;
    JointMotor motor;
    public float velaocity;
    public float angle;
    public KeyUI keyUI;

    private GameObject canvas1; 

    private GameObject canvas2; 

    uitween uiTweenScript = new uitween();

    void Start()
    {
        motor = hinge.motor;

        uiTweenScript = FindObjectOfType<uitween>();

        hinge.useMotor = false;
        hinge.useLimits = false;
        hinge.useSpring = false;
        hinge.useMotor = false;
        hinge.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        keyUI = FindObjectOfType<KeyUI>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("RedCube") && keyUI.redKey.gameObject.activeSelf)
        {
            Color redKeyColor = keyUI.redKey.color;
            redKeyColor.a = 0f;
            keyUI.redKey.color = redKeyColor;
        }     
    }
    void Update()
    {
        angle = hinge.angle;

        motor.targetVelocity = -angle;

        if (keyUI != null && keyUI.redKey.gameObject.activeSelf)
        {
            hinge.useMotor = true;
            hinge.useLimits = true;
            hinge.useSpring = true;
            hinge.useMotor = true;
            hinge.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        hinge.motor = motor;
    }

    void CheckHingeAndActivateCanvas()
{
    // hinge değerleri true ise işlemleri yap
    if (hinge != null && hinge.useMotor && hinge.useLimits)
    {
        // İlk olarak canvas öğelerini aktif hale getir
        canvas1.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(true);

        // LevelComplete scriptini çağır
        LevelComplete levelCompleteScript = GetComponent<LevelComplete>();
        if (levelCompleteScript != null)
        {
            levelCompleteScript.CompleteLevel(); // LevelComplete scriptinin uygun bir fonksiyonunu çağır
        }
        else
        {
            Debug.LogError("LevelComplete scripti bulunamadı.");
        }
    }
    else
    {
        Debug.Log("Hinge değerleri kullanılamaz durumda.");
    }
}

}
