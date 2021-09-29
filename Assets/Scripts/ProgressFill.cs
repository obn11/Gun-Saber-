using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressFill : MonoBehaviour
{
    ComboDisplay combo;
    Image bar;
    // Start is called before the first frame update
    void Start()
    {
        combo = GameObject.Find("Combo").GetComponent<ComboDisplay>();
        bar = GameObject.Find("Progress Fill").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = (combo.combo % 8) / 8.0f;
    }
}
