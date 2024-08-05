using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    //�⺻ �ɷ�ġ    >>>���� Info�� ����
    [SerializeField] float HP;
    [SerializeField] float ATK;
    [SerializeField] float SPD;

    //���� �ɷ�ġ
    [HideInInspector] public float curHP;
    [HideInInspector] public float atk;
    [HideInInspector] public float spd;
    public float act_gauge;        //�ൿ������

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
        //>>>���� ����Ʈ
        Debug.Log("���� ��...");
        sprite.color = Color.blue;
        yield return new WaitForSeconds(0.3f);
        

        yield return target.Hit(this);

        sprite.color = Color.white;
    }

    public IEnumerator Hit(BattleUnit other)
    {
        curHP -= other.atk;

        //>>>�ǰ� ����Ʈ
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

        //>>>��� ����Ʈ
        Debug.Log("���!");
        sprite.color = Color.black;
        yield return new WaitForSeconds(0.1f);

        //��� �Ŀ��� ���� ���� ���� (��Ȱ ���ɼ�)
        //Destroy(gameObject);
    }
}
