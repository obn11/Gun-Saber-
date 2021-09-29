using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class AmmoDisplay : MonoBehaviour
{
    int ammo;
    public GunController gun;
    public Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Updates current ammo display
    void Update()
    {
        ammo = gun.ammo;
        ammoText.text = "AMMO\n" + StringExtensions.Repeat("| ", ammo);
    }
}

// Enables repeating string for easy scaliablity
public static class StringExtensions
{
    public static string Repeat(this string s, int n)
        => new StringBuilder(s.Length * n).Insert(0, s, n).ToString();
}