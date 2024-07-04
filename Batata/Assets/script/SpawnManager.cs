using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] structurePrim;
    [SerializeField]
    GameObject[] structureMed;
    [SerializeField]
    GameObject[] structureCont;
    [SerializeField]
    GameObject[] structureMod;

    bool stPrim;
    bool stMed;
    bool stCont;
    bool stMod;

    GameObject structure;

    int stuPrim;
    int stuMed;
    int stuCont;
    int stuMod;

    int contPrim;
    int contMed;
    int contMod;
    int contCont;

    private GameObject[] spawnedPrimStructures;
    private GameObject[] spawnedMedStructures;
    private GameObject[] spawnedContStructures;
    private GameObject[] spawnedModStructures;

    private void Start()
    {
        stPrim = true;
        stMed = false;
        stCont = false;
        stMod = false;

        contPrim = 0;
        contMed = 0;
        contCont = 0;
        contMod = 0;

        stuPrim = Random.Range(4, 10);
        stuMed = Random.Range(4, 10);
        stuCont = Random.Range(4, 10);
        stuMod = Random.Range(4, 10);

        spawnedPrimStructures = new GameObject[stuPrim];
        spawnedMedStructures = new GameObject[stuMed];
        spawnedContStructures = new GameObject[stuCont];
        spawnedModStructures = new GameObject[stuMod];
    }
    private void Update()
    {
        if ((Contador.instance.era == "Pr�Hist�rica") && (stPrim == true) && (contPrim != stuPrim))
        {

            do
            {
                structure = Instantiate(structurePrim[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);
                spawnedPrimStructures[contPrim] = structure;
                contPrim++;
            }
            while (contPrim != stuPrim);
            stMed = true;
            stPrim = false;


        }
        if ((Contador.instance.era == "Medieval") && (stMed == true) && (contMed != stuMed))
        {
            DestroyAllPrimStructures();
            do
            {
                structure = Instantiate(structureMed[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);
                spawnedMedStructures[contMed] = structure;
                contMed++;
            }
            while (contMed != stuMed);
            stMed = false;
            stCont = true;
        }
        if ((Contador.instance.era == "Contempor�nea") && (stCont == true) && (contCont != stuCont))
        {
            DestroyAllMedStructures();
            do
            {
                structure = Instantiate(structureCont[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);
                spawnedContStructures[contCont] = structure;
                contCont++;
            }
            while (contCont != stuCont);
            stCont = false;
            stMod = true;
        }
        if ((Contador.instance.era == "Moderna") && (stMod == true) && (contMod != stuMod))
        {
            DestroyAllContStructures();
            do
            {
                structure = Instantiate(structureMod[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);
                spawnedModStructures[contMod] = structure;
                contMod++;
            }
            while (contMod != stuMod);
            stMod = false;
        }
    }
        private void DestroyAllPrimStructures()
        {
            for (int i = 0; i < contPrim; i++)
            {
                Destroy(spawnedPrimStructures[i]);
            }
            contPrim = 0;
        }

        private void DestroyAllMedStructures()
        {
            for (int i = 0; i < contMed; i++)
            {
                Destroy(spawnedMedStructures[i]);
            }
            contMed = 0;
        }

        private void DestroyAllContStructures()
        {
            for (int i = 0; i < contCont; i++)
            {
                Destroy(spawnedContStructures[i]);
            }
            contCont = 0;
        }

        private void DestroyAllModStructures()
        {
            for (int i = 0; i < contMod; i++)
            {
                Destroy(spawnedModStructures[i]);
            }
            contMod = 0;
        }
    }       






    //identificar cada prefab para cada era (FEITO)


    //definir uma �rea para gerar o prefab  (Feito)


    //instanciar o prefab, dentro da �rea definida, apenas se n�o houver outro prefab na �rea (A FAZER)


    //checar se o boxcolider dos prefabs est�o distantes um do outro (A FAZER)


    //verificar se a boxcolider esta em contato com o ch�o (A FAZER)


    //caso o prefab gere dentro do ch�o, destruir ele (A FAZER)


    //ao mudar de era, substituir os prefabs pelos prefabs da era seguinte (FEITO)

