using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is based off a tutorial at https://docs.unity3d.com/Manual/WheelColliderTutorial.html
 *  I followed this tutorial to make my car object
 *  I added comments, explaining what it does
 */

public class CarController2 : MonoBehaviour
{
    public List<AxleInfo> axleInfos; //infomation about the axises on the car
    public float maxMotorTorque; //max torque the motor can apply to the wheel
    public float maxSteeringAngle; // max steer angle the wheel can have

    // Fixed Update for Physics
    void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical"); //vertical input controls motor
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal"); //horizontal input control steering

        //loop through all axles 
        foreach (AxleInfo info in axleInfos)
        {
            if (info.steering)
            {
                info.leftWheel.steerAngle = steering;
                info.rightWheel.steerAngle = steering;
            }

            if (info.motor)
            {
                info.leftWheel.motorTorque = motor;
                info.rightWheel.motorTorque = motor;
            }
        }
    }
}

//this class saves the info for one axis on a car
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; //is this axis attachted to the motor
    public bool steering; //does this axis applie steer angles
}
