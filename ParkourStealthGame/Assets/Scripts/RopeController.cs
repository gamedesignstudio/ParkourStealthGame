using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    //Object that will interact with the rope
    public Transform ropeConnectedTo;
    public Transform hangingFromRope;

    //used to display the rope
    private LineRenderer lineRenderer;

    //a list with all rope section
    private List<RopeSection> allRopeSections = new List<RopeSection>();

    //Rope data
    private float ropeSectionLength = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Init the line renderer we use to display the rope
        lineRenderer = getComponent<lineRenderer>();

        //Create the rope
        Vector ropeSectionProsition = ropeConnectedTo.position;

        for (int i = 0; i < 15; i++)
        {
            allRopeSections.Add(new RopeSection(ropeSectionProsition));

            ropeSectionProsition.y -= ropeSectionLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Display the rope with the line renderer
        DisplayRope();

        //Move what is hanging from the rope to the end of the rope
        hangingFromRope.position = allRopeSections[allRopeSections.Count - 1].pos;

        //Make what's hanging from the rope look at the next to last rope position 
        //to make it rotate with the rope
        hangingFromRope.LookAt(allRopeSections[allRopeSections.Count - 2].pos);
    }

    void FixedUpdate()
    {
        UpdateRopeSimulation();
    }

    private void UpdateRopeSimulation()
    {
        Vector3 gravityVector = new Vector3(0f, -9.81f, 0f);

        float t = Time.fixedDeltaTime;

        //Move the first section to what the rope is hanging from
        RopeSection firstRopeSection = allRopeSections[0];

        firstRopeSection.pos = ropeConnectedTo.position;

        allRopeSections[0] = firstRopeSection;

        //Move the other rope sections with Verlet intergration
        for (int i = 1; i < allRopeSections.Count; i++)
        {
            RopeSection currentRopeSection = allRopeSections[i];

            //calculate velocity this update
            Vector3 vel = currentRopeSection.pos - currentRopeSection.oldPos;

            //update the old position with the current poition
            currentRopeSection.oldPos = currentRopeSection.pos;

            //Find the new position
            currentRopeSection.pos += gravityVector * t;

            //add gravity
            currentRopeSection.pos += gravityVector * t;

            //add it back to the array
            allRopeSections[i] = currentRopeSection;
        }

        //Make sure the rope sections have the correct length
        for (int i; i < 20; i++)
        {
            ImplementMaximumStretch();
        }
    }

    private void ImplementMaximumStretch()
    {
        for(int i = 0; i < allRopeSections.Count - 1; i++)
        {
            RopeSection topSection = allRopeSections[i];

            //unfinished
        }
    }
}
