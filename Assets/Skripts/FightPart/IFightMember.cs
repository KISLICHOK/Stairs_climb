
public interface IFightMember {
    public int HP { get; }
    public AttackType CurrentAttack { get; }
    void StartInizialisation();
    void Abillity();
    void ReduceHP(int damage);

}
