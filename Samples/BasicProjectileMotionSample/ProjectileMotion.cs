using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    [SerializeField] float _angle;
    [SerializeField] float _initialVelocity;
    [SerializeField] float _height;
    [SerializeField] LineRenderer _lineRenderer;


    void Update() {
        // float angle = _angle * Mathf.Deg2Rad;
        float angle;
        float v0;
        float time;
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _height = target.y + target.magnitude / 2f;
        _height = Mathf.Max(0.01f, _height);
        // CalculatePath(target, angle, out v0, out time);
        CalculatePathWithHeight(target, _height, out v0, out angle, out time);
        DrawArc(v0, angle, time, 0.1f);
    }

    void DrawArc(float v0, float angle, float totalTime, float step)
    {
        step = Mathf.Max(0.01f, step);
        int count = 0;
        _lineRenderer.positionCount = (int)(totalTime / step) + 2;
        for (float i = 0; i < totalTime; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y  * Mathf.Pow(i, 2);
            _lineRenderer.SetPosition(count, new Vector3(x,y,0));
            count ++;
        }
        float xFinal = v0 * totalTime * Mathf.Cos(angle);
        float yFinal = v0 * totalTime * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y  * Mathf.Pow(totalTime, 2);
        _lineRenderer.SetPosition(count, new Vector3(xFinal,yFinal,0));
    }

    void CalculatePath(Vector3 targetPos, float angle, out float v0, out float t)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt, 2) * g;
        float v2 = 2 * xt * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);

        v0 = Mathf.Sqrt(v1 / (v2 - v3));
        t = xt / (v0*Mathf.Cos(angle));
    }

    float QuadriticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    void CalculatePathWithHeight(Vector3 targetpos, float h, out float v0, out float angle, out float t)
    {
        float xt = targetpos.x;
        float yt = targetpos.y;
        float g = -Physics.gravity.y;
        float b =Mathf.Sqrt(2 * g * h);
        float a = -.5f * g;
        float c = -yt;

        float tplus = QuadriticEquation(a, b, c, 1);
        float tmin = QuadriticEquation(a,b,c, -1);

        t = tplus > tmin ? tplus : tmin;
        angle = Mathf.Atan(b * t / xt);
        v0 = b/Mathf.Sin(angle);
    }

}
