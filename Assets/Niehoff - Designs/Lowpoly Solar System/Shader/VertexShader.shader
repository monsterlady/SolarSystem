// Simple example shader to showcase the use of vertex color with the supplied assets
Shader "NiehoffDesigns/VertexShader" 
{
	Properties 
	{		
		_Color ("Color", Color) = (1,1,1,1)				// Color Multiplier allows to apply a tint to the vertex color
		_Glossiness ("Smoothness", Range(0,1)) = 0.5	// Defines the glossiness of the model
		_Metallic ("Metallic", Range(0,1)) = 0.0		// Defines the metallic amount of the model
		_Em ("Emission Strength", Range(0,1)) = 0.0		// Defines the Strength of the Emission
		_EmColor ("Emission Tint", Color) = (1,1,1,1)	// Allows to add a custom tint to the models emission
	}	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		struct Input {
			float3 vertexColor;					// Vertex Color information
		};
		
		struct v2f {
           float4 pos : SV_POSITION;
           fixed4 color : COLOR;
         };
 
         void vert (inout appdata_full v, out Input o)
         {
             UNITY_INITIALIZE_OUTPUT(Input,o);
             o.vertexColor = v.color; 			// Save the Vertex Color for the surf method
         }

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;		
		
		half _Em;
		fixed4 _EmColor;		

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Apply the Vertex color to the albedo channel
			o.Albedo = IN.vertexColor * _Color;							
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;			
			// Finally apply our emission, to get some shiny glow if desired
			o.Emission = (IN.vertexColor * _EmColor) * _Em;	
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
