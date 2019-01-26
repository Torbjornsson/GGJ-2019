using UnityEngine;
using System.Collections.Generic;

public class GoalChecker : MonoBehaviour
{
    List<Goal> Goals;

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        foreach(var g in Goals)
        {
            GUILayout.Label($"{g.Description}: {(g.CheckCompleted() ? "OK" : "NOT OK")}");
        }

        GUILayout.EndVertical();
    }

    private void Start()
    {
        Goals = new List<Goal>();

        var objects = FindObjectsOfType<GoalObject>();

        foreach(var o in objects)
        {
            Goals.Add(new ObjectOnSideOfRoom(o));
        }
    }

    public enum RoomSide
    {
        NORTH,
        SOUTH,
        WEST,
        EAST
    }

    static RoomSide RandomSide()
    {
        var r = Random.Range(0, 4);
        return (RoomSide)r;
    }

    static bool CheckSide(Vector3 v, RoomSide side)
    {
        switch (side)
        {
            case RoomSide.EAST:
                return v.x > 0;
            case RoomSide.WEST:
                return v.x < 0;
            case RoomSide.NORTH:
                return v.z > 0;
            case RoomSide.SOUTH:
                return v.z < 0;
        }

        return false;
    }

    abstract class Goal
    {
        public virtual string Description => "";
        public abstract bool CheckCompleted();
    }

    class ObjectOnSideOfRoom : Goal
    {
        GoalObject target;
        RoomSide side;

        public ObjectOnSideOfRoom(GoalObject target)
        {
            this.target = target;
            side = RandomSide();
        }

        public override string Description => $"Put the {target.FriendlyName} on the {side} side";

        public GoalObject[] Targets;

        public override bool CheckCompleted()
        {
            return CheckSide(target.transform.position, side);
        }
    }
}
