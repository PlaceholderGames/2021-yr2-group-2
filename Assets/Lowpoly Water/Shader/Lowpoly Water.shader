// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Lowpoly Water" {
	Properties {
		_ColorTex             ("Color", 2D) = "white" {}
		_ShallowColor         ("Shallow", Color) = (1, 1, 1, 1)
		_DeepColor            ("Deep", Color) = (1, 1, 1, 1)
		_EdgeColor            ("Edge", Color)  = (1, 1, 1, 1)
		_EdgeDistance         ("Edge Distance", Float) = 1
		_EdgeIntensity        ("Edge Intensity", Float) = 0
		_SpecColor            ("Specular", Color) = (1, 1, 1, 1)
		_SpecPower            ("Specular Power", Float) = 0.5
		_SpecGloss            ("Specular Gloss", Float) = 0.5
		_FoamTex              ("Foam", 2D) = "white" {}
		_FoamParams           ("Foam Params", Vector) = (1, 1, 1, 1)
		_ReflectionTex        ("Reflection", 2D) = "white" {}
		_ReflectionIntensity  ("Reflection Intensity", Float) = 0.2
		_RefractionTex        ("Refraction", 2D) = "white" {}
		_RefractionIntensity  ("Refraction Intensity", Float) = 0.2
		_Distortion           ("Distortion", Float) = 3
		_WaterColorIntensity  ("Water Color Intensity", Float) = 0.5
		_BumpTex              ("Bump", 2D) = "bump" {}
		_Transparency         ("Transparency", Float) = 1.0
	}
	SubShader {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest LEqual
		ZWrite Off
		Cull Off
		
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert finalcolor:edgeblend keepalpha
		#pragma target 3.0
		
		float _SpecPower, _SpecGloss, _ReflectionIntensity, _RefractionIntensity, _Transparency, _WaterColorIntensity, _Distortion;
		float4 _ShallowColor, _DeepColor, _EdgeColor, _FoamParams;
		sampler2D_float _CameraDepthTexture;
		sampler2D _FoamTex, _ReflectionTex, _BumpTex, _RefractionTex, _ColorTex;
		fixed _EdgeDistance, _EdgeIntensity;
#if UNITY_VERSION < 540
		#define COMPUTESCREENPOS ComputeScreenPos
#else
		#define COMPUTESCREENPOS ComputeNonStereoScreenPos
#endif
		struct Input 
		{
			float2 uv_FoamTex;
			float2 uv_ColorTex;
			float3 worldNormal;
			float3 viewDir;
			float4 bumpuv;
			float4 scrpos;
		};
		void vert (inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.bumpuv = v.vertex.xzxz * 0.063 + _Time.y * 0.008;
			
			float4 wpos = mul(unity_ObjectToWorld, v.vertex);
			float4 opos = UnityObjectToClipPos(v.vertex);
			o.scrpos = COMPUTESCREENPOS(opos);
			o.scrpos.z = lerp(opos.w, mul(UNITY_MATRIX_V, wpos).z, unity_OrthoParams.w);
		}
		void edgeblend (Input IN, SurfaceOutput o, inout fixed4 color)
		{
			float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.scrpos));
			float perpectiveZ = LinearEyeDepth(sceneZ);
#if defined(UNITY_REVERSED_Z)
			sceneZ = 1 - sceneZ;
#endif
			float orthoZ = sceneZ * (_ProjectionParams.y - _ProjectionParams.z) - _ProjectionParams.y;
			sceneZ = lerp(perpectiveZ, orthoZ, unity_OrthoParams.w);
			half edge = abs(sceneZ - IN.scrpos.z) / _EdgeDistance;
			edge = smoothstep(_EdgeIntensity, 1, edge);
			
			float2 uv = IN.uv_FoamTex + _Time * _FoamParams.xy;
			fixed4 foam = tex2D(_FoamTex, uv) * _FoamParams.z;
			
			color.rgb += foam.rgb * (1.0 - edge);
			color = lerp(lerp(color, _EdgeColor, _EdgeColor.a), color, edge);
		}
		void surf (Input IN, inout SurfaceOutput o) 
		{
			float3 N = normalize(IN.worldNormal);
			float3 V = normalize(IN.viewDir);
			
			float3 bump1 = UnpackNormal(tex2D(_BumpTex, IN.bumpuv.xy)).xyz;
			float3 bump2 = UnpackNormal(tex2D(_BumpTex, IN.bumpuv.wz)).xyz;
			float3 bump = (bump1 + bump2) * 0.5;
			
			float4 uv1 = UNITY_PROJ_COORD(IN.scrpos);
			float4 uv2 = UNITY_PROJ_COORD(IN.scrpos);
			uv1.xy += bump.xy * _Distortion;
			uv2.xy += bump.xy * _Distortion;
			fixed4 refl = tex2Dproj(_ReflectionTex, uv1) * _ReflectionIntensity;
			fixed4 refr = tex2Dproj(_RefractionTex, uv2) * _RefractionIntensity;
			
//			float vdn = 0.5 * dot(V, N) + 0.5;
			float vdn = max(0.0, dot(V, N));
			fixed4 c = lerp(_DeepColor, _ShallowColor, vdn) * _WaterColorIntensity + lerp(refr, refl, vdn);
			
			float2 uv = IN.uv_ColorTex + float2(_Time.y  * -0.05, _Time.y  * -0.05);
			uv.y += 0.01 * (sin(uv.x * 3.5 + _Time.y  * 0.35) + sin(uv.x * 4.8 + _Time.y  * 1.05) + sin(uv.x * 7.3 + _Time.y  * 0.45)) / 3.0;
			uv.x += 0.12 * (sin(uv.y * 4.0 + _Time.y  * 0.5) + sin(uv.y * 6.8 + _Time.y  * 0.75) + sin(uv.y * 11.3 + _Time.y  * 0.2)) / 3.0;
			uv.y += 0.12 * (sin(uv.x * 4.2 + _Time.y  * 0.64) + sin(uv.x * 6.3 + _Time.y  * 1.65) + sin(uv.x * 8.2 + _Time.y  * 0.45)) / 3.0;
			fixed3 tc = tex2D(_ColorTex, uv).rgb;
			
			o.Specular = _SpecPower;
			o.Gloss = _SpecGloss;
			o.Albedo = tc * c.rgb;
			o.Alpha = _Transparency;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
