/// <summary>
/// フィールドイベント中の行動制御。
/// フィールドイベントの使用者。
/// </summary>

public interface IFieldEventHandler
{
    /// <summary>
    /// イベント中であればTureを返す
    /// </summary>
    bool IsExecution { set; }
}
