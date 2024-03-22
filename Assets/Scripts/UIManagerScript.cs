using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{

    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    public GameObject gameOverUI;
    public GameObject gameCompletedUI;  
    // Start is called before the first frame update
    
    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        

    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }



    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }


    public void CharacterTookDamage(GameObject character, int damageRecieved)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //Instantiate copy of prefab and set text on it

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageRecieved.ToString();
    }

    public void CharacterHealed(GameObject character, int healthAmaountRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //Instantiate copy of prefab and set text on it

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthAmaountRestored.ToString();
    }

       public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif

            #if (UNITY_EDITOR)
                    SceneManager.LoadScene("ExitScene");
            #elif (UNITY_STANDALONE)
                   SceneManager.LoadScene("ExitScene");
            #elif (UNITY_WEBGL)
                    SceneManager.LoadScene("ExitScene");
            #elif (UNITY_WEBGL)
                    SceneManager.LoadScene("ExitScene");
            #endif
        }
    }
    

    

    public void gameOver(){
        gameOverUI.SetActive(true);
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit(){
        Application.Quit();
    }

    public void gameCompleted(){
        Debug.Log("Game Completed");
        gameCompletedUI.SetActive(true);
    }


}
