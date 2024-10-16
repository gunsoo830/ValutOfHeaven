using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public delegate void OnParticlePlayFinish();
    public ParticleSystemStopAction particleFinishType = ParticleSystemStopAction.Disable;

    private ParticleSystem _pSystem;
    private OnParticlePlayFinish _particleFinishCallFunc;
    // Start is called before the first frame update
    protected void Start()
    {
        this._pSystem = this.GetComponent<ParticleSystem>();
        var control = this._pSystem.main;
        control.stopAction = ParticleSystemStopAction.Disable;
    }

    protected void OnDisable()
    {
        if (this.transform.parent == null)
            return;

        switch (this.particleFinishType)
        {
            case ParticleSystemStopAction.Disable:
                this._onParticleFinish();
                break;
            default:
                this._destroyParticle();
                break;
        }

        if (this._particleFinishCallFunc.GetInvocationList().Length > 0)
            this._particleFinishCallFunc();
    }

    // play end
    protected virtual void _onParticleFinish()
    {
        
    }
    protected void _destroyParticle()
    {
        Destroy(this);
    }

    public void setParticleFinishCallFunc(OnParticlePlayFinish callback)
    {
        this._particleFinishCallFunc += callback;
    }
}
