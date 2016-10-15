using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// エディタイベント処理対象のインターフェイス
/// </summary>
public interface IEditorHandler : IEventSystemHandler
{
    /// <summary>
    /// 初期化時の処理。エディターの参照保持などを行う。
    /// </summary>
    /// <param name="editor">イベント配信元となるエディター</param>
    void OnEditorInitialize(EditorController editor);

    /// <summary>
    /// ポイント追加時の処理
    /// </summary>
    /// <param name="point">追加されたポイント</param>
    void OnAddPoint(HyperPoint point);

    /// <summary>
    /// ポイント削除時の処理
    /// </summary>
    /// <param name="point">削除されるポイント</param>
    void OnRemovePoint(HyperPoint point);

    /// <summary>
    /// 点移動時の処理
    /// </summary>
    /// <param name="point">移動されたポイント</param>
    void OnMovePoint(HyperPoint point);

    /// <summary>
    /// ポイント選択時の処理
    /// </summary>
    /// <param name="point">選択されたポイント</param>
    void OnSelectPoint(HyperPoint point);

    /// <summary>
    /// ポイント非選択時の処理
    /// </summary>
    /// <param name="point">選択解除されたポイント</param>
    void OnDeselectPoint(HyperPoint point);

    /// <summary>
    /// 面追加時の処理
    /// </summary>
    /// <param name="triangle">追加された面</param>
    void OnAddTriangle(HyperTriangle triangle);

    /// <summary>
    /// 面削除時の処理
    /// </summary>
    /// <param name="triangle">削除された面</param>
    void OnRemoveTriangle(HyperTriangle triangle);
}
