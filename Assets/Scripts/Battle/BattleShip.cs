using System;
using Unity.Collections;
using Unity.VisualScripting;
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

    [SerializeField, ReadOnly]
    private float _currHealth = 0f;

    private int _targetIndex = -1;
    private float _turnValPerTick = 0f;
    private float _currTickVal = 0f;
    private float _maxTick = 0f;

    private bool _isTurnGaugeFull = false;

    private BattleManager _battleManager;
    private BattleEffectPool _effectPool;
    private BattleBulletPool _bulletPool;

    private Projectile _projectile;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this._turnValPerTick = (float)Math.Round((this.turnSpeed * 0.07 / 100), 2);
        this._maxTick = (float)Math.Round((1 / this._turnValPerTick), 2);

        this._currHealth = this.maxHealth;

        this._battleManager = GameObject.FindAnyObjectByType<BattleManager>();
        this._battleManager.addBattleShip(this, this.shipType);

        this.setHealth();
        this.setTurnMeter();

        this._effectPool = GameObject.FindAnyObjectByType<BattleEffectPool>();
        this._bulletPool = GameObject.FindAnyObjectByType<BattleBulletPool>();
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
        this.HealthGauge.SetPercent((health / this.maxHealth) * 100);
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
        this._fireProjectile();
    }

    //Attack
    protected virtual void _fireProjectile()
    {
        this._targetIndex = this._battleManager.getRandomTargetIndex(this._getTargetType());
        this._createProjectile();

        GameObject from = this.gameObject;
        GameObject to = this._battleManager.getBattleShip(this._getTargetType(), this._targetIndex).gameObject;

        this._projectile.setProjectileInfo(from, to, 0.2f);
        this._projectile.setMoveEndCallFunc(this._onProjectTileHit);

        this._projectile.gameObject.SetActive(true);
        this._projectile.Fire();
    }
    private void _onProjectTileHit()
    {
        this._battleManager.attackShip(this.attackDamage, this._getTargetType(), BattleManager.BattleShipAttackType.Single, this._targetIndex);
        this._onAttackFinish();
    }
    private void _onAttackFinish()
    {
        this._bulletPool.returnPoolObject(0, this._projectile.gameObject);
        this._isTurnGaugeFull = false;
        this._currTickVal = 0f;
        this.setTurnMeter();
    }

    // Hit
    public virtual void getDamage(float damage)
    {
        this._currHealth -= damage;
        this.setHealth();
        this._playHitFx();

        if (this._currHealth < 0f)
            this._onShipDestroy();
    }
    protected virtual void _playHitFx()
    {
        var effect = this._effectPool.getPoolObject(0);
        effect.transform.SetParent(this.transform.parent);
        effect.transform.position = this.transform.position;
        effect.SetActive(true);
    }

    // Death
    private void _onShipDestroy()
    {
        this._battleManager.destroyShip(this, this.shipType);
        Destroy(this.gameObject);
    }

    // Util
    protected virtual void _createProjectile()
    {
        this._projectile = this._bulletPool.getPoolObject(0).GetComponent<Projectile>();
        Transform parent = this._battleManager.getBattleSceneMgr().getPosBullet().transform;
        this._projectile.transform.SetParent(parent);
    }
    private BattleManager.BattleShipType _getTargetType()
    {
        BattleManager.BattleShipType targetType = BattleManager.BattleShipType.Enemy;
        if (this.shipType == BattleManager.BattleShipType.Enemy)
            targetType = BattleManager.BattleShipType.Player;

        return targetType;
    }
}
