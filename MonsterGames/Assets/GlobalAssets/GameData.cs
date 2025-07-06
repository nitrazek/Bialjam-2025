using UnityEngine;

public class GameData
{
    public static int ShoesScore { get; set ; } = 0;
    public static int BowlingScore { get; set; } = 0;
    public static int HuggingScore { get; set; } = 0;
    public static int CleaningScore { get; set; } = 0;
    public static int Score
    {
        get
        {
            return ShoesScore + HuggingScore + CleaningScore;
        }
    }
}
