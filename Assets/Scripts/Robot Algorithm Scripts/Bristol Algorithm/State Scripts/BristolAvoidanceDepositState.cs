using UnityEngine;

public class BristolAvoidanceDepositState : BristolAbstractAvoidanceState
{
    public BristolAvoidanceDepositState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

    public override void TimeoutChangeState()
    {
        this.robot.TransitionToState(robot.depositState);
    }

    public override void Update()
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState();
    }
}
