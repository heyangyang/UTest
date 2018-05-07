
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShaderArray : MonoBehaviour
{
    public static Dictionary<string, Shader> sMaps = new Dictionary<string, Shader>();
    public static void ReplaceShader(GameObject obj)
    {
        var renders = obj.GetComponentsInChildren<Renderer>(true);

        for (int i = 0; i < renders.Length; i++)
        {
            Renderer render = renders[i];
            if (render.sharedMaterial != null)
            {
                render.sharedMaterial.shader = GetShader(render.sharedMaterial.shader.name);
            }
        }

        var images = obj.GetComponentsInChildren<Image>(true);
        for (int i = 0; i < images.Length; i++)
        {
            Image image = images[i];
            image.material.shader = GetShader(image.material.shader.name);
        }
    }

    public static Shader GetShader(string shaderName)
    {
        if (sMaps.ContainsKey(shaderName))
            return sMaps[shaderName];
        return null;
    }


    public Shader[] gameList;
    void Awake()
    {
        AssetBundleLoader loader = AssetBundleManager.GetInstance().GetLoader(PathUtil.GetAbUrl("Shader", EnResourceType.Shader));
        for (int i = 0; i < gameList.Length; i++)
        {
            Shader shader = gameList[i];
            Shader findShader = Shader.Find(shader.name);
            if (findShader != null)
                shader = findShader;
            else
            {
                findShader = loader.LoadAsset<Shader>(shader.name);
                if (findShader != null)
                    shader = findShader;
            }
            sMaps[shader.name] = shader;
        }
    }
}

