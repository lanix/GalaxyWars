using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityPull : MonoBehaviour
{
    Mesh myMesh;
    Vector3 worldPosition;
    //float lastFireTime = 0.0f;
    public float gravity = 10.0f;
	Dictionary<int, float> colliderTriggered;

    bool pulsing = false;
    float lastGravityPulse = -0.05f;
    float pulsePeriods = 0.05f;
    float pulseDuration = 0.025f;

    // Use this for initialization
    void Start()
    {
        myMesh = transform.parent.GetComponent<MeshFilter>().mesh;
		colliderTriggered = new Dictionary<int, float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastGravityPulse + pulsePeriods)
        {
            lastGravityPulse = Time.time;
        }
        if (Time.time > lastGravityPulse && Time.time <= lastGravityPulse + pulseDuration)
        {
            pulsing = true;
        }else
        {
            pulsing = false;
            colliderTriggered.Clear();
        }
    }

    void OnTriggerStay(Collider other)
    {
        //get the root game object of the collider
        var collider = other.transform.root.gameObject;

        if (pulsing)
        {
            if (collider.tag == "Rocket" && !colliderTriggered.ContainsKey(other.GetHashCode()))
            {
                Vector3 otherCenter = other.transform.position;
                Vector3 resultant = (transform.TransformPoint(myMesh.bounds.center)) - otherCenter;
                Vector3 resultantWithGravity = resultant.normalized * gravity;
                collider.SendMessage("GravityPull", resultantWithGravity);

                colliderTriggered.Add(other.GetHashCode(), Time.time);
            }
        }

        /*
		bool found = false;
		if(colliderTriggered.ContainsKey(other.GetHashCode()))
		{
			lastFireTime = colliderTriggered[other.GetHashCode()];
			found = true;
		}
		
        
        if (collider.tag == "Rocket" && (!found || (found && Time.time > lastFireTime + 0.05)))
        {
            Vector3 otherCenter = other.transform.position;
            Vector3 resultant = (transform.TransformPoint(myMesh.bounds.center)) - otherCenter;
            Vector3 resultantWithGravity = resultant.normalized * gravity;
            collider.SendMessage("GravityPull", resultantWithGravity);
            			
			if(found)
			{
				colliderTriggered[other.GetHashCode()] = Time.time;
			}
			else
			{
				colliderTriggered.Add(other.GetHashCode(), Time.time);	
			}
        }*/
    }
}
