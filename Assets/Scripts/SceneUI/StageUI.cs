using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public string battleScene;

    public void MoveToBattle()
    {
        FindAnyObjectByType<GameManager>().MoveToScene(battleScene);
    }
}
