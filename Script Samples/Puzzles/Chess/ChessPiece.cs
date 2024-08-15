using UnityEngine;


public class ChessPiece : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private DragObject _drag;
    public DragObject Drag => _drag;

    public ChessPieces ChessPieceType;

    public void SnapPiece(bool value)
    {
        _rigidBody.isKinematic = value;
        _drag.enabled = !value;
    }
}
