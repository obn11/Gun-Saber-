using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public GameObject combo;
    public Text scoreText;
    public int score = 0;
    int multi = 1;
    int PT = 10;
    

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        multi = combo.GetComponent<ComboDisplay>().multiplier;
        scoreText.text = "SCORE\n" + score;
    }

    public void AddScore()
    {
        score += (PT * multi);
    }
}
