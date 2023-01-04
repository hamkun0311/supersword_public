using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound{
    public string soundName;    
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("사운드 등록")]
    [SerializeField] Sound[] sfxSounds;

    [Header("효과음 플레이어")]
    public AudioSource[] sfxPlayer;

    public AudioSource background;

    public float BGM;
    public float Effect;
     

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("BGM"))
        {
            BGM = 1;
        } else 
        {
            BGM = PlayerPrefs.GetFloat("BGM");
        }

        if(!PlayerPrefs.HasKey("Effect"))
        {
            Effect = 1;
        } else 
        {
            Effect = PlayerPrefs.GetFloat("Effect");
        }

    }

    // Update is called once per frame
    void Update()
    {
        instance = this;
        BGM = PlayerPrefs.GetFloat("BGM");
        Effect = PlayerPrefs.GetFloat("Effect");
    }


    public void PlaySE(string _soundName)
    {
        for(int i = 0; i < sfxSounds.Length; i++)
        {
            if(_soundName == sfxSounds[i].soundName)
            {
                for(int j=0; j<sfxPlayer.Length; j++)
                {
                    if(!sfxPlayer[j].isPlaying)
                    {
                        if(_soundName == "armor" || _soundName == "book" || _soundName == "sword")
                        {
                            sfxPlayer[j].volume = Effect * 0.3f;
                        }

                        sfxPlayer[j].clip = sfxSounds[i].clip;
                        sfxPlayer[j].Play();
                        sfxPlayer[j].volume = Effect;
                        return;
                    }
                }
                return;
            }

        }
    }
}
