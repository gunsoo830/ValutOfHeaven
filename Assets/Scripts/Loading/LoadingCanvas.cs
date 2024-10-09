using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour
{
    public TMP_Text txtLoading;
    public ProgressBar _loadingProgress;
    public List<GameObject> panels;
    // Start is called before the first frame update
    void Start()
    {
        if( this._loadingProgress != null )
        {
            this._loadingProgress.SetPercent(0);
        }

        if(!!this.txtLoading)
        {
            StartCoroutine(this.updateLoadingText());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLoadingPerecet(float percent)
    {
        this._loadingProgress.SetPercent(percent);
    }

    private void OnDestroy()
    {
        StopCoroutine(this.updateLoadingText());
    }

    private IEnumerator updateLoadingText()
    {
        while(true)
        {
            this._updateLoadingText();
            yield return new WaitForSeconds(1);
        }
    }

    private void _updateLoadingText()
    {
        string[] loadingTxt = new string[3];
        loadingTxt[0] = "Now Loading.";
        loadingTxt[1] = "Now Loading..";
        loadingTxt[2] = "Now Loading...";

        for(int i=0; i<loadingTxt.Length; i++)
        {
            if (loadingTxt[i] == this.txtLoading.text)
            {
                this.txtLoading.text = loadingTxt[(i + 1) % loadingTxt.Length];
                return;
            }
        }
    }

    public List<GameObject> getPanelList()
    {
        return this.panels;
    }
}
