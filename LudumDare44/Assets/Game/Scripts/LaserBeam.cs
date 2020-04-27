using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float decayRate = 0.1f;
    public Color startColour;
    public Color endColour;

    private float alpha = 1.0f;

    private LineRenderer lineRenderer;

    private Vector3 _start;
    private Vector3 _end;

    public void Set(Vector3 start, Vector3 end)
    {
        _start = start;
        _end = end;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        if (lineRenderer)
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = startColour;
            lineRenderer.endColor = endColour;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer)
        {
            if (alpha <= 0)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                Gradient colour = lineRenderer.colorGradient;

                for (int i = 0; i < colour.alphaKeys.Length; ++i)
                {
                    colour.alphaKeys[i].alpha = alpha;
                }

                Color start = lineRenderer.startColor;
                start.a = alpha;

                Color end = lineRenderer.endColor;
                end.a = alpha;

                lineRenderer.startColor = start;
                lineRenderer.endColor = end;

                Vector3 dir = _end - _start;
                dir = Vector3.Normalize(dir);

                _start += (dir * 1.5f);
                lineRenderer.SetPosition(0, _start);

                alpha -= decayRate * Time.deltaTime;
            }
        }
    }
}
