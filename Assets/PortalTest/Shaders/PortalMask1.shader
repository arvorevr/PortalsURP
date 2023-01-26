Shader "PortalMask1"
{
    Properties{ }
        SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-10" }
        LOD 100
        //Cull Off
        //Cull Front
        ColorMask 0
        ZWrite On

        Pass {}
    }
}
