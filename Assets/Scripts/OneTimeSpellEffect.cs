using UnityEngine;

public class OneTimeSpellEffect : MonoBehaviour
{
    public void OnAnimationEnd(){
        Destroy(gameObject);
    }
}
