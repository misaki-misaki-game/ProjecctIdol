Shader "Custom/BlinkShader" // シェーダーの名前を定義　"グループ名/シェーダー名"
{
    // 生成したマテリアルで入力できる項目
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {} // スプライトのテクスチャ
        _BlinkSpeed ("Blink Speed", Range(0, 10)) = 1 // 点滅速度のパラメータ
        _BlinkIntensity ("Blink Intensity", Range(0, 1)) = 1 // 点滅の強度のパラメータ
        _TransparencyMin ("TransparencyMin", Range(0,1)) = 0.1 // 点滅させないα値
    }
    // このシェーダーの主な処理
    SubShader
    {
        // タグを設定
        Tags
        {
            "RenderType"="Transparent" // レンダリングタイプを透明に設定
            "Queue"="Transparent"
           // "RenderPipeline"="UniversalPipeline" // レンダーパイプラインをURPに対応
        }

        // ブレンド設定を追加
        Blend SrcAlpha OneMinusSrcAlpha
        
        // [Level Of Detail] 描写レベル　
        LOD 100 // LOD値が100以上ならこのマテリアルを描写

        // 1つのオブジェクトの1度の描画で行う処理
        Pass
        {
            CGPROGRAM // シェーダーのCgコードブロックの開始
            #pragma vertex vert // 頂点シェーダー関数の指定
            #pragma fragment frag // フラグメントシェーダー関数の指定
            #include "UnityCG.cginc" // Unityの共通シェーダーインクルードファイルを読み込み

            // Unityから受け取るデータ構造体
            struct appdata_t
            {
                float4 vertex : POSITION; // 頂点の位置
                float2 uv : TEXCOORD0; // テクスチャ座標
            };

            // フラグメントシェーダーで使用する構造体
            struct v2f
            {
                float2 uv : TEXCOORD0; // フラグメントシェーダーに渡すテクスチャ座標
                float4 vertex : SV_POSITION; // クリップ空間での頂点の位置
            };

            sampler2D _MainTex; // メインテクスチャのサンプラー
            float4 _MainTex_ST; // テクスチャ座標の変換行列
            float _BlinkSpeed; // 点滅速度の変数
            float _BlinkIntensity; // 点滅強度の変数
            float _TransparencyMin; // 点滅させないα値


            // Unityから取得したデータを変換する関数(頂点シェーダーに関する関数)
            v2f vert (appdata_t v)
            {
                v2f o; // v2f構造体を生成
                o.vertex = UnityObjectToClipPos(v.vertex); // 頂点位置をクリップ空間に変換
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // テクスチャ座標を変換
                return o;
            }

            // Unityから取得したデータをフラグメントに反映させる関数(フラグメントシェーダーに関する関数)
            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv); // テクスチャから色をサンプリング
                if (col.a < _TransparencyMin) // アルファ値が低い部分（透明部分）には点滅を適用しない
                {
                    col.rgb = (0, 0, 0);
                    col.a = 0;
                    return col; // そのままの色を返す
                }
                float blink = abs(sin(_Time.y * _BlinkSpeed)) * _BlinkIntensity; // 点滅の計算 _TimeはUnityが自動的に提供する時間のパラメータ
                                                                                 // _Time.x...ゲーム時間の秒数 .y...秒数2倍 .z...3倍 .w...4倍
                col.rgb *= (1.0 - blink); // 点滅を適用 (透明部分には影響しない)
                col.a = 1;
                return col; // 計算結果の色を返す
            }
            ENDCG // シェーダーのCgコードブロックの終了
        }
    }
    FallBack "Transparent" // シェーダーがサポートされない場合に使用するフォールバックシェーダー
}