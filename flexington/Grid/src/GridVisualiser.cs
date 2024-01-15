using UnityEngine;

namespace flexington.Grid
{
    public abstract class GridVisualiser<T>
    {
        public abstract void Visualise(T[,] grid, Transform parent = null, Vector2 cellSize = default, Vector2 origin = default);

        internal TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3))
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.text = text;
            textMesh.fontSize = 40;
            textMesh.color = Color.white;
            textMesh.characterSize = 0.1f;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = 1;
            return textMesh;
        }
    }
}