using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    [Multiline(10)]
    string luaScipt = "";

    [SerializeField]
    private List<GameObject> levelPieces = new List<GameObject>();
    
    Script script = new Script();
    private Dictionary<string, GameObject> levelPieceDict;
    private List<string> levelPieceNames = new List<string>();

    private void Awake()
    {
        UserData.RegisterType<Vector3>(); 
        UserData.RegisterType<Quaternion>();
        Script.DefaultOptions.DebugPrint = s => Debug.Log(s);
        script.Globals["print"] = (Action<DynValue>)CustomPrint;
        
        levelPieceDict = new Dictionary<string, GameObject>();
        foreach (GameObject levelPiece in levelPieces)
        {
            levelPieceDict.Add(levelPiece.name, levelPiece);
            levelPieceNames.Add(levelPiece.name);
        }
        
        script.Globals["levelPieces"] = levelPieceNames;
        script.Globals["SpawnLevelPiece"] = (System.Action<string, Vector3, Quaternion>)SpawnLevelPiece;
        
        script.Globals["Vector3"] = (Func<float, float, float, Vector3>)((x, y, z) => new Vector3(x, y, z));
        script.Globals["Quaternion"] = new Table(script);
        
        script.Globals.Get("Quaternion").Table["identity"] = Quaternion.identity;
        script.Globals.Get("Quaternion").Table["Euler"] = (Func<float, float, float, Quaternion>)((x, y, z) => Quaternion.Euler(x, y, z));
    }

    void Start()
    {
        script.DoString(luaScipt);
    }

    void CustomPrint(DynValue value)
    {
        Debug.Log(value.ToPrintString());
    }
    
    public void SpawnLevelPiece(string pieceName, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = GetPrefabByName(pieceName);
        if (prefab != null)
        {
            Instantiate(prefab, position, rotation);
        }
        else
        {
            Debug.LogError($"Prefab {pieceName} not found!");
        }
    }

    private GameObject GetPrefabByName(string pieceName)
    {
        return levelPieceDict.GetValueOrDefault(pieceName);
    }
}
