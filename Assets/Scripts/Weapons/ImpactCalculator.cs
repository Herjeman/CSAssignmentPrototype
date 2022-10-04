using UnityEngine;

public static class ImpactCalculator
{
    public static Impact GetImpact(Vector3 origin, Vector3 target, float radius)
    {
        float distance = GetDistance(origin, target);
        float intensity = GetForce(NormalizeDistance(distance, radius));

        Vector3 direction = GetDirection(origin, target);

        Impact impact = new Impact(intensity, direction);
        return impact;
    }

    private static float GetDistance(Vector3 origin, Vector3 target)
    {
        return Vector3.Distance(origin, target);
    }

    private static Vector3 GetDirection(Vector3 origin, Vector3 target)
    {
        Vector3 direction = target - origin;
        return direction.normalized;
    }

    private static float NormalizeDistance(float distance, float maxDistance)
    {
        return distance / maxDistance;
    }

    private static float GetForce(float normalizedDistance)
    {
        return 1 - normalizedDistance * normalizedDistance;
    }
}
