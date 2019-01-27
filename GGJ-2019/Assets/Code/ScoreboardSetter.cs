using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreboardSetter : MonoBehaviour
{
    public GameObject GoalPrefab;
    public CheckSwapper CheckPrefab;

    public Transform GoalHolder;
    public Transform CheckboxHolder;

    CheckSwapper[] Checkboxes;

    public void SetGoals(string[] goals)
    {
        Checkboxes = new CheckSwapper[goals.Length];

        for (int i = 0; i < goals.Length; i++)
        {
            var t = Instantiate(GoalPrefab, GoalHolder);
            t.GetComponent<Text>().text = goals[i];

            Checkboxes[i] = Instantiate(CheckPrefab, CheckboxHolder);
        }
    }

    public void SetGoalStatus(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            Checkboxes[i].Checked = status[i];
        }
    }
}