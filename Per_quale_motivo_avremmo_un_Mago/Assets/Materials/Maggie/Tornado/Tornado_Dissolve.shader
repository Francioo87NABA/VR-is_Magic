// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Tornado_Dissolve"
{
	Properties
	{
		_Grandezzanois("Grandezza nois", Float) = 1.31
		_Noysspeed("Noys speed", Vector) = (1,1,0,0)
		_Tempo("Tempo", Float) = 1.32
		_Color0("Color 0", Color) = (0,0.9723275,1,1)
		_TwirlCenter("Twirl Center", Float) = 1
		_TwirlStrenght("Twirl Strenght", Float) = 1
		_Twirlofset("Twirl ofset", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		uniform float _Tempo;
		uniform float2 _Noysspeed;
		uniform float _Grandezzanois;
		uniform float _TwirlCenter;
		uniform float _TwirlStrenght;
		uniform float _Twirlofset;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 temp_output_12_0 = _Color0;
			o.Albedo = temp_output_12_0.rgb;
			float2 temp_output_1_0_g2 = i.uv_texcoord;
			float2 temp_output_11_0_g2 = ( temp_output_1_0_g2 - float2( 0.5,0.5 ) );
			float2 break18_g2 = temp_output_11_0_g2;
			float2 appendResult19_g2 = (float2(break18_g2.y , -break18_g2.x));
			float dotResult12_g2 = dot( temp_output_11_0_g2 , temp_output_11_0_g2 );
			float mulTime1 = _Time.y * _Tempo;
			float simplePerlin2D5 = snoise( ( temp_output_1_0_g2 + ( appendResult19_g2 * ( dotResult12_g2 * float2( 10,10 ) ) ) + ( mulTime1 * _Noysspeed ) )*_Grandezzanois );
			simplePerlin2D5 = simplePerlin2D5*0.5 + 0.5;
			float2 temp_cast_1 = (_TwirlCenter).xx;
			float2 center45_g1 = temp_cast_1;
			float2 delta6_g1 = ( i.uv_texcoord - center45_g1 );
			float angle10_g1 = ( length( delta6_g1 ) * _TwirlStrenght );
			float x23_g1 = ( ( cos( angle10_g1 ) * delta6_g1.x ) - ( sin( angle10_g1 ) * delta6_g1.y ) );
			float2 break40_g1 = center45_g1;
			float2 temp_cast_2 = (( mulTime1 * _Twirlofset )).xx;
			float2 break41_g1 = temp_cast_2;
			float y35_g1 = ( ( sin( angle10_g1 ) * delta6_g1.x ) + ( cos( angle10_g1 ) * delta6_g1.y ) );
			float2 appendResult44_g1 = (float2(( x23_g1 + break40_g1.x + break41_g1.x ) , ( break40_g1.y + break41_g1.y + y35_g1 )));
			float simplePerlin2D14 = snoise( appendResult44_g1 );
			simplePerlin2D14 = simplePerlin2D14*0.5 + 0.5;
			float temp_output_20_0 = ( simplePerlin2D5 * simplePerlin2D14 );
			o.Alpha = temp_output_20_0;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18000
0;0;1920;1059;2029.621;609.3805;1.760442;True;False
Node;AmplifyShaderEditor.RangedFloatNode;4;-1166.999,41.44;Inherit;False;Property;_Tempo;Tempo;2;0;Create;True;0;0;False;0;1.32;1.32;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;2;-916.9995,368.44;Inherit;False;Property;_Noysspeed;Noys speed;1;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;19;-1023.653,1161.308;Inherit;False;Property;_Twirlofset;Twirl ofset;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;1;-914.9995,47.44;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-695.9995,196.44;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-755.0443,760.8083;Inherit;False;Property;_TwirlCenter;Twirl Center;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-771.6237,1130.143;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-750.0443,869.8083;Inherit;False;Property;_TwirlStrenght;Twirl Strenght;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;6;-484.3593,382.5599;Inherit;False;Radial Shear;-1;;2;c6dc9fc7fa9b08c4d95138f2ae88b526;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-439.479,602.8798;Inherit;False;Property;_Grandezzanois;Grandezza nois;0;0;Create;True;0;0;False;0;1.31;1.31;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;15;-497.0443,760.8083;Inherit;False;Twirl;-1;;1;90936742ac32db8449cd21ab6dd337c8;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;14;-193.4396,754.7994;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;5;-224.6002,453.2799;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;218.4639,629.9708;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;12;36.84523,-243.9995;Inherit;False;Property;_Color0;Color 0;3;0;Create;True;0;0;False;0;0,0.9723275,1,1;0,0.9723275,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;320.118,149.0025;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;649.0439,5.84044;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Tornado_Dissolve;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;0;4;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;18;0;1;0
WireConnection;18;1;19;0
WireConnection;6;4;3;0
WireConnection;15;2;16;0
WireConnection;15;3;17;0
WireConnection;15;4;18;0
WireConnection;14;0;15;0
WireConnection;5;0;6;0
WireConnection;5;1;9;0
WireConnection;20;0;5;0
WireConnection;20;1;14;0
WireConnection;13;0;12;0
WireConnection;13;1;20;0
WireConnection;0;0;12;0
WireConnection;0;9;20;0
ASEEND*/
//CHKSM=2E08D1F9C6A4F4F5EB12038C113E1D6CD1973A4A