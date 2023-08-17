using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{

    private Button _button;
    private GameManager gameManager;

    [Range(1,3)]
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);

        gameManager = FindObjectOfType<GameManager>();
    }

    void SetDifficulty()
    {
        Debug.Log(gameObject.name);
        gameManager.StartGame(difficulty);
    }
}
