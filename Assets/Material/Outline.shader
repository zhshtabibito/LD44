Shader "Tiger/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Width("Edge Width", Range(-1,20)) = 5
    }
    SubShader
    {
        
        Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="true" }
        ZWrite Off
        Cull Off
        Blend One OneMinusSrcAlpha
        Pass{

        CGPROGRAM

        #pragma vertex vertexFunc
        #pragma fragment fragmentFunc
        #include "UnityCG.cginc"

        sampler2D _MainTex;

        struct v2f{
            float4 pos : SV_POSITION;
            half2 uv : TEXCO0RD0;
        };
        
        v2f vertexFunc(appdata_base v){
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }

        fixed4 _Color;
        float _Width;
        float4 _MainTex_TexelSize;

        fixed4 fragmentFunc(v2f i) : COLOR{
            half4 c = tex2D(_MainTex, i.uv);
            c.rgb *= c.a;
            half4 outlineC = _Color;
            //outlineC.a *= ceil(c.a);

            fixed upAlpha = tex2D(_MainTex, i.uv + fixed2(0, floor(_Width) * _MainTex_TexelSize.y)).a;
            fixed downAlpha = tex2D(_MainTex, i.uv - fixed2(0, floor(_Width) * _MainTex_TexelSize.y)).a;
            fixed leftAlpha = tex2D(_MainTex, i.uv + fixed2(floor(_Width) * _MainTex_TexelSize.x, 0)).a;
            fixed rightAlpha = tex2D(_MainTex, i.uv - fixed2(floor(_Width) * _MainTex_TexelSize.x, 0)).a;
            
            fixed judge = upAlpha + downAlpha + leftAlpha + rightAlpha;
            if(judge < 1)
            {
                return c;
            }else{
                return c +  outlineC.a * (outlineC * (1-c.a));
            }
            
        }

        ENDCG
        }
    }
}
