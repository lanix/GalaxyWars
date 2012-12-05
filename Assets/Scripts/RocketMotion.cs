using UnityEngine;
using System.Collections;

public class RocketMotion : MonoBehaviour
{

    Vector3 DirectionVector = new Vector3(0.0f, 1.0f, 0.0f);
    float BirthTime = 0;
    public float Life = 5;

    public float velocity = 10;
    public float missileDamage = 10;
    public float explosionForce = 10;

    // Use this for initialization
    void Start()
    {
        DirectionVector = DirectionVector * velocity;
        BirthTime = Time.time;
        transform.rigidbody.AddForce(DirectionVector, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = DirectionVector;
        transform.rigidbody.AddForce(DirectionVector.normalized * (velocity * Time.deltaTime), ForceMode.Impulse);

        if (Time.time - BirthTime >= Life)
        {
            Explode();
        }
    }

    void Explode()
    {
        DestroyObject(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        //For some reason, sometimes the collider is the gravity object, wtf?
        //get the root game object of the collider
        var collider = collision.collider.transform.root.gameObject;

        var contact = collision.contacts[0];

        if (collider.tag == "Asteroid")
        {
            collider.SendMessage("boom", missileDamage, SendMessageOptions.DontRequireReceiver);
            collider.rigidbody.AddForce(contact.normal * -1 * explosionForce, ForceMode.Impulse);
        }

        Explode();
    }

    public void SetInitialAngle(float rotationAngle)
    {
        if (rotationAngle != 0)
        {
            var rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
            DirectionVector = rotation * DirectionVector;
        }
    }

    public void GravityPull(Vector3 direction)
    {
        transform.rigidbody.AddForce(direction * Time.deltaTime, ForceMode.Impulse);

        DirectionVector.x = DirectionVector.x + (direction.x * Time.deltaTime);
        DirectionVector.y = DirectionVector.y + (direction.y * Time.deltaTime);
    }

}
