using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTemperedGlass : MonoBehaviour
{
   
    public Renderer rend;
    
    
    public bool isGlassShowing = true;

    
    // Start is called before the first frame update
    void Awake()
    {
    }

    public IEnumerator GlassTime()
    {
        yield return new WaitForEndOfFrame();
        rend = GetComponent<Renderer>();
        rend.enabled = true; 
        rend.sharedMaterial = TemperedGlassManager.Instance.material[0];

        isGlassShowing = true;
        
        yield return new WaitForSeconds(3f);
        GlassShowing();
    }

    public void GlassShowing()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = TemperedGlassManager.Instance.material[1];
        isGlassShowing = false;

    }
}
