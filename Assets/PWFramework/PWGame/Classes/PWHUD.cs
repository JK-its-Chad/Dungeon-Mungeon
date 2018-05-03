using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PWHUD : FrameworkHUD
{
    public int PlayerNumber = 0;
    public int Shields = 0;
    public int Energy = 0;
    public int Bullets = 0;
    public int Key = 0;



    public Text PlayerNumberField;
    public Text ShieldsField;
    public Slider ShieldSlider;
    public Text EnergyField;
    public Text BulletField;
    public Image KeyLevel;

    public GameObject ActivePanel;
    public GameObject SpectatePanel;

    // Update is called once per frame
    public override void UpdateHUD()
    {
        if (PlayerNumberField)
        {
            PlayerNumberField.text = "Player " + PlayerNumber; 
        }
        if (ShieldsField)
        {
            ShieldsField.text = "Health: " + Shields;
        }
        if (EnergyField)
        {
            EnergyField.text = "Mana: " + Energy;
        }
        if (ShieldSlider)
        {
            ShieldSlider.value = Shields; 
        }
        if(BulletField)
        {
            BulletField.text = "Bullets: " + Bullets;
        }
        if(KeyLevel)
        {
            switch(Key)
            {
                case 0:
                    KeyLevel.color = new Color32(255, 00, 00, 255);
                    break;
                case 1:
                    KeyLevel.color = new Color32(255, 100, 00, 255);
                    break;
                case 2:
                    KeyLevel.color = new Color32(255, 237, 00, 255);
                    break;
                case 3:
                    KeyLevel.color = new Color32(135, 255, 00, 255);
                    break;
                case 4:
                    KeyLevel.color = new Color32(00, 255, 44, 255);
                    break;
                case 5:
                    KeyLevel.color = new Color32(00, 255, 128, 255);
                    break;
                case 6:
                    KeyLevel.color = new Color32(00, 223, 255, 255);
                    break;
                case 7:
                    KeyLevel.color = new Color32(00, 86, 255, 255);
                    break;
            }
        }
    }
}
