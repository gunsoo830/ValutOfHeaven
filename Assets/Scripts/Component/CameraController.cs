using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class CameraController : MonoBehaviour
{
    public GameObject Canvas;
    public List<GameObject> panels;
    public int defaultPixelPerUnits = 100;
    public int defaultScreenWidth = 1920;
    public int defaultScreenHeight = 1080;


    // Start is called before the first frame update
    protected void Start()
    {
        Camera cam = this.GetComponent<Camera>();
        cam.orthographicSize = (float)defaultScreenHeight / this.defaultPixelPerUnits / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void _setCanvasResolution()
    {
        for(int i=0; i<this.panels.Count; i++)
        {
            if (this.panels[i] != null)
            {
                float ratio = (float)Screen.height / this.defaultScreenHeight;
                Vector3 resizedScale = this.panels[i].transform.localScale;
                resizedScale.y = ratio;
                resizedScale.x = ratio;

                this.panels[i].transform.localScale = resizedScale;
            }
        }
    }
}
