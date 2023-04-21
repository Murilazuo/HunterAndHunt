using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Agent
{   
    enum PreyState { Walk, Escape}
    [SerializeField] int distanceToRun;
    
    delegate void State();
    State currentState;
    
    private void Start()
    {
        currentState = WalkState;
    }
    public override void TakeAction()
    {
        currentState?.Invoke();
    }

    void SetState(PreyState state)
    {
        switch (state)
        {
            case PreyState.Walk: currentState = WalkState; break;
            case PreyState.Escape: currentState = EscapeState; break;
        }

        currentState.Invoke();
    }
    void WalkState()
    {
        if (HunterIsInDistanceToRun()) SetState(PreyState.Escape);
        else MoveInRandomDirection();
    }
    void EscapeState()
    {
        if (HunterIsInDistanceToRun()) GoInOpositeHunterDirection(); 
        else SetState(PreyState.Walk);
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
