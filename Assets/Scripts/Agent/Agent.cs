using UnityEngine;
using DG.Tweening;

public enum AgentType { Prey,Hunter}
public abstract class Agent : MonoBehaviour
{
    [SerializeField] float timeToMove;
    
    protected Vector2Int Pos
    {
        get
        {
            return new()
            {
                x = (int)transform.position.x,
                y = (int)transform.position.y
            };
        }
    }

    protected delegate void State();
    protected State[] states;
    protected State currentState;
    protected void SetState(int stateId) => currentState = states[stateId];

    public abstract void TakeAction();
    public virtual void DestroyAgent()
    {
        Grid.Instance.SetCell(Pos, null);
        GameManager.instance.RemoveAgent(this);
        Destroy(gameObject);
    }

    protected void Move(Vector2Int direction)
    {
        if (direction == Vector2Int.zero) return;

        transform.DOMove((Vector2)transform.position + (Vector2)direction,timeToMove);

        Grid.Instance.SetCell(Pos+direction, this);
        Grid.Instance.SetCell(Pos, null);
    }
    protected void Move(int x, int y)
    {
        Move(new Vector2Int(x,y));
    }
    protected void MoveInRandomDirection()
    {
        Vector2Int dir = new();
        int count = 0;
        int maxCount = 100;


        do
        {
            dir.x = Random.Range(-1, 1);
            dir.y = Random.Range(-1, 1);

            count++;

            if(count >= maxCount)
            {
                dir = Vector2Int.zero;
                break;
            }

            if (!Grid.Instance.InBounds(Pos + dir)) dir *= -1;

        } while (Grid.Instance.GetCell(Pos + dir) != null);
        

        if (count >= maxCount) dir = Vector2Int.zero;

        Move(dir);
    }
}
