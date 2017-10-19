
Shader "Surface/Colored Specular Bumped with Illumination No Metal No Trans" {
Properties {
  _MainTex ("Texture", 2D) = "white" {}
  //_CubeMapHelperTex ("Reflection Helper Texture", 2D) = "white" {}
  _SpecMap ("SpecMap(RGB) Illum(A)", 2D) = "white" {}
  _BumpMap ("Normalmap", 2D) = "bump" {}
  _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
  _Color ("Main Color", Color) = (1,1,1,1)
  //_Cube ("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }
  _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
  //_CubeMapShininess ("CubeMapShininess", Range (1, 10)) = 5
}

SubShader {

Tags {"Queue"="Transparent-20" "IgnoreProjector"="True" "RenderType"="Transparent"}
LOD 200
ZWrite On
ZTest LEqual
//	Need to render the feather on head for two faces

	//Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma surface surf ColoredSpecular
#pragma target 3.0

#pragma debug

samplerCUBE _Cube;
 fixed4 _ReflectColor;
struct MySurfaceOutput {
    half3 Albedo;
    half3 Normal;
    half3 Emission;
    half Specular;
    half3 GlossColor;
    half Alpha;
};
 
 
inline half4 LightingColoredSpecular (MySurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
{
  half3 h = normalize (lightDir + viewDir);
 
  half diff = max (0, dot (s.Normal, lightDir));
 
  float nh = max (0, dot (s.Normal, h));
  float spec = pow (nh, 32.0 * s.Specular);
  half3 specCol = spec * s.GlossColor;
 
  half4 c;
  c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * specCol) * (atten * 2);
  c.a = s.Alpha;
  return c;
}
 
inline half4 LightingColoredSpecular_PrePass (MySurfaceOutput s, half4 light)
{
    half3 spec = light.a * s.GlossColor;
   
    half4 c;
    c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
    c.a = s.Alpha + spec * _SpecColor.a;
    return c;
}
 
 
struct Input {
  float2 uv_MainTex;
  float2 uv_SpecMap;
  float2 uv_BumpMap;
  //float3 worldRefl;
  //float2 uv_CubeMapHelperTex;
  //INTERNAL_DATA
};
 
sampler2D _MainTex;
//sampler2D _CubeMapHelperTex;
sampler2D _SpecMap;
sampler2D _BumpMap;
half4 _Color;
half _Shininess;
//half _CubeMapShininess;
 
void surf (Input IN, inout MySurfaceOutput o)
{
fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
  //half3 c = tex2D (_MainTex, IN.uv_MainTex).rgb * _Color.rgb;

  o.Albedo = c.rgb;
 o.Alpha = c.a;
 
  half4 spec = tex2D (_SpecMap, IN.uv_SpecMap)  * _ReflectColor;
  o.GlossColor = spec.rgb;
  //o.Emission = c * spec.a;
  o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
  
  o.Specular = _Shininess * spec;
}

ENDCG
}
Fallback "Diffuse"
}