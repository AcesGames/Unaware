using UnityEngine;

public class ChessTile : MonoBehaviour
{
    [SerializeField] private ChessPuzzle _chessPuzzle;
    [SerializeField] private ChessPieces _correctChessPiece;

    public bool IsCorrect;

    //private bool _isCorrect;
    //public bool IsCorrect => _isCorrect;

    private void OnTriggerEnter(Collider other)
    {
        ChessPiece chessPiece = other.gameObject.GetComponentInParent<ChessPiece>();

        if (chessPiece != null && chessPiece.ChessPieceType == _correctChessPiece)
        {
            IsCorrect = true;
            chessPiece.SnapPiece(true);

            float yValue = other.transform.parent.position.y;
            chessPiece.transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
            _chessPuzzle.CheckCorrectPlacement();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ChessPiece chessPiece = other.gameObject.GetComponentInParent<ChessPiece>();

        if (chessPiece != null)
        {
            IsCorrect = false;
        }
    }
}
