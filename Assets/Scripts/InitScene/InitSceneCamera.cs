using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneCamera : CameraController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        this._setCanvasResolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
