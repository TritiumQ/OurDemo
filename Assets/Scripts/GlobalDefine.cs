
public enum BossActionType
{
    Skip,
    AOEAttack,
    AOEAttackExcludePlayer,
    SingleAttack,
    Summon,
    Skill,
}
public enum BossSingleAttack
{
    PlayerTarget,
    RandomTarget,
    HighestHPTarget,
    LowestHPTarget,
    HigestATKTarget,
    LowestATKTarget
}
public enum GetSurventInfomation
{
    ATK,
    CurrentHP,
    MaxHP,
    Type
}
/// <summary>
/// ������
/// </summary>
/// <returns></returns>
public static class Const
{
    //͵��
    /// <summary>
    /// ��Χ�жϣ�����ұ�
    /// </summary>
    /// <returns></returns>
    static bool IsInRange(int value, int left, int right)
	{
        return value >= left && value <= right;
	}

    /// <summary>
    /// ��ȡ������Ϣ�ļ�����·��
    /// </summary>
    /// <returns></returns>
    public static string CARD_DATA_PATH(int _id)
	{
        if (IsInRange(_id,0,199))
        {
            return "CardDatas/SVN-" + _id.ToString("D3");
        }
        else if (IsInRange(_id,200,399))
        {
            return "CardDatas/SPL-" + _id.ToString("D3");
        }
        else if(IsInRange(_id,500,699))
		{
            return "CardDatas/MON-" + _id.ToString("D3");
        }
        else
		{
            return null;
		}
    }
    public static string BOSS_DATA_PATH(int _id)
	{
        return "BossDatas/MON-" + _id.ToString("D3");
    }
    public static string PLAYER_DATA_PATH(int _id)
	{
        return UnityEngine.Application.dataPath + "/PlayerDatas/Save" + _id.ToString("D2") + ".json";
	}

    public static int Forever = -1;
    public static int MaxSaveCount = 10;
    public static string InitialCode = "mikufans";
    
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
/// 
public enum TargetOptions
{
    NoTarget,
    
    AllCreatures, //���е�λ
    PlayerCreatures,  //������ҵ�λ��������ұ��壩
    EnemyCreatures,  //���ез���λ������boss���壩

    SinglePlayerTarget, //�������Ŀ��
    SingleEnemyTarget, //�����з�Ŀ��

    MultiPlayerTargets,
    MultiEnemyTargets,

    AllCharacters,  //������ӣ���������Һ�boss��
    PlayerCharacter, //���������� ����������ң�
    EnemyCharacters  //���ез���ӣ�������boss��
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

public static class LevelEvent
{
    public static int[] GameEventCount = { 0, 5, 6, 7, 8 };//��ʼ��ÿ��ؿ�������1-4��
    public static int[] level_1 = { 1, 1, 3, 3, 4 };
    public static int[] level_2 = { 1, 1, 3, 3, 4, 2 };
    public static int[] level_3 = { 1, 1, 1, 2, 3, 3, 5 };
    public static int[] level_4 = { 1, 1, 1, 3, 3, 4, 2, 5 };

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
