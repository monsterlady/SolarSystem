// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Animated Sun Shader with Vertex Color
Shader "NiehoffDesigns/SunVertexShader" {
	Properties 
	{
		// BaseColor Options
		_Color ("Ambient Color",   Color) 		= (1,1,1)	// Color Multiplier allows to apply a tint to the overall color
		_Glossiness ("Smoothness", Range(0,1)) 	= 0.26		// Defines the glossiness of the model
		_Metallic ("Metallic",     Range(0,1)) 	= 0.0		// Defines the metallic amount of the model
		_Em ("Emission Strength",  Range(0,1)) 	= 0.0		// Defines the Strength of the Emission
		_EmColor ("Emission Tint", Color) 		= (1,1,1)	// Allows to add a custom tint to the models emission
		
		// Atmosphere Color Options
		_AtmoColor			("Atmosphere Color", 	Color) 			= (1, 0.416, 0)		// Defines the color of the atmosphere layer
		_AtmoColorFalloff	("Atmosphere Falloff",  Range(0,10)) 	= 1					// Defines the falloff of the atmosphere pointing to the center of the sun
        _AtmoPower			("Atmosphere Power", 	Range(0,5)) 	= 1.75				// Defines the strength of the atmosphere color
		
		// Animation Properties
		_AnimIntensity 		("Blast Intensity", 	Range(0,1) ) 	= 0.75		// Defines the intensity of the solar blast animation
		_AnimSpeed 			("Blast Speed", 		Range(1, 25)) 	= 12		// Defines the speed of the blast animation
		
		// Texture Channels						
		_MainTex ("Diffuse", 2D) = "white" {}	// Texture Containing color information for the diffuse channel
		[Normal]
		_BumpMap ("Normal", 2D) = "bump" {}		// Normal Map for the hard shading look
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		// Color Properies
		fixed3 _Color, _EmColor, _AtmoColor;
		// Shading Properies
		half _Glossiness, _Metallic, _Em;
		// Atmosphere Properies
		fixed _AtmoColorFalloff, _AtmoPower;
		// Animation Properties
		fixed _AnimSpeed, _AnimIntensity;
		// Texture Channels	
		sampler2D _MainTex;
		sampler2D _BumpMap;	
		
		
		// Structs
		struct Input {
			float3 vertexColor;
			float3 atmoColor;
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};		
		struct v2f {
           float4 pos 	: SV_POSITION;
           fixed4 color : COLOR;       
         };
         
         // Vertex shading 
         void vert (inout appdata_full v, out Input o)
         {
            UNITY_INITIALIZE_OUTPUT(Input,o);                                      
			          
			half4 posWorld = mul(unity_ObjectToWorld, v.vertex);
			half4 posObj = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0)); 	
			
			half3 vMod = (posWorld - posObj) * 75;				
			vMod += (_Time.x * _AnimSpeed);
			vMod = sin(vMod) * (_AnimIntensity * 0.1);

			half3 cam = normalize(_WorldSpaceCameraPos - posWorld);
			half rDot = 1 - abs(dot(v.normal, cam));
			rDot = pow(rDot, 1.5);	// Default 2.6
			
            v.vertex.xyz += vMod * rDot;             
                         
         	o.vertexColor = v.color;
            
            // Calculate our atmosphere color
			half3 atmo = saturate( pow(1 - dot(normalize( WorldSpaceViewDir(v.vertex) ), normalize(v.normal)), _AtmoColorFalloff) * _AtmoPower) * _AtmoColor * _AtmoPower;
			o.atmoColor = atmo * _AtmoPower;
         }

		// Surface shading
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Apply the color to the albedo channel
			o.Albedo = (tex2D (_MainTex, IN.uv_MainTex).rgb * IN.vertexColor) * _Color;									
			// also apply our Metallic and smoothness from the slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;			
			// add normal information from texture
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
										
			// Finally apply our emission, to get some shiny glow, if desired
			o.Emission = (IN.vertexColor * _EmColor * _Em);		
			// and add our atmosphere on top of it
			o.Emission = (o.Emission + IN.atmoColor) * 0.5;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
