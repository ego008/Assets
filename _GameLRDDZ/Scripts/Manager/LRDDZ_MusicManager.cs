using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LRDDZ_MusicManager : MonoBehaviour
{
    private static string MUSIC_STATE = "MUSIC_STATE";
    private static string EFFECT_STATE = "EFFECT_STATE";

    private AudioSource mAudioSource; //����������Դ
    private AudioSource mEffectSource; //��Ч��Դ
    private string mAudioSourceName;
    private float m_volume = 1.0f; //����

    private bool _isOpenMusic; //�Ƿ�����Ч
    private bool _isOpenEffect; //�Ƿ�ر���Ч
    public static LRDDZ_MusicManager instance;
    public AudioSource GetAudioSource { get { return mAudioSource; } }
    public void Awake()
    {
        instance = this;
        Init();
    }
    /// <summary>
    /// ��ʼ������
    /// </summary>
    public void Init()
    {
        //��ȡ֮ǰ������״̬
        int musicState = PlayerPrefs.GetInt(MUSIC_STATE, 0);  // 0 ��ʾ����  1��ʾ�ر�
        if (musicState == 0)
        {
            _isOpenMusic = true;
        }
        else
        {
            _isOpenMusic = false;
        }

        //��ȡ֮ǰ����Ч״̬
        int effectState = PlayerPrefs.GetInt(EFFECT_STATE, 0); // 0 ��ʾ����  1��ʾ�ر�
        if (effectState == 0)
        {
            _isOpenEffect = true;
        }
        else
        {
            _isOpenEffect = false;
        }
    }
    /// <summary>
    /// ��ȡ����״̬���Ƿ�����
    /// </summary>
    /// <returns></returns>
    public bool getMusicState()
    {
        return _isOpenMusic;
    }
    /// <summary>
    /// ��ȡ��Ч״̬���Ƿ�����
    /// </summary>
    /// <returns></returns>
    public bool getEffectState()
    {
        return _isOpenEffect;
    }

    /// <summary>
    /// ��������״̬���Ƿ�����
    /// </summary>
    /// <returns></returns>
    public void setMusicState(bool musicState)
    {
        _isOpenMusic = musicState;
        if (_isOpenMusic)
        {
            PlayerPrefs.SetInt(MUSIC_STATE, 0);
            if (mAudioSource && !mAudioSource.isPlaying)
            {
                mAudioSource.UnPause();
            }
        }
        else
        {
            PlayerPrefs.SetInt(MUSIC_STATE, 1);
            mAudioSource.Pause();
        }
    }
    /// <summary>
    /// ������Ч״̬���Ƿ�����
    /// </summary>
    /// <returns></returns>
    public void setEffectState(bool effectState)
    {
        _isOpenEffect = effectState;
        if (_isOpenEffect)
        {
            PlayerPrefs.SetInt(EFFECT_STATE, 0);

        }
        else
        {
            PlayerPrefs.SetInt(EFFECT_STATE, 1);

        }
    }

    /// <summary>
    /// ������Ƶ����
    /// </summary>
    /// <param name="_volume">������С</param>
    public void SetAudioVolume(float _volume)
    {
        if (mAudioSource != null)
        {
            m_volume = _volume;
            mAudioSource.volume = m_volume;
        }
    }
    /// <summary>
    /// ֹͣ����
    /// </summary>
    public void StopPlayAudio()
    {
        if (mAudioSource)
        {
            if (mAudioSource.isPlaying)
            {
                mAudioSource.Stop();
            }
        }
    }

    /// <summary>
    /// ��̬������Ƶ
    /// </summary>
    /// <param name="obj">����</param>
    /// <param name="audioSourceName">��Ƶ����</param>
    /// <param name="loopPlay">ѭ������</param>
    /// <param name="awakeFlag">��������</param>
    /// <param name="isPlayOne">����һ��</param>
    /// <param name="_volume">����</param>
    /// <param name="url">·��</param>
    public void PlayMuisc(string audioSourceName, bool loopPlay = true, bool awakeFlag = true, bool isPlayOne = false, float _volume = 1.0f)
    {
        if (audioSourceName == "") return;
        //mAudioSource = obj.gameObject.GetComponent<AudioSource>();
        if (mAudioSource == null)
        {
            GameObject obj = new GameObject("Muisc");
            obj.transform.position = Vector3.zero;
            mAudioSource = obj.gameObject.AddComponent<AudioSource>() as AudioSource;
        }
        mAudioSourceName = audioSourceName;
        //��Ƶ����
        //AudioClip bgClip = (AudioClip)ResManager.LoadAsset("Sounds", audioSourceName);
        AudioClip bgClip = null;
#if UNITY_EDITOR
        string szAssetName = "Assets/_GameLRDDZ/Build/music/" + audioSourceName + ".ogg";
        bgClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(szAssetName);
        //���û�ҵ�ogg��ʽ ����mp3��ʽ 
        if(bgClip == null)
        {
            szAssetName = "Assets/_GameLRDDZ/Build/music/" + audioSourceName + ".mp3";
            bgClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(szAssetName);
        }

#else     
        bgClip = (AudioClip)LRDDZ_ResourceManager.LoadAsset("music", audioSourceName);
#endif
        if (mAudioSource && bgClip)
        {
            mAudioSource.loop = loopPlay;
            mAudioSource.clip = bgClip;
            mAudioSource.volume = m_volume * _volume;
            mAudioSource.Play();
            if (!_isOpenMusic)
            {
                mAudioSource.Pause();
            }
        }
    }
    /// <summary>
    /// ��̬������Ƶ
    /// </summary>
    /// <param name="obj">����</param>
    /// <param name="buName">buName</param>
    /// <param name="audioSourceName">��Ƶ����</param>
    /// <param name="loopPlay">ѭ������</param>
    /// <param name="awakeFlag">��������</param>
    /// <param name="isPlayOne">����һ��</param>
    /// <param name="_volume">����</param>
    /// <param name="url">·��</param>
    public AudioClip PlayAudio(GameObject obj,string buName, string audioSourceName, bool loopPlay = true, bool awakeFlag = true, bool isPlayOne = false, float _volume = 1.0f)
    {
        if (!_isOpenEffect) return null;
        if (audioSourceName == "") return null;
        //if (!_isOpenEffect) return;
        mEffectSource = obj.gameObject.GetComponent<AudioSource>();

        if (mEffectSource == null)
        {
            mEffectSource = obj.gameObject.AddComponent<AudioSource>() as AudioSource;
        }

        if (loopPlay)
        {
            mEffectSource.loop = true;
        }
        else
        {
            mEffectSource.loop = false;
        }

        if (awakeFlag)
        {
            mEffectSource.playOnAwake = true;
            mEffectSource.volume = 0.5f;
        }
        else
        {
            mEffectSource.playOnAwake = false;
        }
        //��Ƶ����
        //AudioClip bgClip = (AudioClip)Resources.Load("Sounds/" + audioSourceName) as AudioClip;
        AudioClip bgClip = null;
#if UNITY_EDITOR
        string szAssetName = "Assets/_GameLRDDZ/Build/" + buName + "/" + audioSourceName + ".ogg";
        bgClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(szAssetName);
        //���û�ҵ�ogg��ʽ ����mp3��ʽ 
        if (bgClip == null)
        {
            szAssetName = "Assets/_GameLRDDZ/Build/" + buName + "/" + audioSourceName + ".mp3";
            bgClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(szAssetName);
        }

#else
        try
        {
            bgClip = (AudioClip)LRDDZ_ResourceManager.LoadAsset(buName, audioSourceName);
        }
        catch
        {
            Debug.Log("can not find the buNmae  " + "\""+buName+"\"");
        }
#endif
        if (mEffectSource && bgClip)
        {
            mEffectSource.clip = bgClip;
            if (!mEffectSource.isPlaying)
            {
                if (isPlayOne)
                {
                    mEffectSource.PlayOneShot(bgClip);
                    //mAudioSource.audio.volume = m_volume;
                }

                else
                {
                    mEffectSource.Play();
                    //mAudioSource.audio.volume = m_volume*0.5f;
                }
                mEffectSource.volume = m_volume * _volume;
            }
        }
        return bgClip;
    }
    /// <summary>
    /// ��̬������Ƶ
    /// </summary>
    /// <param name="obj">����</param>
    /// <param name="audioSourceName">��Ƶ����</param>
    /// <param name="loopPlay">ѭ������</param>
    /// <param name="awakeFlag">��������</param>
    /// <param name="isPlayOne">����һ��</param>
    /// <param name="_volume">����</param>
    /// <param name="url">·��</param>
    public AudioClip PlayAudio(GameObject obj, string audioSourceName, bool loopPlay = true, bool awakeFlag = true, bool isPlayOne = false, float _volume = 1.0f)
    {
        return PlayAudio(obj, "Sounds", audioSourceName, loopPlay, awakeFlag, isPlayOne, _volume);
    }
    public bool CheckHasAudioFile(string abName,string audioSourceName)
    {
        AudioClip bgClip = null;
#if UNITY_EDITOR
        string szAssetName = "Assets/_GameLRDDZ/Build/" + abName + "/" + audioSourceName + ".ogg";
        bgClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(szAssetName);
#else  
        try
        {   
            bgClip = (AudioClip)LRDDZ_ResourceManager.LoadAsset(abName, audioSourceName);
        }
        catch
        {
            Debug.Log("can not find the buNmae  " + "\""+abName+"\"");
        }
#endif
        if (bgClip != null) return true;
        else return false;
    }
    public void Play(AudioClip clip, Vector3 position,float volume = 0.8f)
    {
        AudioSource.PlayClipAtPoint(clip, position,volume);
    }
    public void PlaySoundEffect(string abName, string audioSourceName)
    {
        PlaySoundEffect(abName, audioSourceName, 0.8f);
    }
    public void PlaySoundEffect(string abName, string audioSourceName,float volume)
    {
        if (!_isOpenEffect) return;
        AudioClip clip = null;
#if UNITY_EDITOR
        string szAssetName = "Assets/_GameLRDDZ/Build/" + abName + "/" + audioSourceName + ".ogg";
        clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(szAssetName);
        //���û�ҵ�ogg��ʽ ����mp3��ʽ 
        if (clip == null)
        {
            szAssetName = "Assets/_GameLRDDZ/Build/" + abName + "/" + audioSourceName + ".mp3";
            clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(szAssetName);
        }
#else
        try
        {
            clip = (AudioClip)LRDDZ_ResourceManager.LoadAsset(abName, audioSourceName);
        }
        catch
        {
            Debug.Log("can not find the abName  " + "\""+abName+"\"");
        }
#endif
        if (clip)
            Play(clip, new Vector3(253, 0, 0), volume);
    }
}