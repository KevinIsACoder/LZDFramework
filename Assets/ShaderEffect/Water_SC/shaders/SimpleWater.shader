// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Simple Water" {
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D ) = "white" {}
        _WiggleTex ("Base (RGB)", 2D) = "white" {}
		_WiggleStrength ("Wiggle Strength", Range (0.01, 0.2)) = 1
		_WaterHeight ("WaterHeight",Range(0.1,1)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Transparent"}
		ZWrite Off
        Pass
        {
            CGPROGRAM
                #include "UnityCG.cginc"
                #pragma vertex vert
                #pragma fragment frag
                struct v2f
                {
                    fixed4 pos : SV_POSITION;
                    fixed2 pack0 : TEXCOORD0;
                };
                sampler2D _MainTex;
                sampler2D _WiggleTex;
                fixed4 _MainTex_ST;
                fixed4 _Color;
                float _WiggleStrength;
                float _WaterHeight;

                v2f vert(appdata_full v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                    return o;
                }
                
                fixed4 frag(v2f i) : COLOR
                {
                    fixed2 tc2 = i.pack0;
                    tc2.x -= _SinTime;
                    tc2.y += _CosTime;
                    
                    fixed4 wiggle = tex2D(_WiggleTex, tc2);
                    
                    fixed2 newUVtext = i.pack0;
                    newUVtext.x -= wiggle.r * _WiggleStrength;
                    newUVtext.y += wiggle.b * _WiggleStrength*1.5f;
                    
                    fixed4 c;
				    float h = _WaterHeight;
				    c.a = (_WaterHeight-newUVtext.y)*20;
				    c.a = clamp(c.a, 0, 1);
					if(c.a<0.6) {
						c.rgb = tex2D(_MainTex,i.pack0).rgb + _Color.rgb*2.5 * c.a;
					} else {
						c.rgb = (lerp(tex2D(_MainTex,newUVtext).rgb, _Color.rgb, 0.7)) * (_Color.a + 1);
					}
                    return c;

                }

            ENDCG

        }

    }

}