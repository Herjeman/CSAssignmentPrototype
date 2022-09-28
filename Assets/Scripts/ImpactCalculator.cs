using UnityEngine;

public static class ImpactCalculator
{
    public static Impact GetImpact(Vector3 origin, Vector3 target, float radius)
    {
        float distance = GetDistance(origin, target);
        distance = NormalizeDistance(distance, radius);

        Vector3 direction = GetDirection(origin, target);

        Debug.Log($"Calculated impact with an intensity of {distance} in the {direction} direction");
        Impact impact = new Impact(distance, direction);
        return impact;
    }

    private static float GetDistance(Vector3 origin, Vector3 target)
    {
        return Vector3.Distance(origin, target);
    }

    private static Vector3 GetDirection(Vector3 origin, Vector3 target)
    {
        Vector3 direction = origin - target;
        return direction.normalized;
    }

    private static float NormalizeDistance(float distance, float maxDistance)
    {
        Debug.Log($"Normalizing distance {distance} by max distance {maxDistance}");
        return 1 - distance / maxDistance; // doing cool math here could be appropriate
    }
}
