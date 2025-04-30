using UnityEngine;

public class DummyFeetCollider : MonoBehaviour
{
    private DummyController controller;

    void Awake()
    {
        controller = GetComponentInParent<DummyController>();
    }

}
