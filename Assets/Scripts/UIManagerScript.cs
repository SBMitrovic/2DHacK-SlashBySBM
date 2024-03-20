using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManagerScript : MonoBehaviour
{

    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
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
                    UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                    Application.Quit();
            #elif (UNITY_WEBGL)
                    SceneManager.LoadScene("QuitScene");
            #endif
        }
    }
}
