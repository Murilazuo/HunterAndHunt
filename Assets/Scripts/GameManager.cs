using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] agentPrefabs;
    [SerializeField] List<Agent> agents = new();
    [SerializeField] int minPrey;
    [SerializeField] int maxPrey;
    [SerializeField] float stepTime;
    [SerializeField] TMP_Text turnCountText;
    [SerializeField] Button nextTurnButton;
    int turnCount = 0;
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
        turnCount++;
        turnCountText.text = "Turn: " + turnCount;
        StartCoroutine(nameof(Turn));
    }
    IEnumerator Turn()
    {
        nextTurnButton.interactable = false;
        for(int i = 0; i < agents.Count; i++)
        {
            Agent agent = agents[i];
            agent.TakeAction();
            yield return new WaitForSeconds(stepTime);
        }
        nextTurnButton.interactable = true;
        print("End Turn");
    }
    public void Reload()
    {
        SceneManager.LoadScene("Main");
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
