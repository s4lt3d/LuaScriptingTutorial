using System;
using System.IO;
using MoonSharp.Interpreter;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private LevelPieces levelPieces;
    
    [SerializeField]
    private string luaSciptFileName = "level_0";
    
    private string luaScipt;
    
    private Script script = new();

    public void Start()
    {
        SetupLuaVariables();
        if(LoadScriptContents())
            RunScript();
        GameManager.Instance.LevelLoaded();
    }

    private bool LoadScriptContents()
    {
        string path = Path.Combine(Application.streamingAssetsPath, luaSciptFileName + ".lua");
        if (File.Exists(path))
        {
            luaScipt = File.ReadAllText(path);
            Debug.Log("Loaded LUA Script: " + luaSciptFileName);
            return true;
        }

        Debug.LogError("Failed to find LUA script!");
        return false;
    }

    private void SetupLuaVariables()
    {
        script = ScriptRunner.Instance.GetScript();
        
        script.Globals["levelPieces"] = levelPieces.LevelPieceNames;
        script.Globals["SpawnLevelPiece"] = (Action<string, Vector3, Quaternion>)SpawnLevelPiece;
    }

    private void RunScript()
    {
        script.DoString(luaScipt);
    }

    public void SpawnLevelPiece(string pieceName, Vector3 position, Quaternion rotation)
    {
        var prefab = levelPieces.GetPrefabByName(pieceName);
        if (prefab != null)
            Instantiate(prefab, position, rotation);
        else
            Debug.LogError($"Prefab {pieceName} not found!");
    }
}