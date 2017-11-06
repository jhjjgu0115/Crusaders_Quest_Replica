using System.Collections.Generic;
[System.Serializable]
public class HeroInfo
{
    public HeroInfo(string name, E_HeroClass heroClass)
    {
        this.name = name;
        this.heroClass = heroClass;
    }
    public HeroInfo()
    {
        name = string.Empty;
        heroClass = E_HeroClass.Worrior;
    }
    public string name;
    public E_HeroClass heroClass;
    public string skillName;
    public string spSkillName;
}
[System.Serializable]
public class HeroDatabase
{
    public List<HeroInfo> list = new List<HeroInfo>();
}