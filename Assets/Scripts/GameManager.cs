using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MoveToScene(nextScene);
    }

    public void MoveToScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
