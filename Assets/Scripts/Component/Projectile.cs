using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void onProjectTileMoveFinishCallFunc();

    [Header("Objects")]
    public GameObject fireFx;
    public GameObject hitFx;
    public GameObject projectileObject;

    private GameObject _fireFxOb;
    private GameObject _hitFxOb;
    private GameObject _projectileOb;

    private float _degree = 0f;
    private float _duration = 0f;
    private Vector3 _fromPos;
    private Vector3 _toPos;
    private bool _isStartOnActive = false;

    private bool _isMove = false;

    private float _elapsedTime = 0f;
    private EasingFunction.Ease _easingType = EasingFunction.Ease.Linear;
    private EasingFunction.Function _easeFunc;
    private onProjectTileMoveFinishCallFunc _endCallFunc;

    protected virtual void _reset()
    {
        this._degree = 0f;
        this._duration = 0f;
        this._fromPos = Vector3.zero;
        this._toPos = Vector3.zero;
        this._isStartOnActive = false;
        
        this._isMove = false;

        this._elapsedTime = 0f;
        this._easingType = EasingFunction.Ease.Linear;
        this._easeFunc = null;

        ParticleSystem pSys = null;
        ParticleSystem.MainModule main;
        if (!!this._fireFxOb)
        {
            pSys = this._fireFxOb.GetComponent<ParticleSystem>();
            main =  pSys.main;
            main.loop = false;
            main.playOnAwake = false;
            main.stopAction = ParticleSystemStopAction.Disable;
            this._fireFxOb.SetActive(false);
        }

        if(!!this._projectileOb)
        {
            pSys = this._projectileOb.GetComponent<ParticleSystem>();
            main = pSys.main;
            main.loop = true;
            main.playOnAwake = false;
            main.stopAction = ParticleSystemStopAction.Disable;
            this._projectileOb.SetActive(false);
        }

        if(!!this._hitFxOb)
        {
            pSys = this._hitFxOb.GetComponent<ParticleSystem>();
            main = pSys.main;
            main.loop = false;
            main.playOnAwake = false;
            main.stopAction = ParticleSystemStopAction.Disable;
            this._hitFxOb.SetActive(false);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(!!this.projectileObject)
        {
            this._projectileOb = Instantiate(this.projectileObject);
            this._projectileOb.transform.SetParent(this.transform);
            this._projectileOb.name = "Projectile";
            this._projectileOb.SetActive(false);
        }

        if(!!this.fireFx)
        {
            this._fireFxOb = Instantiate(this.fireFx);
            this._fireFxOb.transform.SetParent(this.transform);
            this._fireFxOb.name = "fireFx";
            this._fireFxOb.SetActive(false);
        }

        if(!!this.hitFx)
        {
            this._hitFxOb = Instantiate(this.hitFx);
            this._hitFxOb.transform.SetParent(this.transform);
            this._hitFxOb.name = "hitFx";
            this._hitFxOb.SetActive(false);
        }

        if (this._isStartOnActive)
            this._isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this._isMove)
        {
            if (this._elapsedTime <= 0)
                this._onMoveStart();

            this._elapsedTime += Time.deltaTime;

            Vector3 currPos = this.transform.localPosition;
            currPos.x = this._easeFunc(this._fromPos.x, this._toPos.x, this._elapsedTime / this._duration);
            currPos.y = this._easeFunc(this._fromPos.y, this._toPos.y, this._elapsedTime / this._duration);
            this.transform.localPosition = currPos;

            if(this._elapsedTime >= this._duration)
            {
                this._isMove = false;
                this._onMoveFinish();
            }
        }
    }

    // Main
    protected virtual void _onMoveStart()
    {
        Vector3 currRot = this.transform.localEulerAngles;
        currRot.z += this._degree;

        if (!!this._fireFxOb)
        {
            this._fireFxOb.SetActive(true);
            this._fireFxOb.GetComponent<ParticleSystem>().Play();
        }

        if(!!this._projectileOb)
        {
            this._projectileOb.SetActive(true);
            this._projectileOb.GetComponent<ParticleSystem>().Play();
        }
    }
    protected virtual void _onMoveFinish()
    {
        if(!!this._hitFxOb)
        {
            this._hitFxOb.SetActive(true);
            this._hitFxOb.GetComponent<ParticleSystem>().Play();
        }

        if (this._endCallFunc.GetInvocationList().Length > 0)
            this._endCallFunc();
    }

    public void setProjectileInfo(GameObject from, GameObject to, float duration, bool isStartOnActive = false, EasingFunction.Ease easingType = EasingFunction.Ease.Linear)
    {
        this._reset();

        this._duration = duration;
        this._isStartOnActive = isStartOnActive;
        this._fromPos = this.transform.InverseTransformPoint(from.transform.position);
        this._toPos = this.transform.InverseTransformPoint(to.transform.position);
        this._degree = Vector2.Angle(this._fromPos, this._toPos);
        this._easeFunc = EasingFunction.GetEasingFunctionDerivative(easingType);
    }
    public void setMoveEndCallFunc(onProjectTileMoveFinishCallFunc callback)
    {
        this._endCallFunc = callback;
    }

    public void Fire()
    {
        this._isMove = true;
    }
}
