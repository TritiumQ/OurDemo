public static class GlobalConst
{
    

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
