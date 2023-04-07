using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashLoaded : MonoBehaviour {

    void Start()
    {
        StartCoroutine("Countdown");

    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(8);
        Application.LoadLevel("MenuScene");

    }
}
