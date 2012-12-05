using UnityEngine;
using System.Collections;

public class MainControls : MonoBehaviour
{
    public Vector3 DirectionVector;
    public float Angle = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            Angle += 20.0f * Time.deltaTime;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            Angle += -20.0f * Time.deltaTime;
        }

        if (Angle >= 360 || Angle <= -360)
        {
            Angle = 0;
        }

        //var rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
        //this.transform.rotation = rotation;

        this.transform.localEulerAngles = new Vector3(0, 0, Angle);
        
        if (Input.GetButtonUp("Fire1"))
        {
            var newRocket = (GameObject)Instantiate(Resources.Load("Rocket"), this.transform.position, Quaternion.identity);

            newRocket.SendMessage("SetInitialAngle", Angle, SendMessageOptions.RequireReceiver);
        }
    }
}
