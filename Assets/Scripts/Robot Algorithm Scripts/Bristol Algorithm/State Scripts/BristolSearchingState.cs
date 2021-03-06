using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BristolSearchingState : BristolAbstractState
{

    public BristolSearchingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot)
    {
    }

    public override void EnterState(BristolRobotBehaviour robot)
    {
        this.robot.SetStateText("S");
    }

    override public void TimeoutChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(this.robot.homingState);
    }

    public void AvoidanceChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(this.robot.avoidanceSearchingState);
    }

    public void SeenForagingItemChangeState(BristolRobotBehaviour robot)
    {
        this.robot.TransitionToState(this.robot.grabbingState);
    }

    public override void Update(BristolRobotBehaviour robot)
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState(this.robot);

        if (this.robot.isRobotInView())
            AvoidanceChangeState(this.robot);

        if (this.robot.isRobotInView())
            SeenForagingItemChangeState(this.robot);

        //robot.transform.position += robot.transform.right * Time.deltaTime;
    }

    public override void OnTriggerEnter2D(BristolRobotBehaviour robot, Collider2D collider)
    {
        Debug.Log("Tag (Searching): " + collider.gameObject.tag);
    }
}
