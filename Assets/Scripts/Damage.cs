using UnityEngine;
public class Damage : MonoBehaviour
{
    private int attackPoint;

    public int GetAttackPoint(){
        return attackPoint;
    }

    public void SetAttackPoint(int newValue){
        attackPoint = newValue;
    }
}