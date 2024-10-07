using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMover : MonoBehaviour
{

    // contral points
    public Transform point0;
    public Transform point1;
    public Transform point2;

    // Curve motion speed
    public float speed = 1.0f;

    // Internal timer
    private float t = 0f;

    void Update()
    {
        // Increase the value of t to control the movement speed
        t += Time.deltaTime * speed;

        // Make sure the value of t cycles between 0 and 1
        if (t > 1f)
        {
            t = 0f;
        }

        // Calculate points on a Bezier curve
        Vector3 position = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);

        // Update object position
        transform.position = position;
    }

    // Calculate points on a quadratic Bezier curve
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0; // (1-t)^2 * p0
        point += 2 * u * t * p1; // 2 * (1-t) * t * p1
        point += tt * p2;        // t^2 * p2

        return point;
    }


}
