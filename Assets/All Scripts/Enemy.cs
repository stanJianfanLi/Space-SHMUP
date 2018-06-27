using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 10f;     // The speed in m/s
    public float fireRate = 0.3f; // Seconds/shot (Unused)
    public float health = 10;
    public int score = 100; // Points earned for destroying this

    private BoundsCheck bndCheck;

    [Header("Set Dynamically")]
    public Bounds bounds; // The Bounds of this and its children
    public Vector3 boundsCenterOffset; // Dist of bounds.center from position

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        if (bndCheck != null && !bndCheck.offDown)
        {
            //Check to make sure it's gone off the bottom of the screen
            if (pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                // We're off the bottom, so destroy this GameObject
               // Destroy(gameObject);
            }
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO);
            Destroy(gameObject);
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }

    // This is a Property: A method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
}

