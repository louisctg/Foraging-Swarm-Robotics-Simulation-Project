using UnityEngine;

public abstract class BristolAbstractAvoidanceState : BristolAbstractState
{
    public BristolAbstractAvoidanceState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

    public abstract void AvoidanceChangeState(BristolRobotBehaviour robot);
}
