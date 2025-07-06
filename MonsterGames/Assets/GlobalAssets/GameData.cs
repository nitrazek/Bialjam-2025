using UnityEngine;

public class GameData
{
    public static int ShoesScore { get; set ; } = 0;
    public static int BowlingScore { get; set; } = 0;
    public static int HuggingScore { get; set; } = 0;
    public static int CleaningScore { get; set; } = 0;
    public static int NextScreenId {  get; set; } = 0;
    public const int TRANSITION_SCREEN_ID = 4;
    public static bool showSecret = false;

    public static int Score
    {
        get
        {
            return ShoesScore + BowlingScore + HuggingScore + CleaningScore;
        }
    }

    public static void resetScore()
    {
        ShoesScore = 0;
        BowlingScore = 0;
        HuggingScore = 0;
        CleaningScore = 0;
    }
}
