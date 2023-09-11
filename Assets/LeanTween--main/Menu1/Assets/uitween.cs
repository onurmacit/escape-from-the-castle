using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uitween : MonoBehaviour
{
    [SerializeField]
    GameObject backPanel, homeButton, replayButton,
    star1, star2, star3, score, coins, gems, colorWheel, levelSuccess;
    public void LevelSuccess()
    {
        LeanTween.rotateAround(colorWheel, Vector3.forward, -360, 10f).setLoopClamp();
        LeanTween.scale(levelSuccess, new Vector3(1.5f, 1.5f, 1.5f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(LevelComplete);
        LeanTween.moveLocal(levelSuccess, new Vector3(7f, 440f, 2f), 2f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(levelSuccess, new Vector3(4.3f, 4.3f, 4.3f), 2f).setDelay(1.7f).setEase(LeanTweenType.easeInOutCubic);
    }

    void LevelComplete()
    {

        LeanTween.moveLocal(backPanel, new Vector3(21f, -267f, 0f), 0.7f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc).setOnComplete(StarsAnim);
        LeanTween.scale(homeButton, new Vector3(0.25f, 0.25f, 0.25f), 2f).setDelay(.8f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(replayButton, new Vector3(0.25f, 0.25f, 0.25f), 2f).setDelay(.9f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.alpha(score.GetComponent<RectTransform>(), 1f, .5f).setDelay(1f);
        LeanTween.alpha(coins.GetComponent<RectTransform>(), 1f, .5f).setDelay(1.1f);
        LeanTween.alpha(gems.GetComponent<RectTransform>(), 1f, .5f).setDelay(1.2f);
    }

    void StarsAnim()
    {
        LeanTween.scale(star1, new Vector3(0.2f, 0.2f, 0.2f), 2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star2, new Vector3(0.2f, 0.2f, 0.2f), 2f).setDelay(.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star3, new Vector3(0.3f, 0.3f, 0.3f), 2f).setDelay(.2f).setEase(LeanTweenType.easeOutElastic);

    }


}
