public static class Const
{
    //������Ӧ��Դ�ļ�λ���ַ���
    public static string SVN_CARD_DATA_PATH(int _id)
	{
        return "CardDatas/SVN-" + _id.ToString("D3");
    }
    public static string SPL_CARD_DATA_PATH(int _id)
	{
        return "Carddatas/SPL-" + _id.ToString("D3");
    }
    public static string BOSS_DATA_PATH(int _id)
	{
        return "BossDatas/MON-" + _id.ToString("D3");
    }

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
    PlayerCharacters, //������
    EnemyCharacters  //�з����
}
public enum RarityRank  //ϡ�ж�
{
    Legend, Gold, Silver, Normal
}
public enum CardType  
{
    Player, Monster, Spell, Survent
}
public enum CardCamp
{
    BlueOcean, Oddin, BaseSpeed, Fortress, Enemy, Player, Snake
}
public enum CardEffect
{
    Attack,  //����
    VampireAttack,  //��Ѫ
    Taunt,  //����
    DeadWhisper, //����
    Heal, //����
    HPEnhance, //����ǿ��
    Protect,  //�ӻ�
    Inspire, //����(��ʱ��������)
    Waghhh, //Waghhhhhh!!!(���ù�������)
}
