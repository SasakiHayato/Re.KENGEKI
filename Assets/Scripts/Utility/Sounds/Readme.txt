SoundOparatorの使用方法

Setupについて

1. Hierarchy上に Asset/Utility/Sounds/Prefabs にあるプレファブを配置
2. SoundOperatorにDataBaseをアタッチする。

Requestについて

参考 => サンプルのUserを参照




SoundOperater.PlayRequesterについて

必須記述
SetPath(サウンドのデータパス); 
SetType(サウンドのデータタイプ);
Request();　データをセットした後で呼び出す。

任意記述
SetUser(使用者); 使用者の有無を設定





SoundOperater.StopRequesterについて

必須記述
CreateData(); データのインスタンスを作成
Request(); データをセットした後で呼び出す。

任意記述
CurrentBGM(); 現在再生中のBGMを止める。
SetPath(サウンドのデータパス); 指定したSoundを止める。
SetAll(); 全てのSoundを止める。