using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private int unlockedLevels = 0;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement navigation = root.Query<VisualElement>("Navigation");
        int index = 1;
        foreach (var div in navigation.Children())
        {
            Button startButton = div.Query<Button>("Button");
            int sceneIndex = index;
            startButton.clicked += () => SceneManager.LoadScene(sceneIndex);
            index++;
        }
    }
}