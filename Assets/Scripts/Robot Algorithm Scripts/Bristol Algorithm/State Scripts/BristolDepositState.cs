using UnityEngine;

public class BristolDepositState : BristolAbstractState
{
    public BristolDepositState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
    }

    public override void EnterState(BristolRobotBehaviour robot)
    {
        this.robot.SetStateText("D");
    }

    public override void OnTriggerEnter2D(BristolRobotBehaviour robot, Collider2D collider)
    {
        Debug.Log("Tag (Grabbing): " + collider.gameObject.tag);
    }

    public override void TimeoutChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(this.robot.restingState);
    }
    
    public void AvoidanceChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(this.robot.avoidanceDepositState);
    }

    public override void Update(BristolRobotBehaviour robot)
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState(this.robot);
    }
}
