using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    //기본 능력치    >>>추후 Info에 저장
    [SerializeField] float HP;
    [SerializeField] float ATK;
    [SerializeField] float SPD;

    //현재 능력치
    [HideInInspector] public float curHP;
    [HideInInspector] public float atk;
    [HideInInspector] public float spd;
    public float act_gauge;        //행동게이지

    public bool isDead;
    public int team_id;

    Image sprite;

    private void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public void Init()
    {
        curHP = HP;
        atk = ATK;
        spd = SPD;
        act_gauge = 0;
        isDead = false;
    }

    public IEnumerator Attack(BattleUnit target)
    {
        //>>>공격 이펙트
        Debug.Log("공격 중...");
        sprite.color = Color.blue;
        yield return new WaitForSeconds(0.3f);
        

        yield return target.Hit(this);

        sprite.color = Color.white;
    }

    public IEnumerator Hit(BattleUnit other)
    {
        curHP -= other.atk;

        //>>>피격 이펙트
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;

        if (curHP <= 0)
        {
            curHP = 0;
            yield return Dead();
        }
    }

    IEnumerator Dead()
    {
        isDead = true;
        FindObjectOfType<BattleSystem>().UnitDead(team_id);

        //>>>사망 이펙트
        Debug.Log("사망!");
        sprite.color = Color.black;
        yield return new WaitForSeconds(0.1f);

        //사망 후에도 유닛 정보 저장 (부활 가능성)
        //Destroy(gameObject);
    }
}
