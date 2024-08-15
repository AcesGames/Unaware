using UnityEngine;


[CreateAssetMenu(fileName = "MusicData", menuName = "Unaware/Audio/Music", order = 0)]
public class AudioMusicData : AudioData
{
    [SerializeField] private MusicType _type;
    public MusicType Type => _type;
}
