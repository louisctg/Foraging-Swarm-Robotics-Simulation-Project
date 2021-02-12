using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BristolRobotBehaviour : MonoBehaviour
{
    // Foraging States
    public BristolGrabbingState grabbingState;
    public BristolDepositState depositState;
    public BristolHomingState homingState;
    public BristolRestingState restingState;
    public BristolSearchingState searchingState; // Initial State

    // Avoidance States
    BristolAvoidanceGrabbingState avoidanceGrabbingState;
    BristolAvoidanceDepositState avoidanceDepositState;
    BristolAvoidanceHoningState avoidanceHoningState;
    BristolAvoidanceSearchingState avoidanceSearchingState;

    BristolAbstractState currentState;

    // Start is called before the first frame update
    void Start()
    {
        grabbingState = new BristolGrabbingState(20, this);
        depositState = new BristolDepositState(20, this);
        homingState = new BristolHomingState(20, this);
        restingState = new BristolRestingState(20, this);
        searchingState = new BristolSearchingState(20, this);

        avoidanceGrabbingState = new BristolAvoidanceGrabbingState(20, this);
        avoidanceDepositState = new BristolAvoidanceDepositState(20, this);
        avoidanceHoningState = new BristolAvoidanceHoningState(20, this);
        avoidanceSearchingState = new BristolAvoidanceSearchingState(20, this);

        // Robots always starts in a searching state
        currentState = searchingState;

        // Rotate to random direction
        transform.Rotate(0, 0, Random.Range(0, 360));
    }

    void Update()
    {
        //currentState.Update();
    }

    public void TransitionToState(BristolAbstractState newState)
    {
        currentState = newState;
        currentState.resetTime();
    }
}
