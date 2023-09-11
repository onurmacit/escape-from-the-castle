using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public uitween uiTweenScript; 
    public GameObject canvas1; 
    public GameObject canvas2; 


    private void Start()
    {   
        uiTweenScript = FindObjectOfType<uitween>();

         canvas1.SetActive(false);
        canvas2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (uiTweenScript != null)
            {
                uiTweenScript.LevelSuccess();

                 canvas1.SetActive(true);
                canvas2.SetActive(true);
                
            }
            else
            {
                Debug.LogError("uitween scripti bulunamadı.");
            }
        }
    }
}
