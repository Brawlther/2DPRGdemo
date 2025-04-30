using UnityEngine;

public class Explosion : MonoBehaviour
{
    void OnExplosionAnimationEnd(){
        Destroy(gameObject);
    }
}
