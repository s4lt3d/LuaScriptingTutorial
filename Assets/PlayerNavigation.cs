using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour
{
    GameManager gameManager;
    public string goalTag = "Goal";

    [SerializeField]
    private GameObject backpack;

    private readonly List<GameObject> collectedGoals = new();

    private NavMeshAgent agent;
    private GameObject currentGoal;
    private List<GameObject> goals;

    void Awake()
    {
        GameManager.OnLevelLoaded += OnLevelLoaded;
        agent = GetComponent<NavMeshAgent>();
    }
    
    void OnDestroy()
    {
        GameManager.OnLevelLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded()
    {
        goals = GameObject.FindGameObjectsWithTag(goalTag).ToList();
        UpdateDestination();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(goalTag))
        {
            collectedGoals.Add(other.gameObject);
            AddToBackpack(other);
            if(other.gameObject == currentGoal)
                UpdateDestination();
        }
    }

    private void AddToBackpack(Collision other)
    {
        var goalTransform = other.gameObject.transform;
        goalTransform.SetParent(backpack.transform);
        goalTransform.localPosition = Random.insideUnitSphere * 0.5f;
        goalTransform.localRotation = Random.rotation;
    }

    private void UpdateDestination()
    {
        goals.RemoveAll(goal => collectedGoals.Contains(goal));
        if (goals.Count == 0)
            return;
        currentGoal = goals[Random.Range(0, goals.Count)];
        agent.SetDestination(currentGoal.transform.position);
        
    }
    
}