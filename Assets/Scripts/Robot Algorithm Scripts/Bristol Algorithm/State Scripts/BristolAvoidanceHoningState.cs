using UnityEngine;

public class BristolAvoidanceHoningState : BristolAbstractAvoidanceState
{
    public BristolAvoidanceHoningState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
        this.stateString = "A<sub>H</sub>";
    }

    public override void TimeoutChangeState()
    {
        this.robot.TransitionToState(robot.homingState);
    }

    public override void Update()
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState();
    }
}
