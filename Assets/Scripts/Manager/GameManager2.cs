using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{

    public GameObject IntroUI;
    public GameObject LobbyUI;


    public void Start()
    {
        StartCoroutine(Intro());
    }

    public IEnumerator Intro()
    {
        IntroUI.gameObject.SetActive(true);
        yield return null;

        yield return new WaitForSeconds(2f);
        IntroUI.gameObject.SetActive(false);
        LobbyUI.gameObject.SetActive(true);
    }
}
