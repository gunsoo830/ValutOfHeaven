using System;
using Unity.Collections;
using UnityEngine;

public class BattleShip : MonoBehaviour
{
    [Header("UI Elements")]
    public ProgressBar HealthGauge;
    public ProgressBar TurnMeterGauge;
    [Space(10)]

    [Header("Ship Attributes")]
    public BattleManager.BattleShipType shipType;
    public float turnSpeed = 1.0f;
    public float maxHealth = 1.0f;
    public float attackDamage = 1.0f;
    public float defenseVal = 1.0f;

    private BattleManager _battleManager;

    [SerializeField, ReadOnly]
    private float _currHealth = 0f;

    private float _turnValPerTick = 0f;
    private float _currTickVal = 0f;
    private float _maxTick = 0f;

    private bool _isTurnGaugeFull = false;

    // Start is called before the first frame update
    void Start()
    {
        this._turnValPerTick = (float)Math.Round((this.turnSpeed * 0.07 / 100), 2);
        this._maxTick = (float)Math.Round((1 / this._turnValPerTick), 2);

        this._currHealth = this.maxHealth;

        this._battleManager = GameObject.FindAnyObjectByType<BattleManager>();
        this._battleManager.addBattleShip(this, this.shipType);

        this.setHealth();
        this.setTurnMeter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth()
    {
        this.HealthGauge.SetPercent((this._currHealth / this.maxHealth) * 100);
    }
    public void setHealth(float health)
    {
        this._currHealth = health;
        this.HealthGauge.SetPercent((health/this.maxHealth) * 100);
    }
    public void setTurnMeter()
    {
        this.TurnMeterGauge.SetPercent((this._currTickVal / this._maxTick) * 100);
    }
    public void setTurnMeter(float tickVal)
    {
        this.TurnMeterGauge.SetPercent((tickVal / this._maxTick) * 100);
    }

    // Tick Update
    public void updateTick(float tick)
    {
        if (this._isTurnGaugeFull == true)
            return;

        Debug.Log("Tick!");
        this._currTickVal += tick;
        this._updateTurnMeter();
    }

    private void _updateTurnMeter()
    {
        if (this._currTickVal > this._maxTick)
            this._onTurnActivate();
        else
            this.setTurnMeter();
    }
    private void _onTurnActivate()
    {
        this._isTurnGaugeFull = true;
        this.setTurnMeter(this._maxTick);
        this._attackShip();
    }

    //Attack
    private void _attackShip()
    {
        Debug.Log("Ship Attack!!");
        //todo function ±¸Çö

        BattleManager.BattleShipType targetType = BattleManager.BattleShipType.Enemy;
        if (this.shipType == BattleManager.BattleShipType.Enemy)
            targetType = BattleManager.BattleShipType.Player;

        this._battleManager.attackShip(this.attackDamage, targetType, BattleManager.BattleShipAttackType.Random);

        this._onAttackFinish();
    }
    private void _onAttackFinish()
    {
        this._isTurnGaugeFull = false;
        this._currTickVal = 0f;
    }

    // Hit
    public void getDamage(float damage)
    {
        this._currHealth -= damage;
        this.setHealth();

        if (this._currHealth < 0f)
            this._onShipDestroy();
    }

    // Death
    private void _onShipDestroy()
    {
        this._battleManager.destroyShip(this, this.shipType);
        Destroy(this.gameObject);
    }
}
