using UnityEngine; // Import the Unity Engine namespace
using UnityEngine.UI; // Import the UI namespace for handling UI elements
using UnityEngine.SceneManagement; // Import the SceneManagement namespace for handling scene transitions

public class QuestionManager : MonoBehaviour // Define the QuestionManager class which inherits from MonoBehaviour
{
    public GameObject[] questions; // Array to hold the questions, to be assigned in the Unity Inspector
    private int currentQuestionIndex = 0; // Variable to track the current question index

    // Variables to hold scores for each category
    private int EScore = 0;
    private int IScore = 0;
    private int NScore = 0;
    private int SScore = 0;
    private int FScore = 0;
    private int TScore = 0;
    private int JScore = 0;
    private int PScore = 0;

    void Start() // Unity's Start method, called on the frame when a script is enabled just before any of the Update methods are called the first time
    {
        ShowQuestion(currentQuestionIndex); // Call ShowQuestion method to display the first question
    }

    public void ShowQuestion(int index) // Method to show a specific question based on index
    {
        for (int i = 0; i < questions.Length; i++) // Start with i = 0 and if its less than question length then keep adding 1 to i
        {
            questions[i].SetActive(i == index); // Set to active if i(loop) == to index(current question triggered by NextQuestion function)
        }
    }

    // The following methods handle button clicks for different responses.
    // Each method logs the selected response, increments the appropriate score, and then calls NextQuestion to move to the next question.

    public void OnButtonEClicked()
    {
        Debug.Log("Selected: Extroversion"); // Log the Extroversion response
        EScore++; // Increment the EScore
        NextQuestion(); // Move to the next question
    }

    public void OnButtonIClicked()
    {
        Debug.Log("Selected: Introversion"); // Log the Introversion response
        IScore++; // Increment the IScore
        NextQuestion(); // Move to the next question
    }

    public void OnButtonNClicked()
    {
        Debug.Log("Selected: Intuition"); // Log the Intuition response
        NScore++; // Increment the NScore
        NextQuestion(); // Move to the next question
    }

    public void OnButtonSClicked()
    {
        Debug.Log("Selected: Sensing"); // Log the Sensing response
        SScore++; // Increment the SScore
        NextQuestion(); // Move to the next question
    }

    public void OnButtonFClicked()
    {
        Debug.Log("Selected: Feeling"); // Log the Feeling response
        FScore++; // Increment the FScore
        NextQuestion(); // Move to the next question
    }

    public void OnButtonTClicked()
    {
        Debug.Log("Selected: Thinking"); // Log the Thinking response
        TScore++; // Increment the TScore
        NextQuestion(); // Move to the next question
    }

    public void OnButtonJClicked()
    {
        Debug.Log("Selected: Judging"); // Log the Judging response
        JScore++; // Increment the JScore
        NextQuestion(); // Move to the next question
    }

    public void OnButtonPClicked()
    {
        Debug.Log("Selected: Perceiving"); // Log the Perceiving response
        PScore++; // Increment the PScore
        NextQuestion(); // Move to the next question
    }

    private void NextQuestion() // Method to show the next question
    {
        if (currentQuestionIndex < questions.Length - 1) // Check if there are more questions left.
        {
            currentQuestionIndex++; // Increment the question index
            ShowQuestion(currentQuestionIndex); // Show the next question
        }
        else
        {
            CalculateResult(); // All questions answered, proceed to calculate the result
        }
    }

    private void CalculateResult() // Method to calculate and display the result
    {
        string mbtiResult = ""; // Initialize the MBTI result string

        // Determine the result based on the scores
        mbtiResult += (EScore > IScore) ? "E" : "I";
        mbtiResult += (NScore > SScore) ? "N" : "S";
        mbtiResult += (FScore > TScore) ? "F" : "T";
        mbtiResult += (JScore > PScore) ? "J" : "P";

        PlayerPrefs.SetString("MBTI_Result", mbtiResult); // Save the result using PlayerPrefs
        SceneManager.LoadScene("Result"); // Load the Result scene
    }
}