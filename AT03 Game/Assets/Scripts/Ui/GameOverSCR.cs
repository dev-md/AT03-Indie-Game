using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSCR : MonoBehaviour
{
    private GameObject playerGame;
    private EnemySCR enemy;

    [SerializeField] private GameObject gameoverCanvas;
    // Start is called before the first frame update

    private void Awake()
    {
        gameoverCanvas.SetActive(false);
    }
    void Start()
    {
        TryGetComponent<EnemySCR>(out enemy);
        playerGame = enemy.playerGameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("FOUND");
        if (Vector3.Distance(transform.position, playerGame.transform.position) < 2f)
        {
            gameoverCanvas.SetActive(true);
            enemy.playerGameObject.SetActive(false);
        }
    }
}