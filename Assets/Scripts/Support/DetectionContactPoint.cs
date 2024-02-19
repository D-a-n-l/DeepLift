using UnityEngine;

public static class DetectionContactPoint
{
    private static Vector2 newPosition;

    private static float newRotation;

    public static Vector2 Position(Collision2D collision)
    {
        foreach (ContactPoint2D missileHit in collision.contacts)
            newPosition = missileHit.point;

        return newPosition;
    }

    public static float Rotation(Rigidbody2D rigidbody)
    {
        if (rigidbody.velocity.x > 0)
            newRotation = 270f;
        else
            newRotation = 90f;

        return newRotation;
    }
}