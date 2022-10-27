using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour : ScriptableObject
{
    //[SerializeField] AnimationClip animationClip;
    [SerializeField] protected List<BaseBehaviour> transitions;
    protected EnemyController controller;
    public abstract bool Statement(EnemyController controller);
    public virtual void OnStart(EnemyController controller)
    {
        this.controller = controller;
    }
    public virtual void OnExit(BaseBehaviour newBehaviour)
    {
        newBehaviour.OnStart(controller);
        controller.Current = newBehaviour;
    }
    public virtual void OnUpdate()
    {
        CheckChange();
    }
    protected virtual void CheckChange()
    {
        foreach (BaseBehaviour item in transitions)
        {
            if (item.Statement(controller))
                controller.Current.OnExit(item);
        }
    }
}