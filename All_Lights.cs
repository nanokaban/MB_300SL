using UnityEngine;
using System.Collections;

public class All_Lights : MonoBehaviour {
    CarController carController;
    public Light [] stop_signals;
    int [] _rmp;
    
    public Light[] turn_signals;
    float val = 2;
    bool turn_ON;

    void Start () {
        carController = GameObject.Find("MB300SL").GetComponent<CarController>();
    }
	
	void Update () {
        _rmp = carController.Getrmp();
        Stop_signals();
        Turn_signals();

    }

    void Stop_signals()
    {
        if ((_rmp[2]> 0 && Input.GetKey(KeyCode.S)) || (_rmp[2] < 0 && Input.GetKey(KeyCode.W)))
        {
            stop_signals[0].intensity = 6.5f;
            stop_signals[1].intensity = 6.5f;
        }
        else
        {
            stop_signals[0].intensity = 1.5f;
            stop_signals[1].intensity = 1.5f;
        }
    }

    void Turn_signals()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (turn_ON)
            {
                turn_ON = false;
                for (int i = 0; i < turn_signals.Length; i++)
                {
                    turn_signals[i].intensity = 0;
                }
            }
            else
            {
                turn_ON = true;
            }

        }

        if (turn_ON)
        {
            val -= Time.deltaTime;
            if (val < 1 && val > 0)
            {
                for (int i = 0; i < turn_signals.Length; i++)
                {
                    turn_signals[i].intensity = 5f;
                }

            }
            else if (val > 1)
            {
                for (int i = 0; i < turn_signals.Length; i++)
                {
                    turn_signals[i].intensity = 0;
                }
            }

            else
            {
                val = 2;
            }
        }
    }
}
