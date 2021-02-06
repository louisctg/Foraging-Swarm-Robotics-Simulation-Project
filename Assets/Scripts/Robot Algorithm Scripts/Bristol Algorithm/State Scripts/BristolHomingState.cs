using UnityEngine;

public class BristolHomingState : BristolAbstractState
{
    public BristolHomingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

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
