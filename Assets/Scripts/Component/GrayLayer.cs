using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GrayLayer : MonoBehaviour
{
    public float opacity = 0.5f;
    public bool isFadeIn = false;
    public float fadeTime = 1f;
    public delegate void FadeInFinishCallFunc();
    private FadeInFinishCallFunc _fadeInFinishCallFunc;

    // Start is called before the first frame update
    void Start()
    {
        if (isFadeIn == true)
            this.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isFadeIn == true)
        {
            this._onFadeIn();
        }
    }
    private void _onFadeIn()
    {
        float currOpacity = this.GetComponent<Image>().color.a;
        currOpacity += this.opacity / fadeTime * Time.deltaTime;
        Color targetColor = Color.black;
        targetColor.a = currOpacity;
        this.GetComponent<Image>().color = targetColor;

        if (targetColor.a > this.opacity)
        {
            this.GetComponent<Image>().color = new Color(0, 0, 0, this.opacity);
            this.isFadeIn = false;
            this._fadeInFinishCallFunc();
        }
    }

    private void Awake()
    {
        var image = this.AddComponent<Image>();
        image.sprite = null;
        image.color = Color.black;
        image.color = new Color(0, 0, 0, this.opacity);

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);

        this.gameObject.SetActive(false);
    }

    public void StartFadeIn(FadeInFinishCallFunc callback)
    {
        this._fadeInFinishCallFunc = callback;
        this.gameObject.SetActive(true);
        this.isFadeIn = true;
    }
}
