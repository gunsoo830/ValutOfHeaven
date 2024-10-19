using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private BattleSceneManager _battleSceneMgr;

    [Header("Battle Setting")]
    public float tickTimeBySeconds = 60;
    public bool isBattleStart = false;
    
    private float _elapsedTime = 0f;
    private List<List<BattleShip>> _battleShipList = new List<List<BattleShip>>();

    // Start is called before the first frame update
    void Start()
    {
        this._battleSceneMgr = GameObject.FindAnyObjectByType<BattleSceneManager>();
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
                //todo ���߿� �迭�� ����....
                break;
            case BattleShipAttackType.Random:
                this._attackRandom(attackDmg, targetShipType, 1);
                break;
        }
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

    public int getRandomTargetIndex(BattleShipType type)
    {
        if (this._battleShipList[(int)type].Count < 1)
            return -1;

        return Random.Range(0, this._battleShipList[(int)type].Count);
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

        if (loseType != BattleShipType.None) {
            this.StopBattle();
            this._onBattleFinish();
        }
    }

    private void _onBattleFinish()
    {
        StartCoroutine(this._goToLobby());
    }
    private IEnumerator _goToLobby()
    {
        yield return new WaitForSeconds(3.0f);
        VOHSceneManager mgr = GameObject.FindAnyObjectByType<VOHSceneManager>();
        mgr.ChangeSceneByName("LobbyScene");
    }

    //
    public BattleShip getBattleShip(BattleShipType shipType, int index)
    {
        return this._battleShipList[(int)shipType][index];
    }
    public BattleSceneManager getBattleSceneMgr()
    {
        return this._battleSceneMgr;
    }
}
