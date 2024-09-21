using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class LobbyCamera : MonoBehaviour
{
    public GameObject Canvas;
    public int defaultScreenWidth = 1920;
    public int defaultScreenHeight = 1080;

    private LobbyCanvas lobbyCanvas;


    // Start is called before the first frame update
    void Start()
    {
        if(!!this.Canvas)
        {
            this.lobbyCanvas = Canvas.GetComponent<LobbyCanvas>();
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

        List<GameObject> panelList = this.lobbyCanvas.getPanelList();
        if(panelList.Count > 0)
        {
            for(int i=0; i<panelList.Count; i++) 
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
