using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] agentPrefabs;
    [SerializeField] List<Agent> agents = new();
    [SerializeField] int minPrey;
    [SerializeField] int maxPrey;
    [SerializeField] float stepTime;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        int preyCount = Random.Range(minPrey, maxPrey);
        for(int i = 0; i < preyCount; i++)
        {
            SpawnAgentInRandomPosition(AgentType.Prey).name = "Prey " + i;
        }

        SpawnAgentInRandomPosition(AgentType.Hunter).name = "Hunter";
    }
    GameObject SpawnAgentInRandomPosition(AgentType agentType)
    {
        Vector2Int pos;
        int count = 0;
        int maxCount = Grid.Instance.MaxCells();
        
        do
        {
            pos = Grid.Instance.RandomPosInBounds();
            count++;
        } while (Grid.Instance.GetCell(pos) != null && count < maxCount);

        return SpawnAgent(pos, agentType);
    }
    GameObject SpawnAgent(Vector2Int pos, AgentType agentType)
    {
        Agent agent = Instantiate(agentPrefabs[(int)agentType],(Vector2)pos,Quaternion.identity).GetComponent<Agent>();
        Grid.Instance.SetCell(pos, agent);
        agents.Add(agent);

        return agent.gameObject;
    }
    public void NextTurn()
    {
        StartCoroutine(nameof(Turn));
    }
    IEnumerator Turn()
    {
        for(int i = 0; i < agents.Count; i++)
        {
            Agent agent = agents[i];
            agent.TakeAction();
            yield return new WaitForSeconds(stepTime);
        }
        print("End Turn");
    }
    public void RemoveAgent(Agent agent)
    {
        if (agents.Contains(agent))
        {
            agents.Remove(agent);
            agents.TrimExcess();
        }
    }
}
