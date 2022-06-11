using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLOCKMANAGER : MonoBehaviour
{
    public GameObject fishPrefabab;//O GameObject prefab do peixe.
    public int numFish = 20;//quantidade dos peixes.
    public GameObject[] allFish;//todos os peixes.
    public Vector3 swinLimits = new Vector3(5, 5, 5);//Instanciamento dos peixes(limite de 5,5,5 da tela).
    public Vector3 goalPos;

    [Header("Configura��es do Cardume")]//Onde aparece as configura��es na Unity.
    [Range(0.0f, 5.0f)]//velocidade m�nima .
    public float minSpeed;
    [Range(0.0f, 5.0f)]//velocidade maxima.
    public float maxSpeed;
    [Range(1.0f, 10.0f)]//a distancia que tem dos peixes, posso escolher o deslocamento deles com os vizinhos.
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]//rota��o dos peixes.
    public float rotatioSpeed;


    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];//Instacializando esse Array [numFish] com prefab(allFish), fazendo com que apare�a em lugares aleatorios com limites(SwinLimits).
        for(int i=0; i<numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefabab, pos, Quaternion.identity);
            allFish[i].GetComponent<FLOCK>().myManager = this;

        }
    }

    // Update is called once per frame
    void Update()
    {
        goalPos = this.transform.position; //Define o ponto dos peixes rodando.
        if (Random.Range(0, 100) < 10)//introduz varia��es onde os peixes est�o orbitando.
        {
            goalPos = this.transform.position + new Vector3(
                Random.Range(-swinLimits.x, swinLimits.x), 
                Random.Range(-swinLimits.y, swinLimits.y),
                Random.Range(-swinLimits.z, swinLimits.z)
            );
        }
    }
}
