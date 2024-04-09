using System;
using MoonSharp.Interpreter;
using UnityEngine;

public class ScriptRunner : MonoBehaviour
{
    private readonly Script script = new();

    public static ScriptRunner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
        UserData.RegisterType<Vector3>();
        UserData.RegisterType<Quaternion>();
        Script.DefaultOptions.DebugPrint = s => Debug.Log(s);
        script.Globals["print"] = (Action<DynValue>)CustomPrint;

        script.Globals["Vector3"] = (Func<float, float, float, Vector3>)((x, y, z) => new Vector3(x, y, z));
        script.Globals["Quaternion"] = new Table(script);

        script.Globals.Get("Quaternion").Table["identity"] = Quaternion.identity;
        script.Globals.Get("Quaternion").Table["Euler"] =
            (Func<float, float, float, Quaternion>)((x, y, z) => Quaternion.Euler(x, y, z));
    }

    private void CustomPrint(DynValue value)
    {
        Debug.Log(value.ToPrintString());
    }

    public Script GetScript()
    {
        return Instance.script;
    }
}