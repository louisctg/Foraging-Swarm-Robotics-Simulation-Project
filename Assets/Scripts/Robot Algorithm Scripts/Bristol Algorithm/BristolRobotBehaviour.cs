using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BristolRobotBehaviour : MonoBehaviour
{
    // FSM ----------------------------------------------------------
    // Foraging States
    public BristolGrabbingState grabbingState;
    public BristolDepositState depositState;
    public BristolHomingState homingState;
    public BristolRestingState restingState;
    public BristolSearchingState searchingState;

    // Avoidance States
    public BristolAvoidanceGrabbingState avoidanceGrabbingState;
    public BristolAvoidanceDepositState avoidanceDepositState;
    public BristolAvoidanceHoningState avoidanceHoningState;
    public BristolAvoidanceSearchingState avoidanceSearchingState;

    private BristolAbstractState currentState;

    [SerializeField]
    private GameObject stateText;
    [SerializeField]
    private StateTextManager stateTextManager;

    private FieldOfView FOV;


    private HashSet<Transform> visibleObjects;
    private Transform nearestItem;
    private bool otherRobotInView;

    // Start is called before the first frame update
    void Start()
    {

        // Foraging States
        grabbingState = new BristolGrabbingState(20, this);
        depositState = new BristolDepositState(20, this);
        homingState = new BristolHomingState(20, this);
        restingState = new BristolRestingState(20, this);
        searchingState = new BristolSearchingState(20, this);

        // Avoidance States
        avoidanceGrabbingState = new BristolAvoidanceGrabbingState(5, this);
        avoidanceDepositState = new BristolAvoidanceDepositState(5, this);
        avoidanceHoningState = new BristolAvoidanceHoningState(5, this);
        avoidanceSearchingState = new BristolAvoidanceSearchingState(5, this);

        // Robots always starts in a searching state
        TransitionToState(searchingState);

        this.FOV = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        currentState.Update(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void TransitionToState(BristolAbstractState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
        
        currentState.resetTime();
    }

    public void UpdateVisualInfo(HashSet<Transform> visibleObjects, Transform nearestItem, bool otherRobotInView)
    {
        this.visibleObjects = visibleObjects;
        this.nearestItem = nearestItem;
        this.otherRobotInView = otherRobotInView;
    }

    public bool isRobotInView()
    {
        return this.otherRobotInView;
    }

    public bool isItemInView()
    {
        return this.nearestItem != null;
    }

    public HashSet<Transform> GetVisibleObjects()
    {
        return this.visibleObjects;
    }

    public Transform GetNearestItem()
    {
        return this.nearestItem;
    }

    public void SetNearestItem(Transform newItem)
    {
        this.nearestItem = newItem;
    }

    public void SetStateText(string stateString)
    {
        stateTextManager.SetStateString(stateString);
    }
}
