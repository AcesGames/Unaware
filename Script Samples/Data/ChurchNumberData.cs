using UnityEngine;


[CreateAssetMenu(fileName = "Church Number Data", menuName = "Unaware/Data/Church Number", order = 0)]
public class ChurchNumberData : ScriptableObject
{
    [SerializeField] private int _number;
    public int Number => _number;
    [SerializeField] private int _row;
    public int Row => _row;
}
