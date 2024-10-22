using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static config.SoundConfig;

public class SoundManager : MonoBehaviour
{


    public static SoundManager instance;

    private Dictionary<string, AudioClip> dicAudio = new Dictionary<string, AudioClip>();
    [SerializeField]private List<GameObject> soundObjectList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        this.initSoundObject();
        _initBgmSourcePath();

    }

    private void initSoundObject()
    {
        this.soundObjectList = new List<GameObject>();
        string[] typeNames = Enum.GetNames(typeof(SoundType));
        for(int i=0; i<typeNames.Length - 1; i++)
        {
            GameObject obj = new GameObject(typeNames[i]);
            obj.transform.SetParent(this.transform);
            obj.AddComponent<AudioSource>();
            this.soundObjectList.Add(obj);
        }
    }

    public bool playBGM(string path)
    {
        return this.playSound(path, SoundType.Bgm, true);
    }

    public bool playEffect(string path)
    {
        return this.playSound(path, SoundType.Effect, false);
    }

    public bool playSound(string path, SoundType soundType, bool isLoop = false)
    {
        try
        {
            AudioClip clip = this._loadSound(path);
            this.soundObjectList[(int)soundType].GetComponent<AudioSource>().clip = clip;
            this.soundObjectList[(int)soundType].GetComponent<AudioSource>().loop = isLoop;
            this.soundObjectList[(int)soundType].GetComponent<AudioSource>().Play();
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }

    private AudioClip _loadSound(string path)
    {
        if(this.dicAudio.TryGetValue(path , out AudioClip audioClip) == false)
        {
            this.dicAudio[path] = Resources.Load<AudioClip>(path);
        }

        return this.dicAudio[path];
    }
    
    


}
