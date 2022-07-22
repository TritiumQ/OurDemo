public enum BossActionType  //TODO
{
    Skip,
    SingleRandomTargetAttack,
    AoeAttack,
    SummonSurvent,
}
public static class Const
{
    //������Ӧ��Դ�ļ�λ���ַ���
    public static string CARD_DATA_PATH(int _id)
	{
        if (_id > 0)
        {
            return "Carddatas/SVN-" + _id.ToString("D3");
        }
        else if (_id < 0)
        {
            return "Carddatas/SPL-" + (-_id).ToString("D3");
        }
        else return null;
    }
    public static string BOSS_DATA_PATH(int _id)
	{
        return "BossDatas/MON-" + _id.ToString("D3");
    }

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
    PlayerCharacters, //������
    EnemyCharacters  //�з����
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
    BlueOcean, Oddin, BaseSpeed, Fortress, 
    Enemy, 
    //Player, 
    Snake
}
public enum CardActionType
{
    Attack,  //����->�˺�ֵ
    VampireAttack,  //��Ѫ->�˺�ֵ
    Taunt,  //����->�غ���
    //DeadWhisper, //����
    Heal, //����->�ظ���
    HPEnhance, //����ǿ��->�ظ���
    Protect,  //�ӻ�->�غ���
    Inspire, //����(��ʱ��������)->��ֵ+�غ�
    Waghhh, //Waghhhhhh!!!(���ù�������)->��ֵ
}
