using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    float HP = 100;
    float damage = 15;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0)
        {
            DestroyObject(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //get the root game object of the collider
        var collider = collision.collider.transform.root.gameObject;

        if (collider.tag == "Asteroid")
        {
            collider.SendMessage("crash", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void boom(float damage)
    {
        HP -= damage;
    }

    public void crash(float damage)
    {
        HP -= damage;
    }
}
