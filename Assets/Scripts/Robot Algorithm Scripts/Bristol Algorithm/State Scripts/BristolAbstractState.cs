using UnityEngine;

public abstract class BristolAbstractState
{
    protected float timeout;
    protected float timeInState;

    public BristolRobotBehaviour robot;

    protected BristolAbstractState(float timeout, BristolRobotBehaviour robot)
    {
        this.timeout = timeout;
        this.robot = robot;
    }

    public void resetTime()
    {
        timeInState = 0;
    }

    public abstract void TimeoutChangeState();

    public abstract void Update();
}
