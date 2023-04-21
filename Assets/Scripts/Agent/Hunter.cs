using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Agent
{
    enum HunterState { Walk, Hunt}
    [SerializeField]List<Prey> preyAround = new();
    [SerializeField] int visionRange;
    [SerializeField] int attackRange;
    public static Hunter Instance;

    delegate void State();
    State currentState;
    private void Start()
    {
        currentState = WalkState;
    }
   
    private void Awake()
    {
        Instance = this;
    }
    public override void TakeAction()
    {
        GetPreysAround();

        currentState?.Invoke();
    }
    void SetState(HunterState state)
    {
        switch (state)
        {
            case HunterState.Walk: currentState = WalkState; break;
            case HunterState.Hunt: currentState = HuntState; break;
        }

        currentState.Invoke();
    }
    void WalkState()
    {
        if (HasPreyArround()) SetState(HunterState.Hunt);
        else MoveInRandomDirection();
    }
    void HuntState()
    {
        if (HasPreyArround())
        {
            if (HasPreyInAttackRange())
                KillPrey(GetCloserPrey());
            else
                GoInPreyDirection();
        }
        else SetState(HunterState.Walk);
    }
    void GoInPreyDirection()
    {
        Prey prey = GetCloserPrey();

        Vector3 dir = (prey.transform.position - transform.position).normalized;

        Vector2Int finalDir = new() { 
            x = dir.x == 0 ? 0 : (dir.x > 0 ? 1 : -1),
            y = dir.y == 0 ? 0 : (dir.y > 0 ? 1 : -1)
        };

        Move(finalDir);
    }
    bool HasPreyArround()
    {
        return preyAround.Count > 0;
    }
    bool HasPreyInAttackRange()
    {
        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int y = -attackRange; y <= attackRange; y++)
            {
                Agent agent = Grid.Instance.GetCell(Pos.x + x, Pos.y + y);
                if (agent)
                    if (agent.GetType() != GetType())
                    {
                        return true;
                    }
            }
        }
        return false;
    }
    Prey GetCloserPrey()
    {
        Prey result = preyAround[0];
        float minDistance = float.MaxValue;
        foreach (Prey p in preyAround)
        {
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if (distance < minDistance)
            {
                result = p;
                minDistance = distance;
            }
        }
        return result;
    }
    void KillPrey(Prey prey)
    {
        prey.DestroyAgent();
    }

    void GetPreysAround()
    {
        preyAround.Clear();
        for(int x = -visionRange; x <= visionRange; x++)
        {
            for(int y = -visionRange; y <= visionRange; y++)
            {
                Agent agent = Grid.Instance.GetCell(Pos.x + x, Pos.y + y);
                if (agent)
                    if (agent.GetType() != GetType())
                        if(Vector2.Distance(transform.position, agent.transform.position) <= visionRange)
                            preyAround.Add((Prey)agent);
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, (attackRange*2) * Vector3.one );
    }
}
