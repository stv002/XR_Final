using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBook : MonoBehaviour
{
    public GameObject cover;
    public HingeJoint hinge;

    // Start is called before the first frame update
    void Start()
    {
        var hinge = cover.GetComponent<HingeJoint>();
        hinge.useMotor = false;
    }

    // Update is called once per frame
    public void Open_Book()
    {
        hinge.useMotor = true;
    }
}
