using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// カメラ全面パネルコントローラー
/// </summary>
public class SubCameraPanelController : MonoBehaviour,
    IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera camera_;
    private EditorController editorController_;

    /// <summary>
    /// オブジェクト生成時の処理
    /// </summary>
    void Awake()
    {
        camera_ = GetComponentInParent<Camera>();
        editorController_ = GetComponentInParent<EditorController>();
    }

    /// <summary>
    /// クリック時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        editorController_.OnClick(GetEventRay(eventData), gameObject.layer);
    }

    /// <summary>
    /// ドラッグ開始
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        if (Physics.Raycast(GetEventRay(eventData), out hit, float.PositiveInfinity, CurrentLayerMask))
        {
            editorController_.OnBeginDrag(hit);
        }
    }

    /// <summary>
    /// ドラッグ中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        editorController_.OnDrag(GetEventRay(eventData), gameObject.layer);
    }

    /// <summary>
    /// ドラッグ終了
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        editorController_.OnEndDrag();
    }

    /// <summary>
    /// ポインターイベント発生個所からのRayを取得する。
    /// </summary>
    /// <param name="eventData">ポインターイベント</param>
    /// <returns>ポインターイベント発生個所からのRay</returns>
    private Ray GetEventRay(PointerEventData eventData)
    {
        Vector3 pos = new Vector3(eventData.position.x, eventData.position.y, 0.0f);
        return camera_.ScreenPointToRay(pos);
    }

    /// <summary>
    /// このオブジェクトの属するレイヤーのマスクを取得する。
    /// </summary>
    /// <returns>このオブジェクトの属するレイヤーのマスク</returns>
    private int CurrentLayerMask
    {
        get { return 1 << gameObject.layer; }
    }
}
