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
    Attack,
    VampireAttack,
    /// <summary>
    /// �Ա���������ֵ1->�������˺�ֵ, ��ֵ2->�Եз��˺�ֵ
    /// </summary>
    SuicideAttack,
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
    DrawRandomCard

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

/// <summary>
/// ���������Ϣ��JSON�����ļ�֮��ת��ʹ��
/// </summary>
/// <returns></returns>
public class PlayerJSONInformation
{
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public int Mithrils; //����
    public int Tears;  //���
    public int[] CardSet;
    public int[] UnlockCard;

    public PlayerJSONInformation(string InitialCode = "null")
    {
        if(InitialCode == "mikufans")
		{
            Name = "MIKU";
            MaxHP = 39;
            CurrentHP = 16;
            Mithrils = 20070831;
            Tears = 0x39C5BB;

            CardSet = new int[2];
            CardSet[0] = 0;
            CardSet[1] = 200;

            UnlockCard = new int[2];
            UnlockCard[0] = 0;
            UnlockCard[1] = 200;
        }
        else
		{
            Name = null;
            MaxHP = -1;
            CurrentHP = -1;
            Mithrils= -1;
            Tears = -1;
            CardSet = null;
            UnlockCard = null;
		}
    }

    /// <summary>
    /// ��Player���ʼ��
    /// </summary>
    /// <returns></returns>
    public PlayerJSONInformation(Player _player)
	{
        Name = _player.Name;
        MaxHP = _player.MaxHP;
        CurrentHP = _player.CurrentHP;
        Mithrils = _player.Mithrils;
        Tears = _player.Tears;

        CardSet = _player.cardSet.ToArray();

        System.Collections.Generic.List<int> tmpList = new System.Collections.Generic.List<int>();
        for(int i = 0; i < _player.Unlocked.Length; i++)
		{
			if (_player.Unlocked[i] == true)
			{
                tmpList.Add(i);
			}
		}
        UnlockCard = tmpList.ToArray();
        tmpList.Clear();
	}
}
