using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public float full_act_gauge = 100;
    public bool is_reset_zero_gauge = true;

    int playerCount;
    int enemyCount;
    [SerializeField] List<BattleUnit> battleUnits;          //���� ����Ʈ

    public void Init()
    {
        //>>>�� ����, ��������/�� ���� �ε�

        //���� �ʱ�ȭ
        foreach (var unit in battleUnits)
        {
            if (unit.team_id == 0) playerCount++;
            else enemyCount++;

            unit.Init();
        }
    }

    private void Start()
    {
        Init();                     //����׿�
        StartCoroutine(Run());      //����׿�
    }

    //���� ����
    public IEnumerator Run()
    {
        while (true)    //>>>���� ���� ���� Ȯ�� �ʿ�
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

    //���� �ൿ���
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

    //���� ü���� ���� ���� ������ ����
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

    //�������� (�����¸� or �����й�)
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
