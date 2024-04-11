using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    [Multiline(40)]
    private string luaScipt = "";

    [SerializeField]
    private List<GameObject> levelPieces = new();
    private readonly List<string> levelPieceNames = new();
    private Dictionary<string, GameObject> levelPieceDict;
    private Script script = new();

    public void Start()
    {
        SetupLuaVariables();
        RunScript();
    }

    private void SetupLuaVariables()
    {
        script = ScriptRunner.Instance.GetScript();
        levelPieceDict = new Dictionary<string, GameObject>();
        foreach (var levelPiece in levelPieces)
        {
            levelPieceDict.Add(levelPiece.name, levelPiece);
            levelPieceNames.Add(levelPiece.name);
        }

        script.Globals["levelPieces"] = levelPieceNames;
        script.Globals["SpawnLevelPiece"] = (Action<string, Vector3, Quaternion>)SpawnLevelPiece;
    }

    private void RunScript()
    {
        script.DoString(luaScipt);
        GameManager.Instance.LevelLoaded();
    }

    private GameObject GetPrefabByName(string pieceName)
    {
        return levelPieceDict.GetValueOrDefault(pieceName);
    }

    public void SpawnLevelPiece(string pieceName, Vector3 position, Quaternion rotation)
    {
        var prefab = GetPrefabByName(pieceName);
        if (prefab != null)
            Instantiate(prefab, position, rotation);
        else
            Debug.LogError($"Prefab {pieceName} not found!");
    }
    
}