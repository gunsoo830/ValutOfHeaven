using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VOHSceneManager : MonoBehaviour
{
    public static VOHSceneManager instance;

    private string _nextScene;
    private string _currSceneName;
    // Start is called before the first frame update
    void Start()
    {
        this._currSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadNextScene()
    {
        if(this._nextScene.Length > 0)
        {
            StartCoroutine(this._loadSceneAsync(this._nextScene));
            //SceneManager.LoadScene(this._nextScene);
        }
    }
    public void ChangeSceneByName(string name)
    {
        this._nextScene = name;
        this.ChangeToLoadingScene();
    }

    public void ChangeToLoadingScene()
    {
        SceneManager.LoadScene("LoadingScene",LoadSceneMode.Single);
    }

    
    private IEnumerator _loadSceneAsync(string name)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        //oper.allowSceneActivation = false;
        while(oper.isDone == false && oper.progress < 1f)
        {
            Debug.Log(oper.progress);
            this._onSceneLoading(oper.progress);
            yield return null;
        }

        this._onSceneLoadFinish(name);
    }
    private void _onSceneLoading(float percent)
    {
        LoadingSceneManager mgr = GameObject.FindObjectOfType<LoadingSceneManager>();
        if(!!mgr)
        {
            mgr.SetLoadingPercent(Mathf.Floor( percent * 100 ));
        }
    }
    private void _onSceneLoadFinish(string name)
    {
        StartCoroutine(this._unloadSceneAsync("LoadingScene"));

        this._currSceneName = name;
        this._nextScene = "";
    }

    private IEnumerator _unloadSceneAsync(string name)
    {
        AsyncOperation oper = SceneManager.UnloadSceneAsync(name);
        while(oper.isDone == false)
        {
            Debug.Log(oper.progress);
            yield return null;
        }

        oper.allowSceneActivation = false;
    }
}
