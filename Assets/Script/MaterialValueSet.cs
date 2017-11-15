using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialValueSet : MonoBehaviour
{
    public Material mat;
    public float rot;
	void Update ()
    {
        rot += 0.1f;
        
        rot %= 360;

        mat.SetFloat("_Rotation", rot);
    }
}
