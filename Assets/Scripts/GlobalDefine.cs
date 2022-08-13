public enum BossActionType
{
    Skip,
    AOEAttack,
    AOEAttackExcludePlayer,
    SingleAttack,
    Summon,
}
public enum SingleTargetOption
{
    PlayerTarget,
    RandomTarget,
    HighestHPTarget,
    LowestHPTarget,
    HigestATKTarget,
    LowestATKTarget
}
/// <summary>
/// Ŀ��ѡ��
/// </summary>
/// <returns></returns>
public enum TargetOptions
{
    NoTarget,
    /// <summary>
    /// ���е�λ
    /// </summary>
    AllCreatures,
    /// <summary>
    /// ������ҵ�λ��������ұ��壩
    /// </summary>
    AllPlayerCreatures,
    /// <summary>
    /// ���ез���λ������boss���壩
    /// </summary>
    AllEnemyCreatures,
    /// <summary>
    /// ������ӣ���������Һ�boss��
    /// </summary>
    AllCharacters,
    /// <summary>
    /// ���������� ����������ң�
    /// </summary>
    ALlPlayerCharacter,
    /// <summary>
    /// ���ез���ӣ�������boss��
    /// </summary>
    ALlEnemyCharacters,

    MultiPlayerTargets,
    MultiEnemyTargets,

    SinglePlayerTarget, //�������Ŀ��
    SingleEnemyTarget, //�����з�Ŀ��
}
public enum RarityRank  //ϡ�ж�
{
    Normal,
    Rare,
    Epic,
    Legend, 
}
public enum CardType  
{
    Spell, 
    Survent,
    Monster,
}
public enum CardCamp
{
    ����ҽ��, 
    �¶���ҵ, 
    ��������, 
    ����˹����, 
    ��,
    Enemy,
}
public enum GameResult
{
    Success,
    Failure,
    Escape,
}
/// <summary>
/// ���/����Ч��
/// </summary>
/// <returns></returns>
public enum EffectType
{
    Void,
    Attack,
    VampireAttack,
    Heal,
    Taunt,
    Protect,
    Conceal,
    /// <summary>
    /// ����ǿ������ֵ1->��������ֵ2->����
    /// </summary>
    Enhance,
    /// <summary>
    /// ��ʱǿ������ֵ1->��������ֵ2->����, ��ֵ3->�����غ���
    /// </summary>
    Inspire,
    /// <summary>
    /// �ض�����, ��ֵ1->����id, ��ֵ2->��������
    /// </summary>
    DrawSpecificCard,
    /// <summary>
    /// �������, ��ֵ1->��������
    /// </summary>
    DrawRandomCard,

    SpecialEffect,

    //*����*��*�Ȼ�*��*ÿ�غϿ�ʼʱ*��*ÿ�غϽ���ʱ*, boss�����⼼��
}
