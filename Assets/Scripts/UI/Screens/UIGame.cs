using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : BaseUIScreen
{
    public Image rA;
    public Image rB;
    public Image rC;

    public TextMeshProUGUI tA;
    public TextMeshProUGUI tB;
    public TextMeshProUGUI tC;

    public void SetResourceDisplay(int resourceIndex, int value)
    {
        switch(resourceIndex)
        {
            case 0:
                tA.text = value.ToString();
                break;
            case 1:
                tB.text = value.ToString();
                break;
            case 2:
                tC.text = value.ToString();
                break;
        }
    }

}
