using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AgentNavigation : MonoBehaviour
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
        GameManager.OnGameStart += OnGameStart;
        agent = GetComponent<NavMeshAgent>();
    }
    
    void OnDestroy()
    {
        GameManager.OnLevelLoaded -= OnGameStart;
    }

    private void OnGameStart()
    {
        goals = GameObject.FindGameObjectsWithTag(goalTag).ToList();
        UpdateDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(goalTag))
        {
            collectedGoals.Add(other.gameObject);
            AddToBackpack(other.gameObject.transform);
            if(other.gameObject == currentGoal)
                UpdateDestination();
        }
    }

    private void AddToBackpack(Transform other)
    {
        var goalTransform = other;
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

        if(!agent.SetDestination(currentGoal.transform.position))
            Debug.LogError("Could not set destination");
    }
}