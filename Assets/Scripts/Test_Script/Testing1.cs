//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Testing1 : MonoBehaviour //andra script kan g� in och kolla p� den h�r klassen
//                                      //public

////SKYDDSNIV�ER:
////class -> private (bara synligt i detta script)
////Private som m�jligt: b�sta s�ttet att g�ra saker p� om saker bara inte ska kunna f�r�ndras utifr�n
////private och  [SerializeField]

//{

//    //Med dessa datatyper (variabler) klarar vi oss relativt l�ngt!
//    public int nummer = 25; // utan v�rde blir det noll (standardv�rde) int: hela nummer
//    private float decimalTal = 2.5f;
//    private string characterName = "Mario"; //Vill vi f� ut objektet �nd� kan [SerializeField] skrivas in.
//    public string otherName = "TestObject";
//    public bool thisIsTrue = true;
//    //egentligen ska vi skriva private om vi inte har en v�ldigt god anledning att inte skriva private



//    // Start is called before the first frame update
//    void Start()
//    {
//        if (thisIsTrue == true)
//        {
//            print("True");
//        }
//        else
//        {
//            print("False");
//        }
//        //print �r en inbyggd funktion i Unity som skriver ut v�rden i konsolen
//        print(characterName);

//        for (int i = 0; i < nummer; i++) //++ -> kortversion av += 1 addera ett och tilldela den till variabeln
//        {

//            print(nummer);
//            nummer++;
//        }

        

//    }

//    // Update is called once per frame
//    void Update()
//    {
        

//    }
//}
