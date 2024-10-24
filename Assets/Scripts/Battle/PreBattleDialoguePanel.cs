using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using VOHUtil;

public struct stDialogueInfo
{
    public string CharacterName;
    public string DialogueLine;
    public byte CharacterPositionIndex;

    public stDialogueInfo(string name, string line, byte index)
    {
        CharacterName = name;
        DialogueLine = line;
        CharacterPositionIndex = index;
    }
}
public class PreBattleDialoguePanel : MonoBehaviour
{

#if PROTOTYPE
    [Serializable]
    public class SampleSpritePair
    {
        public string key;
        public Sprite sprite;
    }
    [SerializeField] private List<SampleSpritePair> characterSprites;
#endif
    
    
    public List<stDialogueInfo> Dialogues = new();
    private Action OnCompleteCallback;
    [SerializeField] private TMP_Text lbCharacterName;
    [SerializeField] private TMP_Text lbDialogueLine;
    [SerializeField] private Int32 nowPage = -1;

    [SerializeField] private List<Image> imgCharacters;
    private Color enableColor = Color.white;
    private Color disableColor = Color.gray;


    #region Excel
    
    private string excelPath = "";
    private List<List<string>> excelData;
    private byte characterNameColIndex = 0;
    private byte dialogueColIndex = 1;
    private byte positionColIndex = 2;
    
    #endregion
    

    public void loadDialogue(byte stage)
    {
#if PROTOTYPE
        Dialogues.Add(new stDialogueInfo("officer","DoTalk1...",0));
        Dialogues.Add(new stDialogueInfo("adon","DoTalk2...",3)); 
        Dialogues.Add(new stDialogueInfo("officer","DoTalk3...",0));

#else 
        excelData = ExcelParser.getInstance().ParseExcel(excelPath, 0);
        for (int i = 0; i < excelData.Count; i++)
        {
            Dialogues.Add(new stDialogueInfo(
                excelData[i][characterNameColIndex],
                excelData[i][dialogueColIndex], 
                byte.Parse(excelData[i][positionColIndex])
                ));
        }
#endif

    }

    public void startBattleDialogue(byte stage, Action onCompleteCallback)
    {
        reset();
        loadDialogue(1);
        OnCompleteCallback = onCompleteCallback;
        this.gameObject.SetActive(true);
        nowPage = 0;
        onClickNextTalk();
    }

    public void onClickNextTalk()
    {
        if (Dialogues.Count <= nowPage)
        {
            completePreDialogue();
            return;
        }

        var nowCol = Dialogues[nowPage];

        if (nowCol.CharacterPositionIndex <= imgCharacters.Count)
        {
            imgCharacters[nowCol.CharacterPositionIndex].sprite = characterSprites.Find(s => s.key == nowCol.CharacterName).sprite;
            imgCharacters[nowCol.CharacterPositionIndex].color = enableColor;
            imgCharacters[nowCol.CharacterPositionIndex].gameObject.SetActive(true);
            for (int i = 0; i < imgCharacters.Count; i++)
            {
                if (i == nowCol.CharacterPositionIndex) continue;

                if (imgCharacters[i] != null)
                {
                    imgCharacters[i].color = disableColor;
                }
            }
        }
        lbCharacterName.text = Dialogues[nowPage].CharacterName;
        lbDialogueLine.text = Dialogues[nowPage].DialogueLine;
        nowPage++;
    }

    public void onClickSkipTalk()
    {
        completePreDialogue();
    }
    
    public void completePreDialogue()
    {
        reset();
        OnCompleteCallback.Invoke();
    }

    public void reset()
    {
        foreach (var img in imgCharacters)
        {
            if (img == null) continue;
            img.sprite = default;
            img.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
        Dialogues.Clear();
        lbCharacterName.text = string.Empty;
        lbDialogueLine.text = string.Empty;
        nowPage = -1;
    }
}
