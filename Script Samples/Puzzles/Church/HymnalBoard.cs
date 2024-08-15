using System.Collections.Generic;
using UnityEngine;


public class HymnalBoard : MonoBehaviour
{
    [SerializeField] private GameObject[] _woodenNumberPrefabs;
    [SerializeField] private Transform[] _rowPositions;

    private const float Y_MARGIN = 0.07f;

    public void SetNumbers(List<ChurchNumberData> combination)
    {
        for (int i = 0; i < combination.Count; i++)
        {
            PlaceNumbers(i, combination[i].Number);
        }     
    }

    private void PlaceNumbers(int row, int number)
    {
        string numberString = number.ToString();
        int numberOfDigits = numberString.Length;

        float[] yOffsets = new float[numberOfDigits];
        float totalHeight = (numberOfDigits - 1) * Y_MARGIN;

        for (int i = 0; i < numberOfDigits; i++)
        {
            float yOffset = (i * Y_MARGIN) - (totalHeight / 2);
            yOffsets[i] = yOffset;
        }

        for (int i = 0; i < numberOfDigits; i++)
        {
            int digit = int.Parse(numberString[i].ToString());
            GameObject numberObj = Instantiate(_woodenNumberPrefabs[digit], _rowPositions[row]);
            Vector3 localPosition = new Vector3(0, yOffsets[i], 0);
            numberObj.transform.localPosition = localPosition;
            numberObj.transform.localRotation = Quaternion.identity;
        }
    }
}