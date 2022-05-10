using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{
    public static GlassManager Instance;
    public GameObject player;
    public static bool isStart = true;
    public Animator glassAnimator;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
     void Start()
     {
         isStart = true;
     }

    public void GlassBroke()
    {
        glassAnimator.SetBool("isBroke", true);
    }
     
     public void GlassPassed(GameObject TemperedGlass)
     {

     }
}
