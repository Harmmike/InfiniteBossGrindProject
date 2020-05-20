using System.Numerics;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float scrollSpeed;

    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.materials[0];

        var offset = mat.mainTextureOffset;

        offset.x += Time.deltaTime / scrollSpeed;
        offset.y += Time.deltaTime / scrollSpeed;

        mat.mainTextureOffset = offset;
    }
}
