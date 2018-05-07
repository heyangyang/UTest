using UnityEngine;
using System.Collections;

public class UITextureBlurTool
{
    #region 单例
    private static UITextureBlurTool m_Instance;
    public static UITextureBlurTool GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new UITextureBlurTool();
        } return m_Instance;
    }
    #endregion


	private  int Iterations = 6;

    private float BlurSpread = -0.23f;

    private Material m_blurMaterial = null;

    private Shader m_blurShader = null;

    private RenderTexture m_Texture;
	
	private  void FourTapCone (RenderTexture source, RenderTexture dest, int iteration)
	{
		float off = 0.5f + iteration* BlurSpread;
        SetBlurMaterilal();
        Graphics.BlitMultiTap(source, dest, m_blurMaterial,
			new Vector2(-off, -off),
			new Vector2(-off,  off),
			new Vector2( off,  off),
			new Vector2( off, -off)
		);
	}


	private  void DownSample4x (Texture source, RenderTexture dest)
	{
		float off = 1.0f;
        SetBlurMaterilal();
        Graphics.BlitMultiTap(source, dest, m_blurMaterial,
			new Vector2(-off, -off),
			new Vector2(-off,  off),
			new Vector2( off,  off),
			new Vector2( off, -off)
		);
	}
	
    private void SetBlurMaterilal()
    {
        if (m_blurShader == null)
        {
            //m_blurShader = GameTool.GetSpShader("Hidden/BlurEffect");
        }
        if (m_blurMaterial == null)
        {
            m_blurMaterial = new Material(m_blurShader);
            m_blurMaterial.hideFlags = HideFlags.DontSave;
        }
    }

    

    /// <summary>
    /// 处理图片模糊;
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public  RenderTexture F_HandleImage(Texture source)
    {		
		int rtW = source.width/4;
		int rtH = source.height/4;
		RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
		
		DownSample4x (source, buffer);
		
		for(int i = 0; i < Iterations; i++)
		{
			RenderTexture buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0);
			FourTapCone (buffer, buffer2, i);
			RenderTexture.ReleaseTemporary(buffer);
			buffer = buffer2;
		}

        if (null == m_Texture)
        {
            m_Texture = new RenderTexture(source.width, source.height, 0);
        }
        else
        {
            m_Texture.DiscardContents();
        }
        Graphics.Blit(buffer, m_Texture);		
		RenderTexture.ReleaseTemporary(buffer);
        return m_Texture;
	}

}
