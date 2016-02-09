using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioClip startup;
    public AudioClip onidle1;
    CarController carController;
    AudioSource _audio;

    public float timer_start;
    private bool bo=true;

    void Start () {
        carController = gameObject.GetComponent<CarController>();
        _audio = GetComponent<AudioSource>();

        _audio.clip = startup;
        _audio.pitch = 0.5f;
        _audio.Play();

        timer_start = startup.length;
    }
    
	void Update () {
        if (bo)
        {
            timer_start -= Time.deltaTime;
        }
        
        if(timer_start <= 0)
        {            

            if (bo) {
                _audio.clip = onidle1;
                _audio.loop = true;
                _audio.Play();
                bo = false;
            }

            float [] b = carController.Getrmp_float();
            _audio.pitch = Mathf.Lerp(0.5f, 3, Mathf.Abs(b[3]+b[2]+b[1]+b[0])/1600);


        }
    }
}
