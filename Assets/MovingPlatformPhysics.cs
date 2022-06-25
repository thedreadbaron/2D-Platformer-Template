using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPhysics : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public SliderJoint2D forwardSlider;
    public SliderJoint2D reverseSlider;
    public SpriteRenderer endPointSprite;
    float dist;
    bool forward;
    
    JointTranslationLimits2D limits;
    JointMotor2D forwardMotor;
    JointMotor2D reverseMotor;

    // Start is called before the first frame update
    void Awake()
    {
        dist = Vector3.Distance(StartPoint.position, EndPoint.position);
        JointTranslationLimits2D limits = forwardSlider.limits;
        limits.min = 0.1f;
        limits.max = dist - 0.1f;
        forwardSlider.limits = limits;
        reverseSlider.limits = limits;
        JointMotor2D forwardMotor = forwardSlider.motor;
        JointMotor2D reverseMotor = reverseSlider.motor;
        endPointSprite.enabled = false;
    }

    void Update()
    {
        if (forward)
        {
            if (forwardSlider.jointTranslation < 2f)
            {
                forwardMotor.motorSpeed = -forwardSlider.jointTranslation;
                forwardMotor.maxMotorTorque = 100;
            }
            else if (forwardSlider.jointTranslation > (dist - 2f))
            {
                forwardMotor.motorSpeed = -(dist - forwardSlider.jointTranslation);
                forwardMotor.maxMotorTorque = 100;
            }
            else
            {
                forwardMotor.motorSpeed = -2;
                forwardMotor.maxMotorTorque = 100;
            }
        }
 
        if (!forward)
        {
            if (reverseSlider.jointTranslation > (dist - 2f))
            {
                reverseMotor.motorSpeed = dist - forwardSlider.jointTranslation;
                reverseMotor.maxMotorTorque = 100;
            }
            else if (reverseSlider.jointTranslation < 2f)
            {
                reverseMotor.motorSpeed = forwardSlider.jointTranslation;
                reverseMotor.maxMotorTorque = 100;
            }
            else
            {
                reverseMotor.motorSpeed = 2;
                reverseMotor.maxMotorTorque = 100;
            }
        }


        if (forwardSlider.jointTranslation < 0.2f)
        {
            forward = false;
            forwardMotor.maxMotorTorque = 0;
            reverseMotor.maxMotorTorque = 100;
        }

        if (reverseSlider.jointTranslation > (dist - 0.2f))
        {
            forward = true;
            reverseMotor.maxMotorTorque = 0;
            forwardMotor.maxMotorTorque = 100;
        }

        forwardSlider.motor = forwardMotor;
        reverseSlider.motor = reverseMotor;
    }
}
