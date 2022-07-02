using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPhysics : MonoBehaviour
{
    public float speed = 2f;

    public Transform StartPoint;
    public Transform EndPoint;
    public SliderJoint2D sliderJoint;
    public SpriteRenderer endPointSprite;
    float dist;
    bool forward = true;
    JointTranslationLimits2D limits;
    JointMotor2D motor;

    // Start is called before the first frame update
    void Awake()
    {
        dist = Vector3.Distance(StartPoint.position, EndPoint.position);
        JointTranslationLimits2D limits = sliderJoint.limits;
        limits.min = 0.1f;
        limits.max = dist - 0.1f;
        sliderJoint.limits = limits;
        JointMotor2D motor = sliderJoint.motor;
        endPointSprite.enabled = false;
    }

    void Update()
    {
        if (forward)
        {
            if (sliderJoint.jointTranslation < 1f)
            {
                motor.motorSpeed = -sliderJoint.jointTranslation*speed;
                
            }
            else if (sliderJoint.jointTranslation > (dist - 1f))
            {
                motor.motorSpeed = -(dist - sliderJoint.jointTranslation)*speed;
                
            }
            else
            {
                motor.motorSpeed = -speed;
                
            }
        }
 
        if (!forward)
        {
            if (sliderJoint.jointTranslation > (dist - 1f))
            {
                motor.motorSpeed = (dist - sliderJoint.jointTranslation)*speed;
                
            }
            else if (sliderJoint.jointTranslation < 1f)
            {
                motor.motorSpeed = sliderJoint.jointTranslation*speed;
                
            }
            else
            {
                motor.motorSpeed = speed;
                
            }
        }


        if (sliderJoint.jointTranslation < 0.1f)
        {
            forward = false;
        }

        if (sliderJoint.jointTranslation > (dist - 0.1f))
        {
            forward = true;
        }

        motor.maxMotorTorque = speed*50;
        sliderJoint.motor = motor;
    }
}
