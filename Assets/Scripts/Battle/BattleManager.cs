using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public delegate void TickEvent();

    [Header("Battle Setting")]
    public int tickTimeBySeconds = 60;
    public bool isBattleStart = false;
    
    private float _elapsedTime = 0f;
    private TickEvent _tickEventList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBattle()
    {
        this.isBattleStart = true;
    }

    private void FixedUpdate()
    {
        if (this.isBattleStart == false)
            return;

        this._elapsedTime += Time.fixedDeltaTime;
        if(this._elapsedTime > this.tickTimeBySeconds / 60)
        {
            this._elapsedTime = 0f;
            this._fireTickEvent();
        }
    }

    private void _fireTickEvent()
    {
        this._tickEventList();
    }

    public void addTickEvent(TickEvent evt)
    {
        this._tickEventList += evt;
    }
}
