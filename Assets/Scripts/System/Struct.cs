using System.Collections.Generic;
[System.Serializable]
public class HeroInfo
{
    public HeroInfo(string name, E_HeroClass heroClass)
    {
        this.name = name;
        blockSkill = string.Empty;
        specialSkill = string.Empty;
        this.heroClass = heroClass;
    }
    public HeroInfo()
    {
        name = string.Empty;
        blockSkill = string.Empty;
        specialSkill = string.Empty;
        heroClass = E_HeroClass.Warrior;
    }
    public string name;
    public E_HeroClass heroClass;
    public string blockSkill;
    public string specialSkill;
    public bool canUse;
}
[System.Serializable]
public class HeroDatabase
{
    public List<HeroInfo> list = new List<HeroInfo>();
}