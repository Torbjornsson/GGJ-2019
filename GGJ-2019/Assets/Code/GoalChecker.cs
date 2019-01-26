using UnityEngine;
using System.Collections.Generic;

public class GoalChecker : MonoBehaviour
{
    Goal[] TeamAGoals;
    Goal[] TeamBGoals;

    bool[] TeamAGoalStatus;
    bool[] TeamBGoalStatus;

    //List<Goal> Goals;

    List<GoalObject> GoalObjects;

    public int GoalsPerTeam = 3;

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        foreach (var g in TeamAGoals)
        {
            GUILayout.Label($"{g.Description}: {(g.CheckCompleted() ? "OK" : "NOT OK")}");
        }

        GUILayout.EndVertical();
    }

    private void Start()
    {
        TeamAGoals = new Goal[GoalsPerTeam];
        TeamBGoals = new Goal[GoalsPerTeam];

        GoalObjects = new List<GoalObject>(FindObjectsOfType<GoalObject>());

        for (int i = 0; i < GoalsPerTeam; i++)
        {
            TeamAGoals[i] = GetGoal();
            TeamBGoals[i] = GetGoal();
        }

        TeamAGoalStatus = new bool[GoalsPerTeam];
        TeamBGoalStatus = new bool[GoalsPerTeam];


        //    Goals = new List<Goal>();

        //    var objects = FindObjectsOfType<GoalObject>();


        //    foreach(var o in objects)
        //    {
        //        var otherObject = objects[Random.Range(0, objects.Length)];

        //        Goals.Add(new ObjectBySideWall(o));
        //        Goals.Add(new ObjectByCorner(o));
        //        Goals.Add(new ObjectByAnyWall(o));
        //        Goals.Add(new ObjectFacingDirection(o));
        //        Goals.Add(new ObjectNearObject(o, otherObject));
        //        Goals.Add(new ObjectFacingObject(o, otherObject));
        //}
    }

    Goal GetGoal()
    {
        GoalObjects.Shuffle();
        GoalObjects.Sort((aa, bb) => aa.UseCount.CompareTo(bb.UseCount));

        var a = GoalObjects[0];
        var b = GoalObjects[1];

        var r = Random.Range(0, 8);

        switch (r)
        {
            case 0:
                a.UseCount++;
                return new ObjectOnSideOfRoom(a);
            case 1:
                a.UseCount++;
                return new ObjectInMIddleOfRoom(a);
            case 2:
                a.UseCount++;
                return new ObjectBySideWall(a);
            case 3:
                a.UseCount++;
                return new ObjectByAnyWall(a);
            case 4:
                a.UseCount++;
                return new ObjectByCorner(a);
            case 5:
                a.UseCount++;
                return new ObjectFacingDirection(a);
            case 6:
                a.UseCount++;
                b.UseCount++;
                return new ObjectNearObject(a, b);
            case 7:
                a.UseCount++;
                b.UseCount++;
                return new ObjectFacingObject(a, b);


            default:
                a.UseCount++;
                return new ObjectOnSideOfRoom(a);
        }
    }

    private void Update()
    {
        for (int i = 0; i < GoalsPerTeam; i++)
        {
            TeamAGoalStatus[i] = TeamAGoals[i].CheckCompleted();
            TeamBGoalStatus[i] = TeamBGoals[i].CheckCompleted();
        }
    }

    public enum RoomSide
    {
        NORTH,
        SOUTH,
        WEST,
        EAST
    }

    public enum RoomCorner
    {
        NORTHEAST,
        SOUTHEAST,
        SOUTHWEST,
        NORTHWEST
    }

    static RoomSide RandomSide()
    {
        var r = Random.Range(0, 4);
        return (RoomSide)r;
    }

    static RoomCorner RandomCorner()
    {
        var r = Random.Range(0, 4);
        return (RoomCorner)r;
    }

    static Vector3 GetDirection(RoomSide side)
    {
        switch (side)
        {
            case RoomSide.NORTH:
                return Vector3.forward;
            case RoomSide.SOUTH:
                return Vector3.back;
            case RoomSide.EAST:
                return Vector3.right;
            case RoomSide.WEST:
                return Vector3.left;
        }

        return Vector3.zero;
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

    static bool CheckWall(Vector3 v, RoomSide side)
    {
        switch (side)
        {
            case RoomSide.NORTH:
                return v.z > 1.5f;
            case RoomSide.SOUTH:
                return v.z < -1.5f;
            case RoomSide.EAST:
                return v.x > 2.5f;
            case RoomSide.WEST:
                return v.x < -2.5f;
        }

        return false;
    }

    static bool CheckCorner(Vector3 v, RoomCorner corner)
    {
        switch (corner)
        {
            case RoomCorner.NORTHEAST:
                return CheckWall(v, RoomSide.NORTH) && CheckWall(v, RoomSide.EAST);
            case RoomCorner.NORTHWEST:
                return CheckWall(v, RoomSide.NORTH) && CheckWall(v, RoomSide.WEST);
            case RoomCorner.SOUTHEAST:
                return CheckWall(v, RoomSide.SOUTH) && CheckWall(v, RoomSide.EAST);
            case RoomCorner.SOUTHWEST:
                return CheckWall(v, RoomSide.SOUTH) && CheckWall(v, RoomSide.WEST);
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

        public override bool CheckCompleted()
        {
            return CheckSide(target.transform.position, side);
        }
    }

    class ObjectInMIddleOfRoom : Goal
    {
        GoalObject target;

        public ObjectInMIddleOfRoom(GoalObject target)
        {
            this.target = target;
        }

        public override string Description => $"Put the {target.FriendlyName} in the MIDDLE";

        public override bool CheckCompleted()
        {
            var v = target.transform.position;
            return Mathf.Abs(v.x) < 2 && Mathf.Abs(v.z) < 1;
        }
    }

    class ObjectBySideWall : Goal
    {
        GoalObject target;
        RoomSide side;

        public ObjectBySideWall(GoalObject target)
        {
            this.target = target;
            side = RandomSide();
        }

        public override string Description => $"Put the {target.FriendlyName} by the {side} WALL";

        public override bool CheckCompleted()
        {
            return CheckWall(target.transform.position, side);
        }
    }

    class ObjectByCorner : Goal
    {
        GoalObject target;
        RoomCorner corner;

        public ObjectByCorner(GoalObject target)
        {
            this.target = target;
            corner = RandomCorner();
        }

        public override string Description => $"Put the {target.FriendlyName} in the {corner} corner";

        public override bool CheckCompleted()
        {
            return CheckCorner(target.transform.position, corner);
        }
    }

    class ObjectByAnyWall : Goal
    {
        GoalObject target;

        public ObjectByAnyWall(GoalObject target)
        {
            this.target = target;
        }

        public override string Description => $"Put the {target.FriendlyName} by a WALL";

        public override bool CheckCompleted()
        {
            for (int i = 0; i < 4; i++)
            {
                if (CheckWall(target.transform.position, (RoomSide)i)) return true;
            }
            return false;
        }
    }

    class ObjectFacingDirection : Goal
    {
        GoalObject target;
        RoomSide side;

        public ObjectFacingDirection(GoalObject target)
        {
            this.target = target;
            side = RandomSide();
        }

        public override string Description => $"The {target.FriendlyName} should face {side}.";

        public override bool CheckCompleted()
        {
            return Vector3.Dot(target.transform.forward, GetDirection(side)) > 0.2f;
        }
    }

    class ObjectNearObject : Goal
    {
        GoalObject targetA;
        GoalObject targetB;

        public ObjectNearObject(GoalObject targetA, GoalObject targetB)
        {
            this.targetA = targetA;
            this.targetB = targetB;
        }

        public override string Description => $"The {targetA.FriendlyName} should be near the {targetB.FriendlyName}";

        public override bool CheckCompleted()
        {
            return Vector3.Distance(targetA.transform.position, targetB.transform.position) < 2.5f;
        }
    }

    class ObjectFacingObject : Goal
    {
        GoalObject targetA;
        GoalObject targetB;

        public ObjectFacingObject(GoalObject targetA, GoalObject targetB)
        {
            this.targetA = targetA;
            this.targetB = targetB;
        }

        public override string Description => $"The {targetA.FriendlyName} should face the {targetB.FriendlyName}";

        public override bool CheckCompleted()
        {
            var delta = targetB.transform.position - targetA.transform.position;

            return Vector3.Dot(delta.normalized, targetA.transform.forward) > 0.5f;
        }
    }
}
