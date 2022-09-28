using UnityEngine;

public struct Impact
{
    public readonly float intensity;
    public readonly Vector3 direction;

    public Impact (float intensity, Vector3 direction)
    {
        this.intensity = intensity;
        this.direction = direction;
    }
}
