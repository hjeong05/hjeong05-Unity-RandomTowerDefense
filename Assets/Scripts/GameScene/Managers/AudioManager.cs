using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    public Slider bgmSlider;
    public Slider SfxSlider;

    public AudioSource BGMaudioSource; // 배경음 
    public AudioSource[] SFX;   // 효과음 배열
    private float bgmVolume = 0.5f;   
    private float SfxVolume = 0.5f;
    // Use this for initialization
    void Start()
    {
        instance = this;
        bgmVolume = PlayerPrefs.GetFloat("bgmVolume", 1f);
        bgmSlider.value = bgmVolume;
        BGMaudioSource.volume = bgmSlider.value;

        SfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        bgmSlider.value = SfxVolume;
        for(int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = SfxSlider.value;
        }   // 효과음 값 설정
    }
    public void PlayAudio(string name)
    {
        if (name.Equals("Install")) {
            bool Is = true;
            if (Is)
            {
                SFX[0].Play();
                Is = false;
            } else
            {
                SFX[0].Stop();
            }
        } 
        if( name.Equals("Upgrade"))
        {
            bool Is = true;
            if (Is)
            {
                SFX[1].Play();
                Is = false;
            }
            else
            {
                SFX[1].Stop();
            }
        }
        if (name.Equals("Sell"))
        {
            bool Is = true;
            if (Is)
            {
                SFX[2].Play();
                Is = false;
            }
            else
            {
                SFX[2].Stop();
            }
        }
        if (name.Equals("Die"))
        {
            bool Is = true;
            if (Is)
            {
                SFX[3].Play();
                Is = false;
            }
            else
            {
                SFX[3].Stop();
            }
        }
        if (name.Equals("Hit"))
        {
            bool Is = true;
            if (Is)
            {
                SFX[4].Play();
                Is = false;
            }
            else
            {
                SFX[4].Stop();
            }
        }
        if (name.Equals("Panel"))
        {
            bool Is = true;
            if (Is)
            {
                SFX[5].Play();
                Is = false;
            }
            else
            {
                SFX[5].Stop();
            }
        }
        if (name.Equals("Success"))
        {
            bool Is = true;
            if (Is)
            {
                SFX[6].Play();
                Is = false;
            }
            else
            {
                SFX[6].Stop();
            }
        }
    }
	// Update is called once per frame
	void Update () {
        BGMaudioSource.volume = bgmSlider.value;
        bgmVolume = bgmSlider.value;
        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
		
        for(int i = 0; i<SFX.Length; i++)   // 효과음 오디오 제어
        {
            SFX[i].volume = SfxSlider.value;
            SfxVolume = SfxSlider.value;
            PlayerPrefs.SetFloat("SfxVolume", SfxVolume);
        }   
	}
}

