using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// カメラ全面パネルコントローラー
/// </summary>
public class SubCameraPanelController : MonoBehaviour, IPointerClickHandler
{
    // クリック箇所のカメラからの距離
    private const float CLICK_DISTANCE = 1.0f;

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
        Vector3 pos = new Vector3(eventData.position.x, eventData.position.y, 0.0f);
        Ray ray = camera_.ScreenPointToRay(pos);
        editorController_.OnClick(ray.GetPoint(CLICK_DISTANCE), gameObject.layer);
    }
}
