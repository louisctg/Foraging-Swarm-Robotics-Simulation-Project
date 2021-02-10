using UnityEngine;

public class BristolAvoidanceGrabbingState : BristolAbstractAvoidanceState
{
    public BristolAvoidanceGrabbingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }


    public override void TimeoutChangeState()
    {
        this.robot.TransitionToState(robot.grabbingState);
    }

    public override void Update()
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState();
    }
}
