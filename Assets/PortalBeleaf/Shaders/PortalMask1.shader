Shader "PortalMask1"
{
    Properties{ }
        SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry -10" }
        LOD 100
        ColorMask 0
        //ZWrite On

        //ZTest Less

        Pass {}
    }
}
