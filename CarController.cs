using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public WheelCollider[] WColForward;
    public WheelCollider[] WColBack;

    public Transform[] wheelsF; 
    public Transform[] wheelsB;

    float wheelOffset = 0f; 
    public float wheelRadius = 0.88f;

    public float maxSteer = 30f; 
    public float maxAccel = 25f; 
    public float breakSpeed = 600f;
    public Transform COM;
   
    public class WheelData
    { 
        public Transform wheelTransform; 
        public WheelCollider col; 
        public Vector3 wheelStartPos; 
        public float rotation = 0.0f;  
    }


    protected WheelData[] wheels;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = COM.localPosition;

        wheels = new WheelData[WColForward.Length + WColBack.Length]; 

        for (int i = 0; i < WColForward.Length; i++)
        { 
            wheels[i] = SetupWheels(wheelsF[i], WColForward[i]); 
        }

        for (int i = 0; i < WColBack.Length; i++)
        { 
            wheels[i + WColForward.Length] = SetupWheels(wheelsB[i], WColBack[i]); 
        }
  
    }

    private WheelData SetupWheels(Transform wheel, WheelCollider col)
    { 
        WheelData result = new WheelData();

        result.wheelTransform = wheel; 
        result.col = col; 
        result.wheelStartPos = wheel.transform.localPosition; 

        return result; 
    }

    void FixedUpdate()
    {
        float accel = 0;
        float steer = 0;

        accel = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        
        CarMove(accel, steer);
        UpdateWheels(); 
                
        if (Input.GetButton("Jump"))
        {
            WColBack[0].brakeTorque = breakSpeed;
            WColBack[1].brakeTorque = breakSpeed;
        }
        else
        {
            WColBack[0].brakeTorque = 0;
            WColBack[1].brakeTorque = 0;
        }
           
    }
    
    private void UpdateWheels()
    { 
        float delta = Time.fixedDeltaTime; 

        foreach (WheelData w in wheels)
        { 
            WheelHit hit;        
            Vector3 lp = w.wheelTransform.localPosition; 
            if (w.col.GetGroundHit(out hit))
            { 
                lp.y -= Vector3.Dot(w.wheelTransform.position - hit.point, transform.up) - wheelRadius ; 
            }
            else
            { 
                lp.y = w.wheelStartPos.y - wheelOffset; 
            }
            w.wheelTransform.localPosition = lp;
                          
            w.rotation = Mathf.Repeat(w.rotation + delta * w.col.rpm * 360.0f / 60.0f, 360.0f); 
            w.wheelTransform.localRotation = Quaternion.Euler(-w.rotation, w.col.steerAngle+180f, 0.0f); 
        }

    }

    private void CarMove(float accel, float steer)
    {
        foreach (WheelCollider col in WColForward)
        {
            col.steerAngle = steer * maxSteer; 
        }

        if (accel > 0)
        {
            foreach (WheelCollider col in WColBack)
            {            
                col.motorTorque = accel * maxAccel; 
            }
            foreach (WheelCollider col in WColForward)
            {
                col.motorTorque = accel * maxAccel; 
            }
        }
        else if ( accel < 0)
        {
            foreach (WheelCollider col in WColBack)
            {
                col.motorTorque = accel * maxAccel/2; 
            }
            foreach (WheelCollider col in WColForward)
            {
                col.motorTorque = accel * maxAccel/2; 
            }
        }
        else {
            foreach (WheelCollider col in WColBack)
            {
                col.motorTorque = accel * 0; 
            }
            foreach (WheelCollider col in WColForward)
            {
                col.motorTorque = accel * 0; 
            }
        }
    }

    public int[] Getrmp()
    {   
        int[] i = { Mathf.RoundToInt(WColBack[0].rpm), Mathf.RoundToInt(WColBack[1].rpm), Mathf.RoundToInt(WColForward[0].rpm), Mathf.RoundToInt(WColForward[1].rpm)};
        return i;
    }

	public float[] Getrmp_float()
	{   
		float[] i = { Mathf.Round(WColBack[0].rpm), Mathf.Round(WColBack[1].rpm), Mathf.Round(WColForward[0].rpm), Mathf.Round(WColForward[1].rpm)};
		return i;
	}


    public void Suspension(float _targetPosition)
    {
        foreach (WheelCollider col in WColForward)
        {
            JointSpring js = col.suspensionSpring;
            js.targetPosition = _targetPosition;
            col.suspensionSpring = js;
        }
        foreach (WheelCollider col in WColBack)
        {
            JointSpring js = col.suspensionSpring;
            js.targetPosition = _targetPosition;
            col.suspensionSpring = js;
        }

    }

}
