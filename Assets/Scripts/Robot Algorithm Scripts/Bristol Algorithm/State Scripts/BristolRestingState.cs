using UnityEngine;

public class BristolRestingState : BristolAbstractState
{
    public BristolRestingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
        
    }

    public override void EnterState(BristolRobotBehaviour robot)
    {
        this.robot.SetStateText("R");
    }

    public override void OnTriggerEnter2D(BristolRobotBehaviour robot, Collider2D collider)
    {
        Debug.Log("Tag (Resting): " + collider.gameObject.tag);
    }

    public override void TimeoutChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(this.robot.searchingState);
    }

    public override void Update(BristolRobotBehaviour robot)
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState(this.robot);
    }
}
