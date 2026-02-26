using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTextAnimation : MonoBehaviour
{
    public TMP_Text tmpText;
    public float amplitude = 5f;   // height of bounce
    public float frequency = 5f;   // speed of bounce

    private TMP_TextInfo textInfo;

    void Start()
    {
        if (tmpText == null)
            tmpText = GetComponent<TMP_Text>();

        tmpText.ForceMeshUpdate();
        textInfo = tmpText.textInfo;
    }

    void Update()
    {
        tmpText.ForceMeshUpdate();
        textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            int meshIndex = textInfo.characterInfo[i].materialReferenceIndex;

            Vector3[] vertices = textInfo.meshInfo[meshIndex].vertices;

            // Apply sine wave offset to each character
            float offset = Mathf.Sin(Time.time * frequency + i) * amplitude;

            vertices[vertexIndex + 0].y += offset;
            vertices[vertexIndex + 1].y += offset;
            vertices[vertexIndex + 2].y += offset;
            vertices[vertexIndex + 3].y += offset;
        }

        // Push updated vertex data back to TMP
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            tmpText.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}

