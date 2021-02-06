using UnityEngine;

public class BristolAvoidanceHoningState : BristolAbstractAvoidanceState
{
    public BristolAvoidanceHoningState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

    public override void TimeoutChangeState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
