using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLOCK : MonoBehaviour
{
    public FLOCKMANAGER myManager;
    float speed;
    bool turning = false;//define os desvio dos peixes com alguma colisão.
    
    void Start()
    {
        speed = Random.Range(myManager.minSpeed,
            myManager.maxSpeed);
        //randomiza a velocidade do peixe.
    }
    void ApplyRules()//aplica regras
    {
        GameObject[] gos; //Lista de peixes.
        gos = myManager.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach(GameObject go in gos)//define o grupo de peixes, que quando mexe com o neighbour, ele faz com que os peixes se distanciam e voltam em grupos. 
        {
            if(go!= this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    FLOCK anotherFlock = go.GetComponent<FLOCK>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
                    
            }
            if(groupSize>0)//define a posição do grupo formado.
            {
                vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
                speed = gSpeed / groupSize;

                Vector3 direction = (vcentre + vavoid) - transform.position;

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp
                    (
                        transform.rotation,
                        Quaternion.LookRotation(direction),
                        myManager.rotatioSpeed * .0005f 
                    );

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);//Cria uma area na posição do myManager com o tamanh da area do swinLimites.
        RaycastHit hit = new RaycastHit();//Direção do pilar entre os peixes.
        Vector3 direction = myManager.transform.position - transform.position;//Se o peixe nao estiver dentro da area, turning para verdadeiro.

        if (!b.Contains(transform.position))
        {
            turning = true;//pega direção do peixe com o pilar
            direction = myManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
            turning = false;
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotatioSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    

    }

}
