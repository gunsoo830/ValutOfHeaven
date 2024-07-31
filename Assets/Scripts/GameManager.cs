using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private sRandom gachaRand;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //gacha Ȯ�����̺��� ���� (���Ŀ��� UID���� ���̺��� �����ؼ� ���)
        gachaRand = new sRandom(100);

        MoveToScene(nextScene);
    }

    public void MoveToScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public int GetGachaRand() => gachaRand.GetRandom();
}
