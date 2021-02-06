using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BristolSearchingState : BristolAbstractState
{
    public BristolSearchingState(float timeout, BristolRobotBehaviour robot) : base(timeout, robot) { }

    override public void TimeoutChangeState()
    {
        this.robot.TransitionToState(robot.homingState);
    }

    public override void Update()
    {
        this.timeInState += Time.deltaTime;
        if (timeInState >= timeout)
            TimeoutChangeState();
        
        robot.transform.position += robot.transform.right * Time.deltaTime;
    }
}
