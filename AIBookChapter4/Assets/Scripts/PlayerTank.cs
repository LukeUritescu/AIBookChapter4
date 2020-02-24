using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    public Transform targetTransform;
    private float movementSpeed, rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 10.0f;
        rotSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetTransform.position) < 5.0f)
            return;
        //Calculate direction vector from current position to target position
        Vector3 tarPos = targetTransform.position;
        tarPos.y = targetTransform.position.y;
        Vector3 dirRot = tarPos - transform.position;


        //Build a quaterion for this new rotation vector using lookrotation method
        Quaternion tarRot = Quaternion.LookRotation(dirRot);

        //Move and rotate with iterpolation
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
        
    }
}
