using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFollowPlayer : MonoBehaviour
{
    public Transform target;

    public ItemObject item;

    public float min = 5;
    public float max = 10;


    public TrailRenderer trail;

    Vector3 vel = Vector3.zero;
    bool isFollowing = false;



    public void startFollowing()
    {
        isFollowing = true;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        Debug.Log(item.name);
        trail.startColor = item.color;
        startFollowing();
        Debug.Log(target.name);
        trail = GetComponent<TrailRenderer>();
        Destroy(gameObject,5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("coll");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel, Time.deltaTime * Random.Range(min,max) );
        }
        
    }
}
