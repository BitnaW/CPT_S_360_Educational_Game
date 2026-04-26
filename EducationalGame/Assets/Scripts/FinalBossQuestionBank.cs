using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FinalBossQuestionBank", menuName = "Quiz/Final Boss Question Bank")]
public class FinalBossQuestionBank : ScriptableObject
{
    public FinalBossQuestion[] questions;
}

[Serializable]
public class FinalBossQuestion
{
    [TextArea(2, 4)]
    public string prompt;
    public int correctAnswerIndex;
}
