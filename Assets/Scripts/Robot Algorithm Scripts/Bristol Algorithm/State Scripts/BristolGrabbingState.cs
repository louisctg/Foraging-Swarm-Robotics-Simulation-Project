using UnityEngine;

public class BristolGrabbingState : BristolAbstractState
{

    public BristolGrabbingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
        stateString = "G";
    }

    public override void TimeoutChangeState()
    {
        robot.TransitionToState(robot.homingState);
    }

    public override void Update()
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState();
    }
}
