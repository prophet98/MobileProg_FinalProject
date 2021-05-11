namespace DamageScripts
{
    public interface IDamageable<T>
    {
        void RemoveHealth(T damage);
    }
}
