using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public float full_act_gauge = 100;
    public bool is_reset_zero_gauge = true;

    int playerCount;
    int enemyCount;
    [SerializeField] List<BattleUnit> battleUnits;          //유닛 리스트

    public void Init()
    {
        //>>>팀 정보, 스테이지/적 정보 로드

        //유닛 초기화
        foreach (var unit in battleUnits)
        {
            if (unit.team_id == 0) playerCount++;
            else enemyCount++;

            unit.Init();
        }
    }

    private void Start()
    {
        Init();                     //디버그용
        StartCoroutine(Run());      //디버그용
    }

    //전투 진행
    public IEnumerator Run()
    {
        while (true)    //>>>전투 진행 여부 확인 필요
        {
            foreach (var unit in battleUnits)
            {
                if (unit.isDead) continue;

                yield return WaitUnit(unit);

                if (playerCount == 0) { BattleEnd(false); yield break; }
                if (enemyCount == 0) { BattleEnd(true); yield break; }
            }

            yield return null;
        }
    }

    //유닛 행동대기
    IEnumerator WaitUnit(BattleUnit unit)
    {
        unit.act_gauge += unit.spd;

        if (unit.act_gauge >= full_act_gauge)
        {
            BattleUnit target = SetTarget(unit, battleUnits);
            yield return unit.Attack(target);

            unit.act_gauge = is_reset_zero_gauge ?
                0 : unit.act_gauge - full_act_gauge;
        }
    }

    //현재 체력이 가장 많은 유닛을 지정
    BattleUnit SetTarget(BattleUnit actor, List<BattleUnit> units)
    {
        BattleUnit target = null;

        foreach (var unit in units)
        {
            if (actor.team_id == unit.team_id) continue;
            if (unit.isDead) continue;

            if (target == null || target.curHP < unit.curHP)
                target = unit;
        }

        return target;
    }

    //전투종료 (전투승리 or 전투패배)
    void BattleEnd(bool win_battle)
    {
        Debug.Log("b end");
    }

    public void UnitDead(int team_id)
    {
        if (team_id == 0) playerCount--;
        else enemyCount--;
    }
}
