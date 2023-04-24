using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Agent
{   
    enum PreyState { Walk, Escape}
    [SerializeField] int distanceToRun;
    
    private void Start()
    {
        states = new State[2];

        states[0] = WalkState;
        states[1] = EscapeState;

        SetState(0);
    }
    public override void TakeAction()
    {
        currentState?.Invoke();
    }

    void WalkState()
    {
        if (HunterIsInDistanceToRun()) SetState((int)PreyState.Escape);
        else MoveInRandomDirection();
    }
    void EscapeState()
    {
        if (HunterIsInDistanceToRun()) GoInOpositeHunterDirection(); 
        else SetState((int)PreyState.Walk);
    }
    bool HunterIsInDistanceToRun()
    {
        return Vector3.Distance(Hunter.Instance.transform.position, transform.position) < distanceToRun;
    }

    void GoInOpositeHunterDirection()
    {
        Vector3 dir = (Hunter.Instance.transform.position - transform.position).normalized;
        
        Vector2Int finalDir = new()
        {
            x = dir.x == 0 ? 0 : (dir.x > 0 ? 1 : -1),
            y = dir.y == 0 ? 0 : (dir.y > 0 ? 1 : -1)
        };

        finalDir *= -1;
        if (Grid.Instance.InBounds(Pos + finalDir))
        {

            if (Grid.Instance.GetCell(Pos + finalDir) == null)
            {
                Move(finalDir);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToRun);
    }
}
