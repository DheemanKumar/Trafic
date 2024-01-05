using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    actionValue1 AV;

    public double[] state=new double[] {1,0,0,0};



    // Start is called before the first frame update

    void Start(){
        T1();
        T2();
        T3();
        T4();
        T5();
        T6();
        T7();
        T8();
        T9();
        T10();
    }


    void T1(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,1);
        AV.set_Action(state,1,1);
        AV.set_Action(state,1,1);
        AV.set_Action(state,1,1);
        AV.set_Action(state,1,1);

        int ans=AV.get_action(state);

        if (ans==1){
            Debug.Log("T1    PASS");
        }
        else {
            Debug.Log("T1    FAIL");
        }
    }

    void T2(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);

        int ans=AV.get_action(state);

        if (ans==0){
            Debug.Log("T2    PASS");
        }
        else {
            Debug.Log("T2    FAIL");
        }
    }

    void T3(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);

        int ans=AV.get_action(state);

        if (ans==0){
            Debug.Log("T3    PASS");
        }
        else {
            Debug.Log("T3    FAIL");
        }
    }

    void T4(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,0,-1);
        AV.set_Action(state,0,-1);
        AV.set_Action(state,0,-1);
        AV.set_Action(state,0,-1);
        AV.set_Action(state,0,-1);

        int ans=AV.get_action(state);

        if (ans==1){
            Debug.Log("T4    PASS");
        }
        else {
            Debug.Log("T4    FAIL");
        }
    }

    void T5(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,1);
        AV.set_Action(state,1,1);
        AV.set_Action(state,1,0);
        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);

        int ans=AV.get_action(state);

        if (ans==0){
            Debug.Log("T5    PASS");
        }
        else {
            Debug.Log("T5    FAIL");
        }
    }

    void T6(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);
        AV.set_Action(state,0,-1);
        AV.set_Action(state,0,-1);

        int ans=AV.get_action(state);

        if (ans==0){
            Debug.Log("T6    PASS");
        }
        else {
            Debug.Log("T6    FAIL");
        }
    }


    void T7(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,1);
        AV.set_Action(state,1,1);
        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);

        int ans=AV.get_action(state);

        if (ans==0){
            Debug.Log("T7    PASS");
        }
        else {
            Debug.Log("T7    FAIL");
        }
    }


    void T8(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);
        AV.set_Action(state,0,0);
        AV.set_Action(state,0,-1);
        AV.set_Action(state,0,-1);

        int ans=AV.get_action(state);

        if (ans==0){
            Debug.Log("T8    PASS");
        }
        else {
            Debug.Log("T8    FAIL");
        }
    }

    void T9(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,1);
        AV.set_Action(state,1,1);
        AV.set_Action(state,0,0);
        AV.set_Action(state,0,1);
        AV.set_Action(state,0,1);

        int ans=AV.get_action(state);

        if (ans==1){
            Debug.Log("T9    PASS");
        }
        else {
            Debug.Log("T9    FAIL");
        }
    }

    void T10(){
        AV=new actionValue1(4,2,0);
        AV.get_action(state);

        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,-1);
        AV.set_Action(state,1,0);
        AV.set_Action(state,0,-1);
        AV.set_Action(state,0,-1);

        int ans=AV.get_action(state);

        if (ans==1){
            Debug.Log("T10    PASS");
        }
        else {
            Debug.Log("T10    FAIL");
        }
    }
}
