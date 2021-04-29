using UnityEngine;

public class DamageAnimationEvents : MonoBehaviour
{
    public delegate void DamageAction();
    public static event DamageAction OnDamaged;
    
    public void DoDamage()
    {
        OnDamaged?.Invoke();
    }
}
