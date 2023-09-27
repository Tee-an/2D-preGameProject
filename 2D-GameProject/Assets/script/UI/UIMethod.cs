using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIMethod
{
    private static UIMethod instance;
    public static UIMethod GetInstance()
    {
        if (instance == null)
        {
            instance = new UIMethod();
            
        }
        return instance;
    }

    /// <summary>
    /// Get the Canvas
    /// </summary>
    /// <returns>Canvas Obj</returns>
    public GameObject GetCanvas()
    {
        GameObject gameobj = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (gameobj != null)
        {
            Debug.LogError("fail to find the Canvas in the scene!");
            return null;
        }
        return gameobj;
    }

    public GameObject GetChildObj(GameObject panel,string child_name)
    {
        Transform[] transforms = panel.GetComponentsInChildren<Transform>();

        foreach (var transform in transforms)
        {
            if (transform.gameObject.name == child_name)
            {
                return transform.gameObject;
            }
        }
        Debug.LogError($"{child_name} is not in the {panel.name}!");
        return null;
    }

    public T AddOrGetComponent<T>(GameObject panel) where T : Component
    {
        if (panel.GetComponent<T>() != null)
        {
            return panel.GetComponent<T>();
        }
        else
        {
            Debug.LogError($"The component can not be found in {panel.name}!");
            panel.AddComponent<T>();
            return panel.GetComponent<T>();
        }
    }

    public T GetOrAddComponentInChild<T>(GameObject panel,string child_name) where T : Component
    {
        Transform[] transforms = panel.GetComponentsInChildren<Transform>(true);

        foreach (var transform in transforms)
        {
            if (transform.name == child_name)
            {
                if (transform.GetComponent<T>() != null)
                {
                    return transform.GetComponent<T>();
                }
                else
                {
                    transform.AddComponent<T>();
                    return transform.GetComponent<T>();
                }
            }
            
        }

        Debug.LogError($"Can nor find the component in the {panel.name}!");
        return null;
    }
}
