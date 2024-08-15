using UnityEngine;


[CreateAssetMenu(fileName = "TensionData", menuName = "Unaware/Audio/Tension", order = 0)]
public class AudioTensionData : AudioData
{
    [SerializeField] private TensionType _type;
    public TensionType Type => _type;
}
