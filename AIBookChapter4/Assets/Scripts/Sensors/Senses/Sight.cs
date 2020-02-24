﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : Sense
{
    public int FieldOfView = 45;
    public int ViewDistance = 100;

    private Transform playerTrans;
    private Vector3 rayDirection;

    protected override void Initialize()
    {
        //Find player position
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Update is called once per frame
    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        //Detect perspective sense if within the detection rate
        if (elapsedTime >= detectionRate) DetectAspect();
    }

    //Detect perspective field of view for the AI Character
    void DetectAspect()
    {
        RaycastHit hit;

        //direction from current position to player posiition
        rayDirection = playerTrans.position - transform.position;

        //Check the angle between the AI characters forward vector and the dirrection vector between pllayer and AI
        if((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
        {
            //Detect if player is withhin the field of view
            if(Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if(aspect != null)
                {
                    //Check the aspect
                    if(aspect.aspectName == aspectName)
                    {
                        print("Enemy Detected");
                    }
                }
            }
        }
    }
    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if (!Application.isEditor || playerTrans == null)
            return;

        Debug.DrawLine(transform.position, playerTrans.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * ViewDistance);

        //Approximate perspective visualization
        Vector3 leftRayPoint = Quaternion.Euler(0, FieldOfView * 0.5f, 0) * frontRayPoint;

        Vector3 rightRayPoint = Quaternion.Euler(0, -FieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}
