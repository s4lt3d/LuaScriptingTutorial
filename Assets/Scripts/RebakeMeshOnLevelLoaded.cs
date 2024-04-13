
using Unity.AI.Navigation;
using UnityEngine;

public class RebakeMeshOnLevelLoaded : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnLevelLoaded += OnLevelLoaded;
    }

    
    void OnLevelLoaded()
    {
        NavMeshSurface surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }
}

