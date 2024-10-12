using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SpikeMove : MonoBehaviour
{
    private enum RepeatMode { None, Restart, Yoyo }

    [HideInInspector] public Vector3 startingPosition;
    [HideInInspector] public Tween moveTween;

    [Header("Property")]
    public Vector3 moveToPosition = Vector3.one;
    [SerializeField] public float moveTime = 1f;
    [SerializeField] private RepeatMode repeatMode = RepeatMode.Yoyo;
    [SerializeField] private Ease easeType = Ease.InOutSine;
    [SerializeField] public bool autoStart = true;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        SetMoveTween(moveToPosition);
        if (!autoStart) moveTween?.Pause();
        moveTween?.SetAutoKill(false);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            startingPosition = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startingPosition, 0.1f);
        Gizmos.DrawLine(startingPosition, startingPosition + moveToPosition);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startingPosition + moveToPosition, 0.1f);
    }

    public void SetMoveTween(Vector3 moveToPosition)
    {
        moveTween?.Kill();
        transform.position = startingPosition;
        this.moveToPosition = moveToPosition;
        moveTween = transform.DOMove(startingPosition + this.moveToPosition, moveTime).SetEase(easeType).SetAutoKill(false);
        if (repeatMode == RepeatMode.None) return;
        else if (repeatMode == RepeatMode.Restart) moveTween.SetLoops(-1, LoopType.Restart);
        else moveTween.SetLoops(-1, LoopType.Yoyo);
    }

    public void UpdateMoveTween()
    {
        moveTween?.Kill();
        transform.position = startingPosition;
        moveTween = transform.DOMove(startingPosition + moveToPosition, moveTime).SetEase(easeType).SetAutoKill(false);
        if (repeatMode == RepeatMode.None) return;
        else if (repeatMode == RepeatMode.Restart) moveTween.SetLoops(-1, LoopType.Restart);
        else moveTween.SetLoops(-1, LoopType.Yoyo);
    }

    public void Run()
    {
        //UpdateMoveTween();
        moveTween?.Play();
    }

    public void RunBackwards()
    {
        //UpdateMoveTween();
        moveTween?.PlayBackwards();
    }

    public void RunForwards()
    {
        //UpdateMoveTween();
        moveTween?.PlayForward();
    }

    public void Stop()
    {
        //UpdateMoveTween();
        moveTween?.Pause();
    }

    public void Restart()
    {
        UpdateMoveTween();
        moveTween?.Restart();
    }

    public Tween GetTween() => moveTween;
    public void SetTween(Tween tween) => moveTween = tween;
}

#if UNITY_EDITOR
[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{
    public void OnSceneGUI()
    {
        MovingPlatform linkedObject = target as MovingPlatform;
        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.DoPositionHandle(linkedObject.startingPosition + linkedObject.moveToPosition, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Update Move-to-position");
            linkedObject.moveToPosition = newPosition - linkedObject.startingPosition;
            linkedObject.SetMoveTween(linkedObject.moveToPosition);
        }
    }

    public override void OnInspectorGUI()
    {
        MovingPlatform movingPlatform = target as MovingPlatform;
        DrawDefaultInspector();
        if (GUI.changed)
        {
            movingPlatform.UpdateMoveTween();
        }
    }
}
#endif
