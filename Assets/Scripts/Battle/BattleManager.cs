using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum TickEventType
    {
        Update = 0,
        FireEvent,
    }
    public enum BattleShipType
    {
        Player = 0,
        Enemy,
        Count,
        None
    }
    public enum BattleShipAttackType
    {
        Single = 0,
        Multiple,
        Random,
    }


    public delegate void TickEvent(float dt, TickEvent evtType);

    [Header("Battle Setting")]
    public float tickTimeBySeconds = 60;
    public bool isBattleStart = false;
    
    private float _elapsedTime = 0f;
    private List<List<BattleShip>> _battleShipList = new List<List<BattleShip>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        for (int i = 0; i < (int)BattleShipType.Count; i++)
        {
            List<BattleShip> shipList = new List<BattleShip>();
            this._battleShipList.Add(shipList);
        }
    }

    public void PlayBattle()
    {
        this.isBattleStart = true;
    }

    public void StopBattle()
    {
        this.isBattleStart = false;
    }

    private void Update()
    {
        if (this.isBattleStart == false)
            return;

        float dt = Time.deltaTime;
        this._elapsedTime += dt;
        for(int i=0; i<(int)BattleShipType.Count; i++)
        {
            for(int shipindex=0; shipindex < this._battleShipList[i].Count; shipindex++)
            {
                this._battleShipList[i][shipindex].updateTick(dt / this.tickTimeBySeconds);
            }
        }
    }

    public void addBattleShip(BattleShip ship, BattleShipType shipType)
    {
        this._battleShipList[(int)shipType].Add(ship);
    }

    // Attack
    public void attackShip(float attackDmg, BattleShipType targetShipType, BattleShipAttackType attackType, int targetIndex = -1)
    {
        switch(attackType)
        {
            case BattleShipAttackType.Single:
                this._attackSingle(attackDmg, targetShipType, targetIndex);
                break;
            case BattleShipAttackType.Multiple:
                //todo 나중에 배열로 받자....
                break;
            case BattleShipAttackType.Random:
                this._attackRandom(attackDmg, targetShipType, 1);
                break;
        }
        this._battleShipList[(int)BattleShipType.Enemy][0].getDamage(attackDmg);
    }
    private void _attackSingle(float dmg, BattleShipType targetType, int targetIndex)
    {
        this._battleShipList[(int)targetType][targetIndex].getDamage(dmg);
    }
    private void _attackRandom(float dmg, BattleShipType targetType, int attackCount = 1)
    {
        List<int> hitList = new List<int>();
        int count = 0;
        while(count < attackCount)
        {
            int targetIndex = Random.Range(0, this._battleShipList[(int)targetType].Count);
            if (hitList.Find(x => x == targetIndex) != 0)
                continue;
            
            this._attackSingle(dmg, targetType, targetIndex);
            hitList.Add(targetIndex);
            count++;
        }
    }

    // Destroy
    public void destroyShip(BattleShip ship, BattleShipType shipType)
    {
        int index = this._battleShipList[(int)shipType].FindIndex(ob => ob == ship);
        if(index > -1)
        {
            this._battleShipList[(int)shipType].RemoveAt(index);
            this._checkGameFinish();
        }
    }
    private void _checkGameFinish()
    {
        BattleShipType loseType = BattleShipType.None;
        for(int i=0; i<(int)BattleShipType.Count; i++)
        {
            if (this._battleShipList[i].Count <= 0)
            {
                loseType = (BattleShipType) i;
                break;
            }
        }

        if (loseType != BattleShipType.None)
            this.StopBattle();
    }
}
