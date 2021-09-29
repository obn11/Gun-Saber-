using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    enum Mechanics { Simple, Laser, Bullet }
    [SerializeField] Mechanics mechanics;

    public List<AudioClip> sounds;
    public GameObject barrelEnd;
    public SoundManager soundManager;

    public List<MeshRenderer> gunComponents;

    public GameObject comboText;
    public GameObject scoreText;
    public GameObject sword;
    new AudioSource audio;
    LineRenderer trail;

    public int ammoMax = 8;
    public int ammo;
    bool reloading = false;
    public bool reloadRequired = true;
    
 
    void Start()
    {
        ammo = ammoMax;
        trail = GetComponent<LineRenderer>();
        trail.startWidth = 0.01f;
        trail.endWidth = 0f;
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Shoot
        if (Input.GetMouseButtonUp(0) && !PauseMenu.paused)
        {
            switch (mechanics)
            {
                case Mechanics.Simple:
                    DisplayGun(false);
                    ShootSimple();
                    break;
                case Mechanics.Laser:
                    DisplayGun(true);
                    if (ammo > 0 && !reloading) 
                    {
                        ShootLaser();
                    }
                    break;
            }
        }

        //Aim down sight
        if (Input.GetMouseButtonDown(1) && !reloading)
        { 
            transform.Translate(new Vector3(0, 0.12f, 0.42f));
        }

        //Stop Aiming
        if (Input.GetMouseButtonUp(1) && !reloading && transform.localPosition != new Vector3(0.42f, -0.6f, 1.09f))
        {
            transform.localPosition = new Vector3(0.42f, -0.6f, 1.09f);
        }

        //Reload
        if (Input.GetKeyDown(KeyCode.R) && ammo < ammoMax)
        {
            StartCoroutine(Reload());
        }

        //Swap to Katana
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            QuickSwap();
        }

    }

    //Swaps to Katana
    private void QuickSwap()
    {
        sword.SetActive(true);
        gameObject.SetActive(false);
    }

    //Reloads Gun
    IEnumerator Reload()
    {
        reloading = true;
        transform.localPosition = new Vector3(0.42f, -0.6f, 1.09f);
        yield return new WaitForSeconds(soundManager.secPerBeat);
        reloading = false;
        ammo = ammoMax;
    }

    //A basic shot, not used in game
    void ShootSimple()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            DestroyBeat(hit);
        }
        audio.PlayOneShot(sounds[0]);
    }

    //Primary Shot controller, checks target and produces laser
    void ShootLaser()
    {
        trail.startWidth = 0.05f;
        trail.SetPosition(0, barrelEnd.transform.position);
        trail.SetPosition(1, barrelEnd.transform.position + barrelEnd.transform.forward * 5f);
        RaycastHit hit;
        if (Physics.Raycast(barrelEnd.transform.position, barrelEnd.transform.forward, out hit, 5f))
        {
            DestroyBeat(hit);

        } else
        {
            comboText.GetComponent<ComboDisplay>().SetCombo(0);
            GameObject.Find("Miss").GetComponent<AudioSource>().PlayOneShot(sounds[4]);
        }
        if (reloadRequired) { ammo -= 1; }
        audio.PlayOneShot(sounds[2]);
        StartCoroutine("Fade");
    }

    //Removes laser trail
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.1f);
        trail.startWidth = 0f;
    }

    //Renders or derenders gun for the sake of shoot simple
    void DisplayGun(bool b)
    {
        foreach(MeshRenderer m in gunComponents)
        {
            m.enabled = b;
        }
    }

    //Checks target and destorys if target is a beat
    void DestroyBeat(RaycastHit hit)
    {
        DestroyBeat b = hit.collider.gameObject.GetComponent<DestroyBeat>();
        if (b != null)
        {
            b.Pop();
            comboText.GetComponent<ComboDisplay>().AddToCombo(1);
            scoreText.GetComponent<ScoreDisplay>().AddScore();
            Beat.deathCounter = 0;
        }
    }
}
