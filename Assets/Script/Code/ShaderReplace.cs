
using UnityEngine;
using UnityEngine.UI;
public class ShaderReplace : MonoBehaviour
{
    public Renderer[] renderArray;
    public Image[] imageArray;

    void Awake()
    {
        for (int i = 0; renderArray != null && i < renderArray.Length; i++)
        {
            Renderer render = renderArray[i];
            render.sharedMaterial.shader = ShaderArray.GetShader(render.sharedMaterial.shader.name);
        }

        for (int i = 0; imageArray != null && i < imageArray.Length; i++)
        {
            Image image = imageArray[i];
            image.material.shader = ShaderArray.GetShader(image.material.shader.name);
        }
    }
}

