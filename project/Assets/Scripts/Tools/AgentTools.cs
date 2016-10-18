using UnityEngine;
using System.Collections;

public static class AgentTools
{
    public static IEnumerator OffMeshLinkNormal(NavMeshAgent agent, Vector3 endPos)
    {
        var trans = agent.transform;
        var targetDir = (endPos.SetPositionY(trans.position.y) - trans.position).normalized;
        var delta = Time.deltaTime;
        for (; agent && endPos != trans.position;) {
            trans.forward = Vector3.RotateTowards(trans.forward, targetDir, 10 * delta, 0);
            var nextPos = Vector3.MoveTowards(trans.position, endPos, agent.speed * delta);
            trans.position = nextPos;
            yield return null;
        }
    }

    public static IEnumerator Blink(NavMeshAgent agent, Vector3 endPos)
    {
        agent.transform.position = endPos;
        yield return null;
    }

    public static IEnumerator JumpAcross(NavMeshAgent agent, Vector3 endPos)
    {
        var trans = agent.transform;
        var targetDir = (endPos.SetPositionY(trans.position.y) - trans.position).normalized;
        var delta = Time.deltaTime;
        for (; agent && endPos != trans.position;) {
            trans.forward = Vector3.RotateTowards(trans.forward, targetDir, 10 * delta, 0);
            var nextPos = Vector3.MoveTowards(trans.position, endPos, agent.speed * delta);
            trans.position = nextPos;
            yield return null;
        }
    }

    public static IEnumerator DropDown(NavMeshAgent agent, Vector3 endPos)
    {
        var trans = agent.transform;
        var targetDir = (endPos.SetPositionY(trans.position.y) - trans.position).normalized;
        var delta = Time.deltaTime;
        for (; agent && endPos != agent.transform.position;) {
            trans.forward = Vector3.RotateTowards(trans.forward, targetDir, 10 * delta, 0);
            var nextPos = Vector3.MoveTowards(trans.position, endPos, agent.speed * delta);
            trans.position = nextPos;
        }
        yield return null;
    }

    public static IEnumerator JumpDowm(NavMeshAgent agent, Vector3 endPos, float h, float g)
    {
        var trans = agent.transform;
        var targetDir = (endPos.SetPositionY(trans.position.y) - trans.position).normalized;
        float ls = (endPos.SetPositionY(trans.position.y) - trans.position).magnitude;
        float lh = endPos.y - trans.position.y;
        h = h < lh ? lh : h;
        float vh = Mathf.Sqrt(g * h * 2);
        float vs = ls / (Mathf.Sqrt(2 * h / g) + Mathf.Sqrt(2 * (h - lh) / g));
        for (; agent && (endPos.y < trans.position.y || vh > 0);) {
            var delta = Time.deltaTime;
            trans.forward = Vector3.RotateTowards(trans.forward, targetDir, 10 * delta, 0);
            Vector3 move = targetDir * delta * vs + Vector3.up * (vh * delta - g * delta * delta / 2);
            vh -= g * delta;
            trans.position = trans.position + move;
            yield return null;
        }
    }

    public static void CopyFrom(this NavMeshAgent self, NavMeshAgent tarAgent)
    {
        // Agent Size
        self.radius = tarAgent.radius;
        self.height = tarAgent.height;
        self.baseOffset = tarAgent.baseOffset;

        // Steering
        self.speed = tarAgent.speed;
        self.angularSpeed = tarAgent.angularSpeed;
        self.acceleration = tarAgent.acceleration;
        self.stoppingDistance = tarAgent.stoppingDistance;
        self.autoBraking = tarAgent.autoBraking;

        // Obstacle Avoidance
        self.obstacleAvoidanceType = tarAgent.obstacleAvoidanceType;
        self.avoidancePriority = tarAgent.avoidancePriority;

        // Path Finding
        self.autoTraverseOffMeshLink = tarAgent.autoTraverseOffMeshLink;
        self.autoRepath = tarAgent.autoRepath;
        self.areaMask = tarAgent.areaMask;
    }

    public static void CopyFrom(this NavMeshObstacle self, NavMeshObstacle tarObstacle)
    {
        self.carveOnlyStationary = tarObstacle.carveOnlyStationary;
        self.carving = tarObstacle.carving;
        self.carvingMoveThreshold = tarObstacle.carvingMoveThreshold;
        self.carvingTimeToStationary = tarObstacle.carvingTimeToStationary;
        self.center = tarObstacle.center;
        self.height = tarObstacle.height;
        self.radius = tarObstacle.radius;
        self.shape = tarObstacle.shape;
        self.size = tarObstacle.size;
        self.velocity = tarObstacle.velocity;
    }
}
