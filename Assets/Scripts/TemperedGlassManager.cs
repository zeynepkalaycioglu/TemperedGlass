using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperedGlassManager : MonoBehaviour
{
    public static TemperedGlassManager Instance;
    public List<SingleTemperedGlass> temperedGlasses;
    public Material[] material;
    
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
    public void Start()
    {
        StartCoroutine(DisplayGlassesAndActivateButtons());
    }

    public IEnumerator DisplayGlassesAndActivateButtons()
    {
        for (int i = 0; i < temperedGlasses.Count; i++)
        {
            StartCoroutine(temperedGlasses[i].GlassTime());
        }
        yield return new WaitForSeconds(3f);
        PlayerController.Instance.ActivateButton();
    }
    
}
