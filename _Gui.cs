using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _Gui : MonoBehaviour {
    CarController carController;
    Scrollbar scroll;
    Text txt1;
    Text txt2;
    Button but_rest;

    private int [] _rmp;


    void Start () {
        carController = GameObject.Find("MB300SL").GetComponent<CarController>();
        scroll = GetComponentInChildren<Scrollbar>(); 
        txt1 = GameObject.Find("Text_LeftRMP").GetComponent<Text>();
        txt2 = GameObject.Find("Text_RightRMP").GetComponent<Text>();
        but_rest = GameObject.Find("Restart").GetComponent<Button>();
    }
	
	
	void Update () {
        Clearance();

        _rmp = carController.Getrmp();
        txt1.text = System.Convert.ToString(_rmp[2]) + "\n\n\n" + System.Convert.ToString(_rmp[0]);
        txt2.text = System.Convert.ToString(_rmp[3]) + "\n\n\n" + System.Convert.ToString(_rmp[1]);
    }

    void Clearance()
    {
        carController.Suspension(scroll.value);
        if (Input.GetKey(KeyCode.P))
        {
            scroll.value += 0.1f;
            carController.Suspension(Mathf.Clamp(scroll.value, 0f, 1f));
        }
        
        if (Input.GetKey(KeyCode.O))
        {
            scroll.value -= 0.1f;
            carController.Suspension(Mathf.Clamp(scroll.value, 0f, 1f));
        }
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
   
    public void ExitGame()
    {
        Application.Quit();
    }
}
