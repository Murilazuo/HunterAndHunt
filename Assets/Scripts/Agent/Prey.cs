using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Agent
{
    public override void TakeAction()
    {
        MoveInRandomDirection();
    }
}
