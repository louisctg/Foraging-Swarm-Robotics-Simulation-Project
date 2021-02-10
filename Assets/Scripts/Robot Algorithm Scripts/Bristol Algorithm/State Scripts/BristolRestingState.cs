using UnityEngine;

public class BristolRestingState : BristolAbstractState
{
    public BristolRestingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

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
