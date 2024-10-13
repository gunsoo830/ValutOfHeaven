using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image imgProgress;
    public float progressPercent = 0f;
    public bool isMoveByPosition = false;

    private float _defaultWidth = 0f;
    private float _defaultPosX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(!!imgProgress)
        {
            this.imgProgress.GetComponent<RectTransform>().sizeDelta = this.imgProgress.GetComponent<Image>().overrideSprite.rect.size;
            this._defaultWidth = this.imgProgress.GetComponent<RectTransform>().sizeDelta.x;
            this._defaultPosX = this.imgProgress.GetComponent <RectTransform>().localPosition.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this._updateProgressPercent();
    }

    public void SetPercent(float percent)
    {
        if (percent < 0 || percent > 100)
            return;

        this.progressPercent = percent;
    }

    private void _updateProgressPercent()
    {
        if (this.progressPercent > 100)
            this.progressPercent = 100;

        if (this.progressPercent < 0)
            this.progressPercent = 0;

        if(this.isMoveByPosition == true)
        {
            var currPos = this.imgProgress.GetComponent<RectTransform>().localPosition;
            currPos.x = (this.progressPercent/100) * this._defaultWidth;
            this.imgProgress.GetComponent<RectTransform>().anchoredPosition = currPos;
        } 
        else
        {
            Vector2 defaultSize = this.imgProgress.GetComponent<RectTransform>().sizeDelta;
            this.imgProgress.GetComponent<RectTransform>().sizeDelta = new Vector2(this._defaultWidth * (this.progressPercent / 100), defaultSize.y);
        }
    }
}
