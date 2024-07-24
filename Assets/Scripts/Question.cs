[System.Serializable]
public class Question
{
    public string Text;
    public string OptionA;
    public string OptionB;
    public DimensionType Dimension;

    public Question(string text, string optionA, string optionB, DimensionType dimension)
    {
        Text = text;
        OptionA = optionA;
        OptionB = optionB;
        Dimension = dimension;
    }
}

public enum DimensionType
{
    ExtrovertIntrovert,
    SensingIntuition,
    ThinkingFeeling,
    JudgingPerceiving
}
