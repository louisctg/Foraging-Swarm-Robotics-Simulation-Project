using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateTextManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro stateTMP;

    // Update is called once per frame
    void LateUpdate()
    {
        // Keep the text rotation at 0,0,0 for human readability
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetStateString(string stateString)
    {
        if(stateTMP != null)
            this.stateTMP.SetText(stateString);
    }
}
