using UnityEngine;

public class BristolGrabbingState : BristolAbstractState
{

    public BristolGrabbingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
        
    }

    public override void EnterState(BristolRobotBehaviour robot)
    {
        this.robot.SetStateText("G");
    }

    public override void OnTriggerEnter2D(BristolRobotBehaviour robot, Collider2D collider)
    {
        Debug.Log("Tag (Grabbing): " + collider.gameObject.tag);
    }

    public override void TimeoutChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(robot.homingState);
    }

    public override void Update(BristolRobotBehaviour robot)
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState(this.robot);
    }
}
