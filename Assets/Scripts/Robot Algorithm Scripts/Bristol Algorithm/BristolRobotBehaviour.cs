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
        grabbingState = new BristolGrabbingState(5, this);
        depositState = new BristolDepositState(5, this);
        homingState = new BristolHomingState(5, this);
        restingState = new BristolRestingState(5, this);
        searchingState = new BristolSearchingState(5, this);

        avoidanceGrabbingState = new BristolAvoidanceGrabbingState(5, this);
        avoidanceDepositState = new BristolAvoidanceDepositState(5, this);
        avoidanceHoningState = new BristolAvoidanceHoningState(5, this);
        avoidanceSearchingState = new BristolAvoidanceSearchingState(5, this);

        // Robots always starts in a searching state
        currentState = searchingState;

        this.stateTextManager = stateText.GetComponent<StateTextManager>();

        stateTextManager.SetStateString(currentState.GetStateString());

        // Rotate to random direction
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    void Update()
    {
        currentState.Update();
    }

    private void LateUpdate()
    {
    }

    public void TransitionToState(BristolAbstractState newState)
    {
        currentState = newState;
        stateTextManager.SetStateString(currentState.GetStateString());
        currentState.resetTime();
    }
}
