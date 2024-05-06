using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPieces", menuName = "Project/LevelPieces", order = 1)]
public class LevelPieces : ScriptableObject
{
    [SerializeField]
    private List<GameObject> levelPieces = new();
    
    readonly List<string> levelPieceNames = new();
    Dictionary<string, GameObject> levelPieceDict;

    public List<string> LevelPieceNames => levelPieceNames;

    public Dictionary<string, GameObject> LevelPieceDict => levelPieceDict;

    void Awake()
    {
        levelPieceDict = new Dictionary<string, GameObject>();
        foreach (var piece in levelPieces)
        {
            levelPieceNames.Add(piece.name);
            levelPieceDict.Add(piece.name, piece);
        }
    }
    
    public GameObject GetPrefabByName(string pieceName)
    {
        return levelPieceDict.GetValueOrDefault(pieceName);
    }    
}
