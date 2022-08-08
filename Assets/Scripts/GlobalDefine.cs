public enum BossActionType  //
{
    Skip,
    AOEAttack,
    AOEAttackExcludePlayer,
    SingleAttack,
    Summon,

}
public enum BossAttack
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

public static class Const
{
    //������Ӧ��Դ�ļ�λ���ַ���
    public static string CARD_DATA_PATH(int _id)
	{
        if (_id > 0)
        {
            return "CardDatas/SVN-" + _id.ToString("D3");
        }
        else if (_id < 0)
        {
            return "CardDatas/SPL-" + (-_id).ToString("D3");
        }
        else return null;
    }
    public static string MONSTER_CARD_PATH(int _id)
	{
        return "CardDatas/MON-" + _id.ToString("D3");
	}
    public static string BOSS_DATA_PATH(int _id)
	{
        return "BossDatas/MON-" + _id.ToString("D3");
    }
    public static string PLAYER_DATA_PATH(int _id)
	{
        return UnityEngine.Application.dataPath + "/PlayerDatas/" + _id.ToString("D2") + ".json";
	}
    //����
    public static int Forever = -1;
    public static int MaxSaveCount = 10;
    
}
public struct PlayerBattleInformation
{
    public string name;
    public int maxHP;
    public int currentHP;
    public System.Collections.Generic.List<int> cardSet;
}
public enum TargetOptions //����Ŀ��ѡ��
{
    NoTarget,
    
    AllCreatures, //���е�λ
    AllPlayerCreatures,  //������ҵ�λ��������ұ��壩
    AllEnemyCreatures,  //���ез���λ������boss���壩

    SinglePlayerCreatures, //�������Ŀ��
    SingleEnemyCreature, //�����з�Ŀ��



    AllCharacters,  //������ӣ���������Һ�boss��
    PlayerCharacter, //���������� ����������ң�
    EnemyCharacter  //���ез���ӣ�������boss��
}
public enum RarityRank  //ϡ�ж�
{
    Legend, Gold, Silver, Normal
}
public enum CardType  
{
    //Player, 
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
public enum CardActionType
{
    Attack,  //����->�˺�ֵ
    VampireAttack,  //��Ѫ->�˺�ֵ
    Taunt,  //����->�غ���
    Heal, //����->�ظ���
    HPEnhance, //����ǿ��->ǿ����
    Protect,  //�ӻ�->�غ���
    Inspire, //����(��ʱ��������)->��ֵ+�غ�
    Waghhh, //Waghhhhhh!!!(���ù�������)->��ֵ
    Conceal, //����->�غ�

    //Silence, //��Ĭ->�غ���

    //*����*��*�Ȼ�*��*ÿ�غϿ�ʼʱ*��*ÿ�غϽ���ʱ*��Ч��ͨ��
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
