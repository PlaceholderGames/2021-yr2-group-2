using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    Image _image = null;
    Coroutine _currentFlashRoutine = null;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void StartFlash(float secondsForOneFlash, float maxAlpha, Color newColor)
    {
        _image.color = newColor;

        //make sure max alpha isnt above 1

        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        //reset current flash animation
        if (_currentFlashRoutine != null)
            StopCoroutine(_currentFlashRoutine);
        _currentFlashRoutine = StartCoroutine(Flash(secondsForOneFlash, maxAlpha));
    }

    IEnumerator Flash(float secondsForOneFlash, float maxAlpha)
    {




        //animate flash half in half out
        float FlashInDuration = secondsForOneFlash / 2;

        for (float t = 0; t <= FlashInDuration; t += Time.deltaTime)
        {
            //create colour change
            //apply colour change


            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / FlashInDuration);
            _image.color = colorThisFrame;

            //wait until next frame
            yield return null;
        }



        //animate flash out same code, but swapped for the flash out.

        float flashOutDuration = secondsForOneFlash / 2;
        for (float t = 0; t < flashOutDuration; t+= Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t / flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
        }
        //ensure alpha goes back to 0
        _image.color = new Color32(0, 0, 0, 0);
    }

}
