using System;
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

    private void Start()
    {
        stPrim = true;
        stMed = false;
        stCont = false;
        stMod = false;

    }
    private void Update()
    {
        if ((Contador.instance.era == "Pr�Hist�rica")&&(stPrim == true))
        {
            structure = Instantiate(structurePrim[0]);
            stPrim = false;
            stMed = true;
        }
        if ((Contador.instance.era == "Medieval")&& (stMed == true))
        {
            Destroy(structure);
            structure = Instantiate(structureMed[0]);
            stMed = false;
            stCont = true;
        }
        if ((Contador.instance.era == "Contempor�nea")&&(stCont == true))
        {
            Destroy(structure);
            structure = Instantiate(structureCont[0]);
            stCont = false;
            stMod = true;
        }
        if ((Contador.instance.era== "Moderna")&&(stMod == true))
        {
            Destroy(structure);
            structure = Instantiate(structureMod[0]);
            stMod = false;
        }





    }

    //identificar cada prefab para cada era (FEITO)


    //definir uma �rea para gerar o prefab  (A FAZER)


    //instanciar o prefab, dentro da �rea definida, apenas se n�o houver outro prefab na �rea (A FAZER)


    //checar se o boxcolider dos prefabs est�o distantes um do outro (A FAZER)


    //verificar se a boxcolider esta em contato com o ch�o (A FAZER)


    //caso o prefab gere dentro do ch�o, destruir ele (A FAZER)


    //ao mudar de era, substituir os prefabs pelos prefabs da era seguinte (FEITO)
}
