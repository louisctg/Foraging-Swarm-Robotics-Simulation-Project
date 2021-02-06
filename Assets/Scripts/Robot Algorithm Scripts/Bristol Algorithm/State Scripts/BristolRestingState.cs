using UnityEngine;

public class BristolRestingState : BristolAbstractState
{
    public BristolRestingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

    public override void TimeoutChangeState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
