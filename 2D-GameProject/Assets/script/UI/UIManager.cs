using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance;

    /// <summary>
    /// This dict saves the connection between the uiMame and the Object
    /// </summary>
    public Dictionary<string, GameObject> dict_ui;

    /// <summary>
    /// The stack saves the UI Panel
    /// </summary>
    public Stack<BasePanel> stack_ui;

    /// <summary>
    /// UIManager need to have a canvas in the scene
    /// </summary>
    public GameObject canvasobj;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("UIManager does not exist!");
            return instance;
        }
        else
        {
            return instance;
        }
    }

    //This method is firstly used for GetInstance method 
    public UIManager()
    {
        instance = this;
    }

    public GameObject GetSingleObj(UIType uItype)
    {
        if (dict_ui.ContainsKey(uItype.Name))
        {
            return dict_ui[uItype.Name];
        }
        if (canvasobj == null)
        {
            canvasobj = UIMethod.GetInstance().GetCanvas();
        }

        //This code aims to instantiate a gameobject from the local PC
        GameObject gameobj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uItype.Path),canvasobj.transform);
        return gameobj;
    }

    public void push(BasePanel basepanel)
    {
        Debug.Log($"{basepanel.uiType.Name} is pushing into the stack!");

        if (stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisable();

        }

        GameObject ui_obj = GetSingleObj(basepanel.uiType);
        dict_ui.Add(basepanel.uiType.Name, ui_obj);

        // so that you can dosomething in the ui canvas
        basepanel.ActiveObj = ui_obj;

        if (stack_ui.Count == 0)
        {
            stack_ui.Push(basepanel);
        }
        else
        {
            if (stack_ui.Peek().uiType.Name != basepanel.uiType.Name)
            {
                stack_ui.Push(basepanel);
            }
        }

        basepanel.OnStart();
    }

    /// <summary>
    /// so you can destroy current UI windows one times or more 
    /// </summary>
    /// <param name="isload"></param>
    public void pop(bool isload)
    {
        if (isload == true)
        {
            if (stack_ui.Count > 0)
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestroy();
                GameObject.Destroy(dict_ui[stack_ui.Peek().uiType.Name]);
                dict_ui.Remove(stack_ui.Peek().uiType.Name);
                stack_ui.Pop();
                pop(true );
            }
            else
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestroy();
                GameObject.Destroy(dict_ui[stack_ui.Peek().uiType.Name]);
                dict_ui.Remove(stack_ui.Peek().uiType.Name);
                stack_ui.Pop();

                if (stack_ui.Count > 0)
                {
                    stack_ui.Peek().OnEnable();
                }
            }
        }
    }
}
