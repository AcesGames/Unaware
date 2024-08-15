using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ChurchPuzzle : MonoBehaviour
{
    [SerializeField] private HymnalBoard[] _hymnalBoards;
    [SerializeField] private InteractableLetterLockPuzzle _letterPuzzle;

    [SerializeField] private List<ChurchNumberData> _row1 = new();
    [SerializeField] private List<ChurchNumberData> _row2 = new();
    [SerializeField] private List<ChurchNumberData> _row3 = new();
    [SerializeField] private List<ChurchNumberData> _row4 = new();

    private List<ChurchNumberData> _correctCombination = new();
    private List<ChurchNumberData> _wrongCombination  = new();

    private void Start()
    {
        GenerateCorrectCombination();
        GenerateWrongCombination(); 
        _letterPuzzle.SetRandomStartReels(_correctCombination, _wrongCombination);

        foreach (var board in _hymnalBoards)
        {
            board.SetNumbers(_correctCombination);
        }
    }

    private void GenerateCorrectCombination()
    {
        _correctCombination.Clear();

        int index1 = Random.Range(0, _row1.Count);
        var data1 = _row1[index1];
        _correctCombination.Add(data1);

        int index2 = Random.Range(0, _row2.Count);
        var data2 = _row2[index2];

        while (data1.Number == data2.Number)
        {
            index2 = Random.Range(0, _row2.Count);
            data2 = _row2[index2];
        }

        _correctCombination.Add(data2);

        int index3 = Random.Range(0, _row3.Count);
        var data3 = _row3[index3];

        while (data1.Number == data3.Number || data2.Number == data3.Number)
        {
            index3 = Random.Range(0, _row3.Count);
            data3 = _row3[index3];
        }

        _correctCombination.Add(data3);

        int index4 = Random.Range(0, _row4.Count);
        var data4 = _row4[index4];

        while (data1.Number == data4.Number || data2.Number == data4.Number || data3.Number == data4.Number)
        {
            index4 = Random.Range(0, _row4.Count);
            data4 = _row4[index4];
        }

        _correctCombination.Add(data4);
    }

    private void GenerateWrongCombination()
    {
        _wrongCombination.Clear();

        AddRandomElementToWrongCombination(_row1);
        AddRandomElementToWrongCombination(_row2);
        AddRandomElementToWrongCombination(_row3);
        AddRandomElementToWrongCombination(_row4);
    }

    private void AddRandomElementToWrongCombination(List<ChurchNumberData> row)
    {
        int index = Random.Range(0, row.Count);
        var data = row[index];

        while (_correctCombination.Any(correctData => correctData.Number == data.Number))
        {
            index = Random.Range(0, row.Count);
            data = row[index];
        }

        _wrongCombination.Add(data);
    }
}
