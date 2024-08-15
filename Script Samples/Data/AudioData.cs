using UnityEngine;

public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    public AudioClip AudioClip => _audioClip;

    [SerializeField][Range(0,1)] private float _volume = 1;
    public float Volume => _volume;

    [SerializeField] private bool _loop;
    public bool Loop => _loop;
}
