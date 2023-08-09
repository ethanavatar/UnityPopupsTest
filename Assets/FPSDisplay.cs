using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public int avgFrameRate;
    public TMP_Text fpsText;

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        fpsText.text = avgFrameRate.ToString() + " FPS";
    }
}
