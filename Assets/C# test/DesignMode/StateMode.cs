using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : #AUTHORNAME#
//DATE : #DATE#
//DESC : #DESC#
public class StateMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public abstract class State   //用于状态处理
{
    public abstract void Handle(Context context);
}
public class ConcreateStateA : State
{
    public override void Handle(Context context)
    {
        context.ChangeState(new ConcreateStateB());
    }
}
public class ConcreateStateB : State
{
    public override void Handle(Context context)
    {
        context.ChangeState(new ConcreateStateA());
    }
}
public class Context   //
{
    private State currentState;
    public void ChangeState(State state)
    {
        this.currentState = state;
        currentState.Handle(this);
    }
}
