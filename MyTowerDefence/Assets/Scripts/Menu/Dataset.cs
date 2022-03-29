using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dataset
{
    public List<Question> Questions = new List<Question>();
    public string name;
    public int stars;
}

public class Question
{
    public string Problem;
    public string Result;

    public Question(string problem, string result)
    {
        Problem = problem;
        Result = result;
    }
}
