using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class UniRxTest : MonoBehaviour
{
    private void Awake()
    {
        // Subscribe1();
        // Subscribe2();
        // Subscribe3();
        // Subscribe4();
        Subscribe5();
    }

    private void Subscribe1()
    {
        Observable.CombineLatest(
            Observable.Interval(TimeSpan.FromSeconds(1)).Select(x =>
            {
                Debug.Log(1111);
                return x;
            }),
            Observable.Interval(TimeSpan.FromSeconds(2))
        )
        .First()
        .Subscribe(_ =>
        {
            Debug.Log(1112);
        })
        .AddTo(this);
    }

    private void Subscribe2()
    {
        Observable.CombineLatest(
            Observable.Interval(TimeSpan.FromSeconds(1)).Select(x =>
            {
                Debug.Log(1111);
                return x;
            }).First(),
            Observable.Interval(TimeSpan.FromSeconds(2)).First()
        )
        // .First()
        .Subscribe(_ =>
        {
            Debug.Log(1112);
        }, _ => Debug.Log("completed"))
        .AddTo(this);
    }

    private void Subscribe3()
    {
        var a = Observable.Interval(TimeSpan.FromSeconds(1)).Select(x =>
        {
            Debug.Log(1111);
            return x;
        });
        Observable.CombineLatest(
            a,
            Observable.Interval(TimeSpan.FromSeconds(2)).Select(x =>
            {
                Debug.Log(1112);
                return x;
            })
        )
        .First()
        .Subscribe(_ =>
        {
            Debug.Log(2111);
        }, _ => Debug.Log("completed"))
        .AddTo(this);
        a.Subscribe(_ => Debug.Log(3111), _ => Debug.Log(3112)).AddTo(this);
    }

    private void Subscribe4()
    {
        var a = Observable.Return(1111);
        a.Subscribe(x =>
        {
            Debug.Log(2111 + " " + x);
        })
        .AddTo(this);

        Observable.Return(Unit.Default)
        .Delay(TimeSpan.FromSeconds(1))
        .Subscribe(_ =>
        {
            a.Subscribe(x => Debug.Log(x)).AddTo(this);
        })
        .AddTo(this);
    }

    private void Subscribe5()
    {
        var a = new Subject<int>();
        a.Subscribe(x => Debug.Log(1111 + " " + x)).AddTo(this);

        a.OnNext(3);

        Observable.Return(Unit.Default)
        .Delay(TimeSpan.FromSeconds(1))
        .Subscribe(_ =>
        {
            a.Subscribe(x => Debug.Log(1112 + " " + x)).AddTo(this);
        })
        .AddTo(this);
    }
}
