using System;
using TMPro;
using UnityEngine;

public class InteractionsTextRenderer : MonoBehaviour
{
    [SerializeField] string floatingText = "Press [E]";
    [SerializeField] float floatingHeight = 2f;

    public TextMeshPro textMesh;

    private void Awake()
    {
    }

    public void DisplayText(Vector3 position)
    {
        textMesh.gameObject.SetActive(true);
        textMesh.transform.position = new Vector3(position.x, position.y + floatingHeight, position.z);
    }

    public void UpdateText(string text)
    {
        textMesh.text = text;
    }

    public void UpdateHeight(float height)
    {
        floatingHeight = height;
    }

    public void HideText()
    {
        textMesh.gameObject.SetActive(false);
    }
}