namespace EnumTypes
{
    public enum MapType { KEY, NORMAL, BOSS, FINALBOSS, START }   //�� ���� : ��ư���� �̹����� �޶����
    public enum MapTheme { NONE, NATURE, DARK }                   //Stage �׸� : �̰ɷ� ��� ���������� ������ ����
    public enum MapDifficulty { NONE = 0, EASY, NORMAL, HARD }    //���̵� -> (int)������ �޾ƿͼ� ���� ���Ȱ��� �����ְų� �ؾ��ҵ� 
    public enum MonsterID { Chomper, SPITTER, BEHOLDER, MIMIC }
    public enum Window { BOSSHPBAR, PLAYERSTATE, STAGETITLE, GETCARD, SELECTCARD, MAINMENU, GAMEOVER, MAP, OPTION, LOADING }
    public enum Option { FULLHD, CAMERASHAKE, SOUND, MOUSE, STATUS}
    public enum CardRarity { NOMAL, RARE }
    public enum Class { NONE, SWORD, GUN, MAGIC }
    public enum CardID
    {
        CARD_SPRINT,        // �̼� ����
        CARD_FRENZY,        // ���� ����
        CARD_SNIPER,        // ���� ����(��Ÿ�) ����
        CARD_POWER,         // ���ݷ� ����
        CARD_JUMP,          // ���� Ƚ�� ����
        CARD_DEFENSE,       // �ǰ� ������ ����
        CARD_COOLDOWN,      // ��ų ��Ÿ�� ����
        CARD_REBIRTH,       // ��Ȱ
        CARD_BARRIER,       // ��ȣ��
        CARD_LIFESTEAL,     // �⺻ ���ݽ� ����
        CARD_REGEN,         // ���� �ð����� ���������� ü�� ���
        CARD_BERSERK,       // ü���� �������� �̼� ���� ����
        CARD_EXPLODE,       // �� óġ�� �����ϸ� ���� ����
        CARD_EXECUTE,       // ������ HP ������ ��� ó��
        CARD_AMPLIFY,       // ũ��Ƽ�ý� ������ ����
        CARD_BOSS,          // ���� �ߵ�
        CARD_POISON,        // �⺻ ���� �� �Ӽ�
        CARD_BLOODLOSS,     // �⺻ ���ݽ� ����Ȯ���� ����
        CARD_FIRE,          // �⺻ ���� �� �Ӽ�
        CARD_ICE,           // �⺻ ���� ���� �Ӽ�
        CARD_SHIELD,        // (����)���� ������ �� ����
        CARD_EARTHQUAKE,    // (����)�� ��� ���� ����
        CARD_STING,         // (����) ��� ��Ʈ�ڽ� ����
        CARD_FLASH,         // (������) �����̵� �� ������
        CARD_METEOR,        // (������) ���׿� ����ü �߰�
        CARD_FROZENFIELD,   // (������) �������� ����, ��ȭ ����
        CARD_ROLL,          // (������) ������ �Ҷ� ����ź ��ô
        CARD_FLASHBANG,     // (������) ����ź ���� ����
        CARD_GENOCIDE,      // (������) ����� �̵�, ����, ���ӽð� ����
        CARD_PET
    }
}
