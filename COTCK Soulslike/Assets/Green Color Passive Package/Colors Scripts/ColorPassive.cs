using UnityEngine;

public class ColorPassive : ScriptableObject
{
    public string passiveName;
    public string description;

    protected virtual void Start()
    {
        
    }

    public void SetPassiveInfo(string name, string desc)
    {
        passiveName = name;
        description = desc;
    }
}
