using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private int countToShowVideo=10;
    private int countplay;

    public static event Action HandlePulled = delegate { };

    private AudioSource audioPlayer;
    //public AudioClip musica1;
    //public AudioClip musica2;

    [SerializeField]
    private Text prizeText; //texto da recompensa

    [SerializeField]
    private Row[] rows; //representa a classe rows linha (slots)

    [SerializeField]
    private Transform handle; //cabo da máquina

    private int prizeValue;

    private bool resultsChecked; //verifica se os slots pararam
    void Start()
    {
        countplay = 0;
        resultsChecked = false;
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize " + prizeValue;
        }
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !audioPlayer.isPlaying)
        {
            Invoke("ShowVideo", 1f);
        }
    }
    public void ShowVideo()
    {
        //exibir propaganda de vídeos
        if (countplay >= countToShowVideo && prizeText.enabled == true)
        {
            countplay = 0;
        }
    }
    private void OnMouseDown()
    {    
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
            //conto a quantidade de jogadas
            countplay++;
        }
    }

    private IEnumerator PullHandle()
    {
        //animação da alavanca
        for (int i = 0; i < 15; i+=5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.01f);
        }

        audioPlayer.Play();
        //audioPlayer.PlayOneShot(musica2);
        HandlePulled();

        //animação da alavanca
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.01f);
        }
    }
    private void CheckResults()
    {
        //três iguais
        if (rows[0].stoppedSlot== "Diamond" && rows[1].stoppedSlot == "Diamond" && rows[2].stoppedSlot == "Diamond")
        {
            prizeValue = 200;
        }
        else if (rows[0].stoppedSlot == "ExplodindoVaca" && rows[1].stoppedSlot == "ExplodindoVaca" && rows[2].stoppedSlot == "ExplodindoVaca")
        {
            prizeValue = 400;
        }
        else if (rows[0].stoppedSlot == "Melon" && rows[1].stoppedSlot == "Melon" && rows[2].stoppedSlot == "Melon")
        {
            prizeValue = 600;
        }
        else if (rows[0].stoppedSlot == "AlexMamed" && rows[1].stoppedSlot == "AlexMamed" && rows[2].stoppedSlot == "AlexMamed")
        {
            prizeValue = 800;
        }
        else if (rows[0].stoppedSlot == "Seven" && rows[1].stoppedSlot == "Seven" && rows[2].stoppedSlot == "Seven")
        {
            prizeValue = 1500;
        }
        else if (rows[0].stoppedSlot == "Cherry" && rows[1].stoppedSlot == "Cherry" && rows[2].stoppedSlot == "Cherry")
        {
            prizeValue = 3000;
        }
        else if (rows[0].stoppedSlot == "Dfilitto" && rows[1].stoppedSlot == "Dfilitto" && rows[2].stoppedSlot == "Dfilitto")
        {
            prizeValue = 5000;
        }
        //dois iguais
        else if (
            rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot== "Diamond" ||
            rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot == "Diamond" ||
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[1].stoppedSlot == "Diamond"
            )
        {
            prizeValue = 100;
        }
        else if (
            rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot == "ExplodindoVaca" ||
            rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot == "ExplodindoVaca" ||
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[1].stoppedSlot == "ExplodindoVaca"
            )
        {
            prizeValue = 100;
        }
        else if (
            rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot == "Melon" ||
            rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot == "Melon" ||
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[1].stoppedSlot == "Melon"
            )
        {
            prizeValue = 500;
        }
        else if (
            rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot == "AlexMamed" ||
            rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot == "AlexMamed" ||
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[1].stoppedSlot == "AlexMamed"
            )
        {
            prizeValue = 700;
        }
        else if (
            rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot == "Seven" ||
            rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot == "Seven" ||
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[1].stoppedSlot == "Seven"
            )
        {
            prizeValue = 1000;
        }
        else if (
            rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot == "Cherry" ||
            rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot == "Cherry" ||
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[1].stoppedSlot == "Cherry"
            )
        {
            prizeValue = 2000;
        }
        else if (
            rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot == "Dfilitto" ||
            rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot == "Dfilitto" ||
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[1].stoppedSlot == "Dfilitto"
            )
        { 
            prizeValue = 4000; 
        }
        resultsChecked = true;
    }
}
