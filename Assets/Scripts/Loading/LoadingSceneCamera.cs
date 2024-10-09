using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneCamera : CameraController
{
    private LoadingCanvas _loadingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(!!this.Canvas)
        {
            this._loadingCanvas = GameObject.FindAnyObjectByType<LoadingCanvas>();
            this.setScreenResolution();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setScreenResolution()
    {
        Debug.Log("Width : " + Screen.width + " " + "Height : " + Screen.height);

        List<GameObject> panelList = this._loadingCanvas.getPanelList();
        if (panelList.Count > 0)
        {
            for (int i = 0; i < panelList.Count; i++)
            {
                if (!!panelList[i])
                {
                    float ratio = (float)Screen.height / this.defaultScreenHeight;
                    Vector3 resizedScale = panelList[i].transform.localScale;
                    resizedScale.y = ratio;
                    resizedScale.x = ratio;

                    panelList[i].transform.localScale = resizedScale;
                }
            }
        }
    }
}
