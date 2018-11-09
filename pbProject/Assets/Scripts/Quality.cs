using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quality : MonoBehaviour {


    public Canvas Graphic;
    public Button Low;
    public Button Medium;
    public Button High;
    public Button Ultra;
    public Text qualityValue;

	void Start () {
        Low = Low.GetComponent<Button>();
        Medium = Low.GetComponent<Button>();
        High = Low.GetComponent<Button>();
        Ultra = Low.GetComponent<Button>();
        Graphic = Graphic.GetComponent<Canvas>();
        detectYourQuality();
    }
	
	
    public void detectYourQuality()
    {
        int qualityLevel = QualitySettings.GetQualityLevel();
        switch(qualityLevel)
        {
            case 0:
                qualityValue.text = "Very Low";
                break;
            case 1:qualityValue.text = "Low";
                break;
            case 2:
                qualityValue.text = "Medium";
                break;
            case 3:
                qualityValue.text = "High";
                break;
            case 4:
                qualityValue.text = "Very High";
                break;
            case 5:
                qualityValue.text = "Ultra";
                break;
        }
    }

    public void lowQuality()
    {
        QualitySettings.SetQualityLevel(1);
        qualityValue.text = "Low";
    }

    public void mediumQuality()
    {
        QualitySettings.SetQualityLevel(2);
        qualityValue.text = "Medium";
    }

    public void highQuality()
    {
        QualitySettings.SetQualityLevel(3);
        qualityValue.text = "High";
    }

    public void ultraQuality()
    {
        QualitySettings.SetQualityLevel(5);
        qualityValue.text = "Ultra";
    }

    
}
