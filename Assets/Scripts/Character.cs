using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private string _name;
    public string Name {
        get => _name;
        set => _name = value;
    }

}