using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static bool isStartScreenLoaded = false;
    Image imgYou;       // your selected image (rock, paper, scissor)
    Image imgCom;       // computer selected image (rock, paper, scissor)

    TextMeshProUGUI txtYou;        // the score you win
    TextMeshProUGUI txtCom;        // the score computer win
    TextMeshProUGUI txtResult;     // the result

    int cntYou = 0;     // number you win
    int cntCom = 0;     // number computer win

    void CheckResult(int yourResult)
    {
        // Generate computer's choice
        int comResult = UnityEngine.Random.Range(1, 4);
        int k = yourResult - comResult;

        // Determine game result
        if (k == 0)
        {
            txtResult.text = "Draw.";
        }
        else if (k == 1 || k == -2) // You win
        {
            cntYou++;
            txtResult.text = "You win.";
        }
        else // Computer wins
        {
            cntCom++;
            txtResult.text = "Computer wins.";
        }

        // Update UI with the new score and result images
        SetResult(yourResult, comResult);

        // Check for win conditions (first to score 3 wins)
        if (cntYou >= 3)
        {
            SceneManager.LoadScene("WinScene");
        }
        else if (cntCom >= 3)
        {
            SceneManager.LoadScene("OverScene");
        }
    }

    void SetResult(int you, int com)
    {
        // Update selected images for player and computer
        imgYou.sprite = Resources.Load<Sprite>("img_" + you);
        imgCom.sprite = Resources.Load<Sprite>("img_" + com);

        if (imgYou.sprite == null || imgCom.sprite == null)
        {
            Debug.LogError("Failed to load one or more images: img_" + you + ", img_" + com);
        }

        // Invert the computer image horizontally
        imgCom.transform.localScale = new Vector3(-1, 1, 1);

        // Update the winning score on the UI
        txtYou.text = cntYou.ToString();
        txtCom.text = cntCom.ToString();
    }

    public void OnButtonClick(GameObject buttonObject)
    {
        // Triggered when a choice button is clicked
        int you = int.Parse(buttonObject.name.Substring(0, 1));
        CheckResult(you);
    }

    public void OnMouseExit(GameObject buttonObject)
    {
        // Triggered when the mouse exits a button area
        Animator anim = buttonObject.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("Normal"); // Switch to idle animation
        }
    }

    private void StartGame()
    {
        // Initialize images and text components
        imgYou = GameObject.Find("ImageYou").GetComponent<Image>();
        imgCom = GameObject.Find("ImageCom").GetComponent<Image>();

        txtYou = GameObject.Find("TxtYou").GetComponent<TextMeshProUGUI>();
        txtCom = GameObject.Find("TxtCom").GetComponent<TextMeshProUGUI>();
        txtResult = GameObject.Find("TxtResult").GetComponent<TextMeshProUGUI>();

        // Set initial text before the game starts
        txtResult.text = "Select the button below";
    }

    // Start is called before the first frame update
    void Start()
    {
        // Load the start screen only once
        if (!isStartScreenLoaded)
        {
            SceneManager.LoadScene("StartScene");
            isStartScreenLoaded = true;
        }

        // Initialize the game if in PlayingScene
        if (SceneManager.GetActiveScene().name == "PlayingScene")
        {
            StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Exit the game if the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            // Stop playing in the editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the application
            Application.Quit();
        #endif
    }
}
