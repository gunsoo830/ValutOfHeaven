using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShip : MonoBehaviour
{
    [Header("UI Elements")]
    public ProgressBar HealthGauge;
    public ProgressBar TurnMeterGauge;
    [Space(10)]

    [Header("Ship Attributes")]
    public float turnSpeed = 1.0f;
    public float maxHealth = 1.0f;

    private float _currHealth = 0f;
    private float _currTurnVal = 0f;

    private float _turnValPerTick = 0f;

    private bool _isTurnGaugeFull = false;

    // Start is called before the first frame update
    void Start()
    {
        this._turnValPerTick = (float)Math.Round((this.turnSpeed * 0.07 / 100), 2);

        GameObject.FindAnyObjectByType<BattleManager>().addTickEvent(this.tickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(float health)
    {
        this._currHealth = health;
        this.HealthGauge.SetPercent(health/this.maxHealth);
    }
    public void setTurnMeter(float meter)
    {
        this._currTurnVal = meter;
        this.TurnMeterGauge.SetPercent(meter * 100);
    }

    public void tickEvent()
    {
        Debug.Log("Tick!");
        this._currTurnVal += this._turnValPerTick;
        this._updateTurnMeter();
    }

    private void _updateTurnMeter()
    {
        if (this._isTurnGaugeFull == true)
            return;

        if (this._currTurnVal > 1)
            this._onTurnActivate();
        else
            this.setTurnMeter(this._currTurnVal);
    }
    private void _onTurnActivate()
    {
        this._isTurnGaugeFull = true;
        this._currTurnVal = 0f;
        this.setTurnMeter(1);
    }
}
