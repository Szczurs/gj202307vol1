using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int score;
    private float timePointer = 0f;
    private float minusPointCool = 3f;
    private void FixedUpdate()
    {
        if (timePointer >= minusPointCool)
        {
            score--;
            timePointer = 0f;
        }
        else
            timePointer += Time.deltaTime;
        scoreText.text = score.ToString();
    }
}
