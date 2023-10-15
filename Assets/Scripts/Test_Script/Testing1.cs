//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Testing1 : MonoBehaviour //andra script kan gå in och kolla på den här klassen
//                                      //public

////SKYDDSNIVÅER:
////class -> private (bara synligt i detta script)
////Private som möjligt: bästa sättet att göra saker på om saker bara inte ska kunna förändras utifrån
////private och  [SerializeField]

//{

//    //Med dessa datatyper (variabler) klarar vi oss relativt långt!
//    public int nummer = 25; // utan värde blir det noll (standardvärde) int: hela nummer
//    private float decimalTal = 2.5f;
//    private string characterName = "Mario"; //Vill vi få ut objektet ändå kan [SerializeField] skrivas in.
//    public string otherName = "TestObject";
//    public bool thisIsTrue = true;
//    //egentligen ska vi skriva private om vi inte har en väldigt god anledning att inte skriva private



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
//        //print är en inbyggd funktion i Unity som skriver ut värden i konsolen
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
