using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class PatrolerTrailColision : MonoBehaviour
{
    TrailRenderer myTrail;
    EdgeCollider2D myCollider;

    static List<EdgeCollider2D> unusedColliders = new List<EdgeCollider2D>();

    void Awake()
    {
        myTrail = this.GetComponent<TrailRenderer>();
        myCollider = GetValidCollider();
        myCollider.isTrigger = true;
        myCollider.gameObject.AddComponent<PatrolTrailDetect>();
        myCollider.gameObject.GetComponent<PatrolTrailDetect>()._patroler = this.GetComponentInParent<PatrolMovement>();
    }

    void Update()
    {
        SetColliderPointsFromTrail(myTrail, myCollider);
    }


    //Gets from unused pool or creates one if none in pool
    EdgeCollider2D GetValidCollider()
    {
        EdgeCollider2D validCollider;
        if (unusedColliders.Count > 0)
        {
            validCollider = unusedColliders[0];
            validCollider.enabled = true;
            unusedColliders.RemoveAt(0);
        }
        else
        {
            validCollider = new GameObject("TrailCollider", typeof(EdgeCollider2D)).GetComponent<EdgeCollider2D>();
        }
        return validCollider;
    }

    void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        //avoid having default points at (-.5,0),(.5,0)
        if (trail.positionCount == 0)
        {
            points.Add(transform.position);
            points.Add(transform.position);
        }
        else for (int position = 0; position < trail.positionCount; position++)
            {
                Vector2 newpos = new Vector2(trail.GetPosition(position).x, trail.GetPosition(position).y);
                points.Add(newpos);
            }
        collider.SetPoints(points);
    }

    void OnDestroy()
    {
        if (myCollider != null)
        {
            myCollider.enabled = false;
            unusedColliders.Add(myCollider);
        }
    }
}
