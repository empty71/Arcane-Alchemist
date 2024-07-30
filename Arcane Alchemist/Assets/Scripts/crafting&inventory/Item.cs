using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName ="Item",menuName = "Item/BaseItem")]
public class Item : ScriptableObject
{
    new public string name = "defauld item";
    public Sprite icon = null;

    public virtual void Use()
    {
        Debug.Log("using " + name);
    }
}
