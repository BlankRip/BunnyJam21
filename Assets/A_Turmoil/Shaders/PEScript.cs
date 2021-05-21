using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[ExecuteInEditMode]
public class PEScript : MonoBehaviour
{
    public Material mat;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
