using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue; //valor da roleta
    private float timeInterval; // tempo para trocar entre um item e outro

    public bool rowStopped; //verificar se o slot(row) parou
    public string stoppedSlot; //armazena o nome da imagem que parou na tela
    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;
        randomValue = Random.Range(60,100);
        //para corrigir a posi��o do item na tela
        switch (randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;
            case 3:
                randomValue += 2;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if(transform.position.y <= -3.5f)
            {
                transform.position = new Vector2(transform.position.x, 1.75f);
            }
            transform.position = new Vector2(transform.position.x, transform.position.y-0.25f);
            if (i > Mathf.RoundToInt(randomValue * 0.25f)) timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f)) timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f)) timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f)) timeInterval = 0.2f;
            yield return new WaitForSeconds(timeInterval);
        }
        if (transform.position.y == -3.5f) stoppedSlot = "Diamond";
        if (transform.position.y == -2.75f) stoppedSlot = "ExplodindoVaca";
        if (transform.position.y == -2f) stoppedSlot = "Melon";
        if (transform.position.y == -1.25f) stoppedSlot = "AlexMamed";
        if (transform.position.y == 0.5f) stoppedSlot = "Seven";
        if (transform.position.y == 0.25f) stoppedSlot = "Cherry";
        if (transform.position.y == 1f) stoppedSlot = "Dfilitto";
        if (transform.position.y == 1.75f) stoppedSlot = "Diamond";

        rowStopped = true;
    }

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }
}
