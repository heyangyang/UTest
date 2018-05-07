
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Animator animator;
    //static RuntimeAnimatorController controller;
    void Start()
    {
        animator = TransformUtil.Add<Animator>(gameObject);
        animator.logWarnings = false;
        //if (controller == null)
        //{
            //string fileName = ResMgr.GetInstance().F_GetPath(EnResType.Common) + EnResType.Common.ToString() + FieldConfig.ABExtensionName;
            //string name = ResMgr.GetInstance().F_GetPath(EnResType.Common) + "ButtonAnimator" + FieldConfig.AnimaterExtendsName;
            //controller = ResMgr.GetInstance().LoadAsset<RuntimeAnimatorController>(fileName, name);  //Resources.Load("ButtonAnimator");
        //}
        //animator.runtimeAnimatorController = controller;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (animator != null && _isAnimator == true)
            animator.Play("PressUp");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (animator != null && _isAnimator == true)
            animator.Play("PressDown");
    }
    private bool _isAnimator = true;
    public bool IsAnimator
    {
        get { return _isAnimator; }
        set { _isAnimator = value; }
    }
}
