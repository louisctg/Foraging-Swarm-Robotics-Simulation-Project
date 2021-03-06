using UnityEngine;

public class BristolAvoidanceHoningState : BristolAbstractAvoidanceState
{
    public BristolAvoidanceHoningState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
        
    }

    public override void EnterState(BristolRobotBehaviour robot)
    {
        this.robot.SetStateText("A<sub>H</sub>");
    }

    public override void OnTriggerEnter2D(BristolRobotBehaviour robot, Collider2D collider)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(BristolRobotBehaviour robot)
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState(this.robot);
    }

    public override void TimeoutChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(this.robot.homingState);
    }

    public override void AvoidanceChangeState(BristolRobotBehaviour robot)
    {
        throw new System.NotImplementedException();
    }
}
