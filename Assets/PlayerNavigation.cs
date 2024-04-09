using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour
{
    public string goalTag = "Goal";

    [SerializeField]
    private GameObject backpack;

    private readonly List<GameObject> collectedGoals = new();

    private NavMeshAgent agent;
    private GameObject currentGoal;
    private List<GameObject> goals;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goals = GameObject.FindGameObjectsWithTag(goalTag).ToList();
        UpdateDestination();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(goalTag))
        {
            var goalTransform = other.gameObject.transform;
            var resettable = goalTransform.GetComponent<IResettable>();

            goalTransform.SetParent(backpack.transform);
            goalTransform.localPosition = Random.insideUnitSphere * 0.5f;
            goalTransform.localRotation = Random.rotation;

            if (resettable != null) resettable.Reset();
            collectedGoals.Add(other.gameObject);
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        goals.RemoveAll(goal => collectedGoals.Contains(goal));

        if (goals.Count == 0)
            return;

        currentGoal = goals[Random.Range(0, goals.Count)];

        if (currentGoal != null) agent.SetDestination(currentGoal.transform.position);

        collectedGoals.Clear();
    }
}