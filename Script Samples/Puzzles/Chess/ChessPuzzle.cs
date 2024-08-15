using UnityEngine;

public class ChessPuzzle : InteractablePuzzle
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private GameObject _barrierWalls;

    [SerializeField] private GameObject _key;
    [SerializeField] private Transform _keySpawnPosition;

    [SerializeField] private GameObject _piecePawn;
    [SerializeField] private GameObject _pieceQueen;
    [SerializeField] private GameObject _pieceKnight;

    [SerializeField] private ChessPiece[] _chessPieces;
    [SerializeField] private ChessTile[] _chessTiles;

    private bool _isSolved;
    private Vector3[] _defaultPos;
    private Vector3[] _defaultRot;

    private int _piecesOnTheBoard = 0;

    private const string CHESS_PIECE = "ITEM_CHESSPIECE";
    private const string KNIGHT = "ITEM_CHESSKNIGHT_D";
    private const string PAWN = "ITEM_CHESSPAWN_D";
    private const string QUEEN = "ITEM_CHESSQUEEN_D";

    private void Start()
    {
        StoreOriginPiecePositions();
    }

    public override void Examine()
    {
        if (!_isSolved)
        {
            if (_piecesOnTheBoard == 3)
            {
                _barrierWalls.SetActive(true);
                _boxCollider.enabled = false;

                base.Examine();

                foreach (var piece in _chessPieces)
                {
                    piece.Drag.enabled = true;
                }
            }
            else
            {
                GameInstance.Data.Quest.AcceptQuest(10);

                int piecesCollected = GameInstance.Data.GetVariableDB().GetIntValue(CHESS_PIECE);

                if (piecesCollected == 3)
                {
                    GameInstance.UI.PlayDialogue("Player_ChessPuzzle_Have_All_Pieces");
                }
                else if (piecesCollected < 2)
                {
                    GameInstance.UI.PlayDialogue("Player_ChessPuzzle_Missing_Pieces");
                }
                else if (piecesCollected == 2)
                {
                    GameInstance.UI.PlayDialogue("Player_ChessPuzzle_One_More");
                }
            }
        }
        else
        {
            GameInstance.UI.PlayDialogue("Player_Already_Solved");
        }
    }

    public override void OnCancel()
    {
        base.OnCancel();

        _barrierWalls.SetActive(false);
        _boxCollider.enabled = true;

        foreach (var piece in _chessPieces)
        {
            piece.enabled = false;
        }
    }

    public override bool UseItem(ItemData itemData)
    {
        bool _isChessPiece = SetMissingPieces(itemData);

        return _isChessPiece;
    }

    private bool SetMissingPieces(ItemData itemData)
    {
        if (itemData.Name == CHESS_PIECE)
        {
            if (itemData.Description == QUEEN)
            {
                _pieceQueen.SetActive(true);
            }
            if (itemData.Description == KNIGHT)
            {
                _pieceKnight.SetActive(true);
            }
            if (itemData.Description == PAWN)
            {
                _piecePawn.SetActive(true);
            }

            _piecesOnTheBoard++;

            GameInstance.Sound.PlaySFX(itemData.UseClip);

            if (_piecesOnTheBoard == 3)
            {
                Examine();
            }

            return true;
        }
        else
        {
            GameInstance.UI.PlayDialogue("Player_Generic_Error");
        }

        return false;
    }

    public void CheckCorrectPlacement()
    {
        if (_isSolved) return;

        for (int i = 0; i < _chessTiles.Length; i++)
        {
            if (!_chessTiles[i].IsCorrect)
                return;
        }

        RevealKey();
    }

    private void RevealKey()
    {
        _isSolved = true;
        GameInstance.Data.Quest.CompleteQuest(10);
        GameInstance.Sound.PlaySFX(SFXType.Click);

        _barrierWalls.SetActive(false);

        Instantiate(_key, _keySpawnPosition.position, Quaternion.identity);
    }

    public void ResetBoard()
    {
        for (int i = 0; i < _chessPieces.Length; i++)
        {
            _chessPieces[i].transform.localEulerAngles = _defaultRot[i];
            _chessPieces[i].transform.position = _defaultPos[i];
            _chessPieces[i].SnapPiece(false);
        }
    }

    private void StoreOriginPiecePositions()
    {
        _defaultPos = new Vector3[_chessPieces.Length];
        _defaultRot = new Vector3[_chessPieces.Length];

        for (int i = 0; i < _defaultPos.Length; i++)
        {
            _defaultPos[i] = _chessPieces[i].transform.position;
            _defaultRot[i] = _chessPieces[i].transform.localEulerAngles;
        }
    }
}
