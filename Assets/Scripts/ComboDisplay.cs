using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : MonoBehaviour
{
    public int combo = 0;
    public Text comboText;

    int[] mults = new int[] { 1, 2, 4, 8 };
    public int multiplier = 1;
    int prevMult = 1;
    Text multText;

    // Start is called before the first frame update
    void Start()
    {
        multText = GameObject.Find("Mult").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        multiplier = mults[System.Math.Min(combo / 8, mults.Length-1)];
        if (prevMult != multiplier)
        {
            StartCoroutine("increase");
        }
        prevMult = multiplier;
        comboText.text = "COMBO\n" + combo;
        multText.text = "X" + multiplier;
        
    }

    //Makes combo stand out when it changes
    IEnumerator increase()
    {
        multText.fontSize += 20;
        yield return new WaitForSecondsRealtime(1f);
        multText.fontSize -= 20;
    }

    //Increment combo 
    public void AddToCombo(int val)
    {
        combo += val;
    }

    //Sets at specific number (Usually 0)
    public void SetCombo(int val)
    {
        combo = val;
    }
}
