using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private bool _isDemo;

    [SerializeField, ReadOnly] private List<AudioMusicData> _musicData = new();
    [SerializeField, ReadOnly] private List<AudioSFXData> _sfxData = new();
    [SerializeField, ReadOnly] private List<AudioTensionData> _tensionData = new();

    [SerializeField, ReadOnly] private List<VoiceBlock> _voiceBlocks = new();

    [SerializeField] private AudioSource _audioSourceVoice;
    public AudioSource Voice => _audioSourceVoice;

    [SerializeField] private AudioSourcePool _poolMusic;
    public AudioSourcePool PoolMusic => _poolMusic;
    [SerializeField] private AudioSourcePool _poolSFX;
    public AudioSourcePool PoolSFX => _poolMusic;
    [SerializeField] private AudioSourcePool _poolTension;
    public AudioSourcePool PoolTension=> _poolTension;


    private const float FADE_INCREMENT = 0.015f;

    private const string FOLDER_PATH_AUDIO_2D = "Assets/Settings/Audio/2D";
    private const string FOLDER_PATH_VOICEBLOCKS = "Assets/Settings/Audio/Voice";
    private const string DIRECTORY_KIERAN = "Kieran";
    private const string DIRECTORY_RICHARD = "Richard";


    public AudioClip GetVoiceClip(string identifier)
    {
        for (int i = 0; i < _voiceBlocks.Count; i++)
        {
            if (_voiceBlocks[i].Identifier == identifier)
            {

                return _voiceBlocks[i].RandomVoiceClip();
            }
        }

        return null;
    }

    public void PlayVoice()
    {

    }

    public void PlaySFX(AudioClip audioClip)
    {
        AudioSource source = _poolSFX.GetAudioSource();
        source.clip = audioClip;
        source.Play();
    }

    public void PlayMusic(MusicType type)
    {
        var data = _musicData.FirstOrDefault(m => m.Type == type) as AudioData;

        if (data != null)
        {
            SetAndPlayAudioSource2D(_poolMusic, data);
        }
        else
        {
            Debug.LogError("There's no music by that type");
        }
    }

    public void PlaySFX(SFXType type)
    {
        var data = _sfxData.FirstOrDefault(m => m.Type == type) as AudioData;

        if (data != null)
        {
            SetAndPlayAudioSource2D(_poolSFX, data);
        }
        else
        {
            Debug.LogError("There's no SFX by that type");
        }
    }

    public void PlayTension(TensionType type)
    {
        var data = _tensionData.FirstOrDefault(m => m.Type == type) as AudioData;

        if (data != null)
        {
            SetAndPlayAudioSource2D(_poolTension, data);
        }
        else
        {
            Debug.LogError("There's no tension by that type");
        }
    }

    private void SetAndPlayAudioSource2D(AudioSourcePool pool, AudioData data)
    {
        AudioSource audioSource = pool.GetAudioSource();
        audioSource.volume = data.Volume;
        audioSource.loop = data.Loop;
        audioSource.clip = data.AudioClip;
        audioSource.Play();

        float clipDuration = data.AudioClip.length;
        StartCoroutine(ReturnToPool(pool, audioSource, clipDuration));
    }

    IEnumerator ReturnToPool(AudioSourcePool pool, AudioSource source, float duration)
    {
        yield return new WaitForSeconds(duration);
        pool.ReturnAudioSourceToPool(source);
    }

    public void FadeOutAudio(AudioSource source) => StartCoroutine(CoroutineFadeOutAudio(source));

    public void FadeInAudio(AudioSource source, float defaultvolume) => StartCoroutine(CoroutineFadeInAudio(source, defaultvolume));


    private IEnumerator CoroutineFadeOutAudio(AudioSource source)
    {
        while (source.volume > 0)
        {
            source.volume -= FADE_INCREMENT;
            yield return null;
        }

        source.Stop();
      //  source.gameObject.SetActive(false);
    }

    private IEnumerator CoroutineFadeInAudio(AudioSource source, float defaultVolumne)
    {
        while (defaultVolumne > source.volume)
        {
            source.volume += FADE_INCREMENT;
            yield return null;
        }
    }


#if UNITY_EDITOR
    [Button]
    private void CacheAllAudioData()
    {
        _musicData.Clear();
        _sfxData.Clear();
        _tensionData.Clear();

        string[] files = Directory.GetFiles(FOLDER_PATH_AUDIO_2D, "*.asset", SearchOption.AllDirectories);

        Debug.Log($"Found {files.Length} .asset files in {FOLDER_PATH_AUDIO_2D}");

        foreach (var assetPath in files)
        {
            AudioData data = AssetDatabase.LoadAssetAtPath<AudioData>(assetPath);

            if (data != null)
            {
                if (data is AudioMusicData musicData)
                {
                    _musicData.Add(musicData);
                }
                else if (data is AudioSFXData sfxData)
                {
                    _sfxData.Add(sfxData);
                }
                else if (data is AudioTensionData tensionData)
                {
                    _tensionData.Add(tensionData);
                }
                else
                {
                    Debug.LogWarning($"Unknown AudioData type found at {assetPath}");
                }
            }
            else
            {
                Debug.LogWarning($"Failed to load AudioData from {assetPath}");
            }
        }


        EditorUtility.SetDirty(this);
    }


    [Button]
    private void CacheAllVoices()
    {
        _voiceBlocks.Clear();

        //TODO look into auto cache

        string directoryToExclude = string.Empty;

        if (_isDemo)
            directoryToExclude = DIRECTORY_RICHARD;
        else
            directoryToExclude = DIRECTORY_KIERAN;

        string[] files = Directory.GetFiles(FOLDER_PATH_VOICEBLOCKS, "*.asset", SearchOption.AllDirectories)
                            .Where(file => !file.Contains(directoryToExclude))
                            .ToArray();

        Debug.Log($"Found {files.Length} .asset files in {FOLDER_PATH_VOICEBLOCKS}");

        foreach (var assetPath in files)
        {
            VoiceBlock data = AssetDatabase.LoadAssetAtPath(assetPath.ToString(), typeof(VoiceBlock)) as VoiceBlock;

            if (!ContainsIdentifier(data) && !ContainsAudio(data))
                _voiceBlocks.Add(data);
        }

        EditorUtility.SetDirty(this);
    }

    private bool ContainsIdentifier(VoiceBlock data)
    {
        for (int i = 0; i < _voiceBlocks.Count; i++)
        {
            if (_voiceBlocks[i].Identifier == data.Identifier)
            {
                Debug.LogError("There's already an idendifier by this name: " + data.Identifier);
                Debug.LogError("It's in : " + _voiceBlocks[i].name);
                return true;
            }
        }

        return false;
    }

    private bool ContainsAudio(VoiceBlock data)
    {
        for (int i = 0; i < _voiceBlocks.Count; i++)
        {
            for (int j = 0; j < data.Clips.Length; j++)
            {
                for (int h = 0; h < _voiceBlocks[i].Clips.Length; h++)
                {
                    if (data.Clips[j] == _voiceBlocks[i].Clips[h])
                    {
                        Debug.LogError("There's already a clip by this name: " + data.Clips[j].name + " in " + data.name);
                        Debug.LogError("It's in : " + _voiceBlocks[i].name);
                        return true;
                    }
                }
            }

        }

        return false;
    }
#endif
}

