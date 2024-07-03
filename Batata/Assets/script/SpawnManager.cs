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
    }
    private void Update()
    {
        if ((Contador.instance.era == "Pr�Hist�rica") && (stPrim == true)&&(contPrim != stuPrim))
        {

                do
                {
                    structure = Instantiate(structurePrim[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);

                    contPrim = contPrim + 1;
                }
                while (contPrim != stuPrim);
            stMed = true;


        }
        if ((Contador.instance.era == "Medieval")&& (stMed == true))
        {
            Destroy(structure);
            structure = Instantiate(structureMed[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);
            stMed = false;
            stCont = true;
        }
        if ((Contador.instance.era == "Contempor�nea")&&(stCont == true))
        {
            Destroy(structure);
            structure = Instantiate(structureCont[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);
            stCont = false;
            stMod = true;
        }
        if ((Contador.instance.era== "Moderna")&&(stMod == true))
        {
            Destroy(structure);
            structure = Instantiate(structureMod[0], new Vector3(Random.Range(31, 104), 0f, 0f), Quaternion.identity);
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
