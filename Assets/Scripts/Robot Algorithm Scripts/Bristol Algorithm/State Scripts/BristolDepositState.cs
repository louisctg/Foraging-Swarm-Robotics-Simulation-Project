using UnityEngine;

public class BristolDepositState : BristolAbstractState
{
    public BristolDepositState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
        this.stateString = "D";
    }

    public override void TimeoutChangeState()
    {
        this.robot.TransitionToState(robot.restingState);
    }

    public override void Update()
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState();
    }
}
