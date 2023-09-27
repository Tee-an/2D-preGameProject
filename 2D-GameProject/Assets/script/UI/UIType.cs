using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType
{
    private string name;
    private string path;

    public string Name { get => name; }
    public string Path { get => path; }

    public UIType(string ui_name,string ui_path)
    {
        this.name = ui_name;
        this.path = ui_path;
    }
}
