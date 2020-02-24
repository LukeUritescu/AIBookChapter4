﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    private Vector3 tarPos;

    private float movementSpeed = 5.0f;
    private float rotSpeed = 2.0f;
    private float minX, maxX, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        minX = -45.0f;
        maxX = 45.0f;

        minZ = -45.0f;
        maxZ = 45.0f;

        //Get Wandder Position
        GetNextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we're near the destination position
        if (Vector3.Distance(tarPos, transform.position) <= 5.0f)
            GetNextPosition();
        //Set up quaternion for roation  toward destination
        Quaternion tarRot = Quaternion.LookRotation(tarPos - transform.position);

        //Upddate rotation and translation
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }

    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }
}
