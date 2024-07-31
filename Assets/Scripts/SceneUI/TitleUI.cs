using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            FindAnyObjectByType<GameManager>().MoveToScene(nextScene);
    }
}
