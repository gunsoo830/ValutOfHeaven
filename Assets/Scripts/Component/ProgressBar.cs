using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image imgProgress;
    public float progressPercent = 0f;

    private float _defaultWidth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(!!imgProgress)
        {
            this.imgProgress.GetComponent<RectTransform>().sizeDelta = this.imgProgress.GetComponent<Image>().overrideSprite.rect.size;
            this._defaultWidth = this.imgProgress.GetComponent<RectTransform>().sizeDelta.x;
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
        Vector2 defaultSize = this.imgProgress.GetComponent<RectTransform>().sizeDelta;
        this.imgProgress.GetComponent<RectTransform>().sizeDelta = new Vector2(this._defaultWidth * (this.progressPercent / 100), defaultSize.y);
    }
}
