using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class LobbyCamera : MonoBehaviour
{
    public int defaultScreenWidth = 1920;
    public int defaultScreenHeight = 1080;
    public GameObject canvasPanel;


    // Start is called before the first frame update
    void Start()
    {
        this.setScreenResolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setScreenResolution()
    {
        Debug.Log("Width : " + Screen.width + " " + "Height : " + Screen.height);

        if(!!canvasPanel)
        {
            float ratio = (float)Screen.height / this.defaultScreenHeight;
            Vector3 resizedScale = canvasPanel.transform.localScale;
            resizedScale.y = ratio;
            resizedScale.x = ratio;

            canvasPanel.transform.localScale = resizedScale;
        }
    }
}
