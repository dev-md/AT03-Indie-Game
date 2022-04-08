using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSCR : MonoBehaviour
{
    private GameObject playerGame;
    private EnemySCR enemy;

    [SerializeField] private GameObject gameoverCanvas;
    [SerializeField] private GameObject playerGmVar;
    [SerializeField] private bool isMoving = false;
    // Start is called before the first frame update

    private void Awake()
    {
        gameoverCanvas.SetActive(false);
    }
    void Start()
    {
        if (playerGmVar == null)
        {
            TryGetComponent<EnemySCR>(out enemy);
            playerGame = enemy.playerGameObject;
        }
        else
        {
            playerGame = playerGmVar;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("FOUND");
        if(isMoving == true)
        {
            if (Vector3.Distance(transform.position, playerGame.transform.position) < 2f)
            {
                gameoverCanvas.SetActive(true);
                playerGame.SetActive(false);
            }
        }
        else if (isMoving == false)
        {
            //Debug.Log(playerGame.tag);
            //Debug.Log(other.gameObject.name);
            if (playerGame.tag == other.gameObject.tag)
            {
                gameoverCanvas.SetActive(true);
                playerGame.SetActive(false);
            }
        }
    }
}
