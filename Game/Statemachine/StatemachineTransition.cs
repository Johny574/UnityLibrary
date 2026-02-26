
using System;

public class StatemachineTrasition<T>
{
    public T From { get; private set; }
    public T To { get; private set; }
    public Func<bool> Condition { get; private set; }
    public StatemachineTrasition(T from, T to, Func<bool> condition){
        From = from;
        To = to;
        Condition = condition;
    }
}