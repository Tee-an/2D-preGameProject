using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasePanel
{
    public UIType uiType;

    /// <summary>
    /// The object in the scene which this panel is included
    /// </summary>
    public GameObject ActiveObj;

    public BasePanel(UIType uiType)
    {
        this.uiType = uiType;
    }

    public virtual void OnStart()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }

    public virtual void OnEnable()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }

    public virtual void OnDisable()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }

    public virtual void OnDestroy()
    {
        UIMethod.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }
}
