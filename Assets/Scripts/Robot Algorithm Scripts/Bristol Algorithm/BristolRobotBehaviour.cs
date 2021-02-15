using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public BristolAvoidanceGrabbingState avoidanceGrabbingState;
    public BristolAvoidanceDepositState avoidanceDepositState;
    public BristolAvoidanceHoningState avoidanceHoningState;
    public BristolAvoidanceSearchingState avoidanceSearchingState;

    BristolAbstractState currentState;
    [SerializeField]
    private GameObject stateText;
    private StateTextManager stateTextManager;

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
        currentState = avoidanceHoningState;

        // Get object to state text manager script and set text;
        stateTextManager = stateText.GetComponent<StateTextManager>();
        stateTextManager.SetStateString(currentState);

        // Rotate to random direction
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    void Update()
    {
        //currentState.Update();
    }

    private void LateUpdate()
    {
    }

    public void TransitionToState(BristolAbstractState newState)
    {
        currentState = newState;
        stateTextManager.SetStateString(currentState);
        currentState.resetTime();
    }
}
