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
public struct PlayerBattleInformation
{
    public string name;
    public int maxHP;
    public int currentHP;
    public System.Collections.Generic.List<int> cardSet;
}

/// <summary>
/// Ŀ��ѡ��
/// </summary>
/// <returns></returns>
public enum TargetOptions
{
    NoTarget,
    
    AllCreatures, //���е�λ
    AllPlayerCreatures,  //������ҵ�λ��������ұ��壩
    AllEnemyCreatures,  //���ез���λ������boss���壩

    SinglePlayerTarget, //�������Ŀ��
    SingleEnemyTarget, //�����з�Ŀ��

    MultiPlayerTargets,
    MultiEnemyTargets,

    AllCharacters,  //������ӣ���������Һ�boss��
    ALlPlayerCharacter, //���������� ����������ң�
    ALlEnemyCharacters  //���ез���ӣ�������boss��
}
public enum RarityRank  //ϡ�ж�
{
    Legend, Gold, Silver, Normal
}
public enum CardType  
{
    Monster, 
    Spell, 
    Survent
}
public enum CardCamp
{
    ����ҽ��, �¶���ҵ, ��������, ����˹����, 
    Enemy,
    Snake
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

/// <summary>
/// ��Ϸ����ȫ�ֳ���
/// </summary>
/// <returns></returns>
public static class GameConst
{
    public static int[] GameEventCount = { 0, 5, 6, 7, 8 };//��ʼ��ÿ��ؿ�������1-4��
    public static int[] level_1 = { 1, 1, 3, 3, 4 };
    public static int[] level_2 = { 1, 1, 3, 3, 4, 2 };
    public static int[] level_3 = { 1, 1, 1, 2, 3, 3, 5 };
    public static int[] level_4 = { 1, 1, 1, 3, 3, 4, 2, 5 };

    public static int[] DrawCard = { 1,1,1,1,2,2,2,3,3,4 };
    public static int[,] Card_SVN = { { 0, 0 }, { 1, 23 }, { 50, 78 }, { 100, 117 }, { 150, 159 } };
    public static int[,] Card_SPL = { { 0, 0 }, { 201, 249 }, { 250, 299 }, { 300, 349 }, { 350, 399 } };//���޸�

    public static int[] GetArrray(int[] _target)
    {
        int[] copy = (int[])_target.Clone();
        return copy;
	}
}
