using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    private VOHSceneManager _sceneManager;
    public LoadingCanvas loadingCanvas;
    // Start is called before the first frame update
    void Start()
    {
        this._sceneManager = GameObject.FindAnyObjectByType<VOHSceneManager>();
        this._sceneManager.LoadNextScene();

        this.loadingCanvas = GameObject.FindAnyObjectByType<LoadingCanvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLoadingPercent(float percent)
    {
        this.loadingCanvas.SetLoadingPerecet(percent);
        }
}
