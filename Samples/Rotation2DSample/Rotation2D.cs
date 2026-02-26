using UnityEngine;

public static class Rotation2D
{

    public static float LookAngle(Vector3 lookDiff, float offset = -90f) => (Mathf.Atan2(lookDiff.y, lookDiff.x) * Mathf.Rad2Deg) + offset;

    public static Vector3 GetPointOnCircle(Vector2 center, float angle) {
        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(x, y, 0f);
    }

    public static Vector2 ClampPointToCircle(Vector2 point, Vector2 pivot, float radius) {
        float distance = Vector2.Distance(point,  pivot);

        if (distance > radius) {
            float angle = Mathf.Atan2(point.y - pivot.y, point.x - pivot.x);
            point = pivot + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        return point;
    }
}