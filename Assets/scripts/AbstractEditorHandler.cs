using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// エディターハンドラのデフォルト実装
/// </summary>
public class AbstractEditorHandler : MonoBehaviour, IEditorHandler
{
    /// <summary>
    /// エディタオブジェクト
    /// </summary>
    public EditorController Editor
    {
        get;
        private set;
    }

    public virtual void OnEditorInitialize(EditorController editor)
    {
        this.Editor = editor;
    }

    public virtual void OnAddPoint(HyperPoint point)
    {
        // do nothing
    }

    public virtual void OnAddTriangle(HyperTriangle triangle)
    {
        // do nothing
    }

    public virtual void OnDeselectPoint(HyperPoint point)
    {
        // do nothing
    }

    public virtual void OnMovePoint(HyperPoint point)
    {
        // do nothing
    }

    public virtual void OnRemovePoint(HyperPoint point)
    {
        // do nothing
    }

    public virtual void OnRemoveTriangle(HyperTriangle triangle)
    {
        // do nothing
    }

    public virtual void OnSelectPoint(HyperPoint point)
    {
        // do nothing
    }
}
