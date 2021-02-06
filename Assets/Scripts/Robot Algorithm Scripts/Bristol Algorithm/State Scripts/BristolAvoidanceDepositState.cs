using UnityEngine;

public class BristolAvoidanceDepositState : BristolAbstractAvoidanceState
{
    public BristolAvoidanceDepositState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

    public override void TimeoutChangeState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
