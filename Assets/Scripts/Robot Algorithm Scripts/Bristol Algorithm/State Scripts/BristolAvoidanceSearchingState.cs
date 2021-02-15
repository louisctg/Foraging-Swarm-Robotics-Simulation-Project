using UnityEngine;

public class BristolAvoidanceSearchingState : BristolAbstractAvoidanceState
{
    public BristolAvoidanceSearchingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
        stateString = "A<sub>S</sub>";
    }


    public override void TimeoutChangeState()
    {
        this.robot.TransitionToState(robot.searchingState);
    }

    public override void Update()
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState();
    }
}
