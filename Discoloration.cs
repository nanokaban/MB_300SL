using UnityEngine;
using System.Collections;

public class Discoloration : MonoBehaviour {
    public Material material_Interior;
    public Material material_Exterior;

    private Color[] allColors_Inerior;
    private Color[] allColors_Exterior;

    private Color red_Inerior;
    private Color turquoise_Inerior;
    private Color blue_Inerior;

    private Color blue_Exterior;
    private Color grey_Exterior;
    private Color red_Exterior;
    private Color beige_Exterior; 

    private int i = 0;
	private int j = 0;
	private Color currentColor_Inerior;
	private Color currentColor_Exterior;

    void Start () {
        red_Inerior = new Color(0.48f, 0f, 0f, 1f);
        turquoise_Inerior = new Color(0.047f, 0.39f, 0.39f, 1f);
        blue_Inerior = new Color(0f, 0.07f, 0.275f, 1f);

        blue_Exterior = new Color(0.016f, 0f, 0.133f, 1f);
        grey_Exterior = new Color(0.65f, 0.65f, 0.65f, 1f);
        red_Exterior = new Color(0.59f, 0f, 0f, 1f);
        beige_Exterior = new Color(1f, 0.9f, 0.72f, 1f);

		allColors_Inerior = new Color[] { Color.grey, Color.black, Color.white, turquoise_Inerior, blue_Inerior, red_Inerior};
		allColors_Exterior = new Color[] { blue_Exterior, red_Inerior, Color.black, grey_Exterior, Color.yellow, beige_Exterior, Color.white, turquoise_Inerior};

		currentColor_Inerior = material_Interior.color;
		currentColor_Exterior = material_Exterior.color;
    }

	void Update () {
		Discoloration_Inerior ();
		Discoloration_Exterior ();
    }

	void Discoloration_Inerior(){
		currentColor_Inerior = material_Interior.color;
		if (Input.GetKeyDown(KeyCode.R))
		{
			currentColor_Inerior = allColors_Inerior[i];
			i++;
		}
		if (i > allColors_Inerior.Length-1)
		{
			i = 0;
		}
		material_Interior.color = Color.Lerp(currentColor_Inerior, allColors_Inerior[i], Time.smoothDeltaTime);
	}

	void Discoloration_Exterior(){
		currentColor_Exterior = material_Exterior.color;
		if (Input.GetKeyDown(KeyCode.T))
		{
			currentColor_Exterior = allColors_Exterior[j];
			j++;
		}
		if (j > allColors_Exterior.Length-1)
		{
			j = 0;
		}
		material_Exterior.color = Color.Lerp(currentColor_Exterior, allColors_Exterior[j], Time.smoothDeltaTime);
	}
	
}
