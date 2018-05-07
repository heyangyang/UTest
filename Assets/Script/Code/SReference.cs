
using UnityEngine;
public class SReference
{
    public bool mAllowDestroy;
    public bool isDisposed { private set; get; }
    public int referenceCount { private set; get; }
    public float lastUseTime;

    public SReference()
    {
        isDisposed = false;
        mAllowDestroy = false;
        referenceCount = 0;
        Retain();
    }

    public void Retain()
    {
        if (isDisposed)
            return;
        ++referenceCount;
        mAllowDestroy = false;
    }

    public void Release()
    {
        if (isDisposed)
            return;
        lastUseTime = Time.realtimeSinceStartup;
        --referenceCount;
        if (referenceCount < 0)
            Loger.Error(this.GetType().Name + "  release error");
        if (referenceCount == 0)
        {
            mAllowDestroy = true;
        }
    }

    public bool TryDestroy()
    {
        if (isDisposed)
            return false;
        if (mAllowDestroy)
        {
            OnDispose();
            return true;
        }
        return false;
    }

    /**
     * 清除内存
     */
    protected virtual void OnDispose()
    {
        isDisposed = true;
        mAllowDestroy = false;
        referenceCount = 0;
    }
}
