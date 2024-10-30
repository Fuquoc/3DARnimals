Shader "Custom/HoleShader"
{
    Properties
    {
        _MainTex ("Square Texture", 2D) = "white" {}  // Texture của sprite hình vuông
        _HoleTex ("Hole Texture", 2D) = "white" {}    // Texture của sprite lỗ thủng
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        LOD 200

        // Pass 1: Đánh dấu stencil theo hình dạng của sprite lỗ thủng
        Pass
        {
            Stencil
            {
                Ref 1                       // Giá trị tham chiếu stencil là 1
                Comp Always                 // Luôn ghi vào stencil buffer
                Pass Replace                // Thay thế giá trị stencil buffer bằng Ref
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragHole
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _HoleTex;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 fragHole (v2f i) : SV_Target
            {
                fixed4 holeColor = tex2D(_HoleTex, i.uv);
                
                // Nếu alpha của vùng lỗ thủng đủ lớn, ghi giá trị vào stencil buffer
                if (holeColor.a > 0.5)      
                {
                    return fixed4(1, 1, 1, 1); // Ghi vào stencil buffer
                }

                // Trả về giá trị alpha bằng 0 cho vùng không có lỗ thủng
                return fixed4(0, 0, 0, 0);
            }
            ENDCG
        }

        // Pass 2: Render sprite hình vuông với lỗ thủng trong suốt
        Pass
        {
            Stencil
            {
                Ref 1                       // Kiểm tra với giá trị tham chiếu stencil là 1
                Comp NotEqual               // Chỉ render ngoài vùng stencil
            }

            Blend SrcAlpha OneMinusSrcAlpha // Blend alpha để giữ tính trong suốt của lỗ thủng

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragSquare
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 fragSquare (v2f i) : SV_Target
            {
                // Trả về texture của hình vuông và sử dụng màu của _MainTex
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
