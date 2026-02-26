
using UnityEngine;
using TMPro;

public class SimpleTextColorAnimation : MonoBehaviour
{
    public TMP_Text tmpText;
    public float speed = 2f; // how fast colors cycle

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

            Color32[] colors = textInfo.meshInfo[meshIndex].colors32;

            // Generate a rainbow color based on time and character index
            float hue = Mathf.Repeat(Time.time * speed + i * 0.1f, 1f);
            Color newColor = Color.HSVToRGB(hue, 1f, 1f);

            colors[vertexIndex + 0] = newColor;
            colors[vertexIndex + 1] = newColor;
            colors[vertexIndex + 2] = newColor;
            colors[vertexIndex + 3] = newColor;

            colors[vertexIndex + 0] = Color.red;    // bottom-left
            colors[vertexIndex + 1] = Color.green;  // top-left
            colors[vertexIndex + 2] = Color.blue;   // top-right
            colors[vertexIndex + 3] = Color.yellow; // bottom-right

        }

        // Push updated colors back to TMP
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.colors32 = meshInfo.colors32;
            tmpText.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
