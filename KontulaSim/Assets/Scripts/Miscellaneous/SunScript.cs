using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    public Material shadowMaterial;

    private float skewX = 0f;

    // Start is called before the first frame update
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

        skewX = Mathf.Abs(shadowMaterial.GetFloat("_SkewX"));

        shadowMaterial.SetFloat("_SkewX", -transform.position.x);
        //shadowMaterial.SetFloat("_ScaleHeight", skewX);
    }
}
