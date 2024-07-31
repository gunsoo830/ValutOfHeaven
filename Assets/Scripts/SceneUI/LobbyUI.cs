using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    private GameManager gameM;

    private void Awake()
    {
        gameM = FindAnyObjectByType<GameManager>();
    }

    public void MoveToScene_Lobby(string scene)
    {
        gameM.MoveToScene(scene);
    }
}
