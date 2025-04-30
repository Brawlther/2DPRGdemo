using UnityEngine;

public class Monitor : MonoBehaviour
{
    private Knight _knight;
    void Start()
    {
        _knight = FindAnyObjectByType<Knight>();
        // Debug.LogFormat("Knight Info:\tHp: {0}\tMp: {1}\tStamina: {2}",
        //     _knight.GetHp(),
        //     _knight.GetMp(),
        //     _knight.GetStamina()
        // );
    }
}
