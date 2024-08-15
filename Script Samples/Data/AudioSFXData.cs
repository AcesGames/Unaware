using UnityEngine;

[CreateAssetMenu(fileName = "SFXData", menuName = "Unaware/Audio/SFX", order = 0)]
public class AudioSFXData : AudioData
{
    [SerializeField] private SFXType _type;
    public SFXType Type => _type;
}
