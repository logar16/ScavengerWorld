namespace ScavengerWorld.Units.Interfaces
{
    public interface IAttacker
    {
        int AttackLevel { get; }
        bool CanAttack();
        void Attack();
    }
}
