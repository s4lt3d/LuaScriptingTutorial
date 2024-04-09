using UnityEngine;
using MoonSharp.Interpreter;

public class LuaScriptRunner : MonoBehaviour
{
    void Start()
    {
        // Initialize the Lua environment
        Script.DefaultOptions.DebugPrint = s => Debug.Log(s); // Redirect Lua print to Unity Console
        Script luaScript = new Script();

        // Lua script as a string
        string scriptCode = @"
            function addNumbers(a, b)
                return a + b
            end
            
            print('Lua script executed!')
            return addNumbers(5, 3)
        ";

        try
        {
            // Execute the Lua script from the string
            DynValue res = luaScript.DoString(scriptCode);

            // Printing the result to the Unity console
            Debug.Log($"The result is: {res.Number}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error running Lua script: {ex.Message}");
        }
    }
}