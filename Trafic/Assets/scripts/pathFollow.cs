using System.Drawing;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows;

public class pathFollow : MonoBehaviour
{
    public List<Transform> Point;

    public List<Transform> ControlPoint;

    public GameObject FirstPoint;

    Transform startPoint;
    Transform controlPoint;
    Transform endPoint;

    public bool Break;

    int point;
    public float speed = 5f;
    private float t = 0f;

    Vector3 position;

    void Start()
    {
        Break=false;
        Point.Add(FirstPoint.transform);
        get_next_point(FirstPoint);

        point = 0;
        startPoint = Point[point];
        controlPoint = ControlPoint[point];
        endPoint = Point[point + 1];

        //float angle = Mathf.Atan2(FirstPoint.transform.rotation.y, FirstPoint.transform.rotation.x) * Mathf.Rad2Deg;
        //Debug.Log(name);
    }

    

    void get_next_point(GameObject point)
    {
        nextPoint point_transform = point.GetComponent<nextPoint>();
        //Debug.Log(point_transform);
        if (point_transform != null)
        {
            (GameObject nextp, Transform nextc) = point_transform.get_next_point();

            Point.Add(nextp.transform);
            ControlPoint.Add(nextc);
            get_next_point(nextp);
        }
        
    }

    void Update()
    {
        //if (UnityEngine.Input.GetKey(KeyCode.Space))
        if (Break==false)
        MoveAlongCurve();
        
        if (position == endPoint.position)
        {
            //Debug.Log(Point.Length);
            if (point < Point.Count - 2)
            {
                point += 1;
                startPoint = Point[point];
                controlPoint = ControlPoint[point];
                endPoint = Point[point + 1];

                t = 0;

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void MoveAlongCurve()
    {
        t += speed * (Time.deltaTime);

        if (t > 1f)
        {
            t = 1f;
        }

        position = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint.position, endPoint.position);
        transform.position = position;

        // Update rotation based on the tangent vector
        Vector3 tangent = CalculateQuadraticBezierTangent(t, startPoint.position, controlPoint.position, endPoint.position);
        float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, angle +FirstPoint.transform.eulerAngles.z);


    }

    // Quadratic Bezier curve calculation
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }

    // Quadratic Bezier tangent calculation
    Vector3 CalculateQuadraticBezierTangent(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;

        Vector3 tangent = 2 * u * (p1 - p0) + 2 * t * (p2 - p1);
        tangent.Normalize();

        return tangent;
    }
}
