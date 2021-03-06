using UnityEngine;

public abstract class BristolAbstractState
{
    protected float timeout;
    protected float timeInState;

    protected BristolRobotBehaviour robot;

    protected BristolAbstractState(float timeout, BristolRobotBehaviour robot)
    {
        this.timeout = timeout;
        this.robot = robot;
    }

    public abstract void EnterState(BristolRobotBehaviour robot);

    public abstract void OnTriggerEnter2D(BristolRobotBehaviour robot, Collider2D collision);

    public abstract void Update(BristolRobotBehaviour robot);

    public abstract void TimeoutChangeState(BristolRobotBehaviour robot);

    public void resetTime() { timeInState = 0; }
}
