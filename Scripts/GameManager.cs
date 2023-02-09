using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class GameManager : MonoBehaviour {
    
    private Respuesta respuesta; // lista - questions

    public Pregunta pregunta; // clase - question

    public int peticion = 10;

    void Awake() { 
        // A correct website page.
        //https://opentdb.com/api.php?amount=1&category=31&difficulty=easy&type=boolean
        StartCoroutine(GetRequest($"https://opentdb.com/api.php?amount={peticion}")); //REVISAR https://opentdb.com/api.php?amount=10&category=15&type=boolean

        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest(string uri) {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

           /* string[] pages = uri.Split('/'); //Eliminar el Split

            int page = pages.Length - 1; */
            
            //Cambiado el switch por un if
            if (webRequest.result == UnityWebRequest.Result.Success) {
                CargarJson(webRequest.downloadHandler.text);
            } else {
                Debug.Log("Error");
            }
        }
    }
               /* case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break; */

            public void CargarJson(string jsonString) {

            respuesta = JsonUtility.FromJson<Respuesta>(jsonString);
            pregunta = respuesta.results[0];

           /* Debug.Log(respuesta.response_code);
            Debug.Log(respuesta.results[0].difficulty);
            Debug.Log(respuesta.results[0].category);
            Debug.Log(respuesta.results[0].type);
            Debug.Log(respuesta.results[0].question); */

            //Añadir 1 dato a mayores

            Debug.Log($"Preguntas pedidas a la API : {peticion}");
            
            Debug.Log("4 Datos de la 1º pregunta");

            Debug.Log($"Categoria : {pregunta.category}");
            Debug.Log($"Tipo : {pregunta.type}");
            Debug.Log($"Dificultad : {pregunta.difficulty}");

            Debug.Log($"Pregunta : {pregunta.question}");

            }
        }
