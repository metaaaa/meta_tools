// クォータニオン関連

float4 quaternion(float rad, float3 axis)
{
    return float4(cos(rad / 2.0), axis * sin(rad / 2.0));
}

float4 mulQuaternion(float4 q, float4 r)
{
    // Q = (q; V)  R = (r; W)
    //QR = (qr - V ・ W; qW + rV + V × W)
    return float4(q.x*r.x - dot(q.yzw,r.yzw), q.x*r.yzw + r.x*q.yzw + cross(q.yzw, r.yzw));
}

float4 sLerpQuaternion(float4 q, float4 r, float t)
{
    float theta = acos(max(dot(q,r), dot(-q,r)));
    return sin((1 - t)*theta)*q/sin(theta) + r*sin(t*theta)/sin(theta);
}

float3 rotateQuaternion(float rad, float3 axis, float3 pos)
{
    float4 q = quaternion(rad, axis);
    return (q.x*q.x - dot(q.yzw, q.yzw)) * pos + 2.0 * q.yzw * dot(q.yzw, pos) + 2 * q.x * cross(q.yzw, pos);
}

float3 rotateQuaternion(float4 q, float3 pos)
{
    return (q.x*q.x - dot(q.yzw, q.yzw)) * pos + 2.0 * q.yzw * dot(q.yzw, pos) + 2 * q.x * cross(q.yzw, pos);
}

float3 angleToRadian(float3 angle)
{
    return PI*angle/180.0;
}

// Unityの回転順はZXY
float4 eulerToQuaternion(float3 rad)
{
    rad = rad*0.5;
    return float4(cos(rad.x)*cos(rad.y)*cos(rad.z) + sin(rad.x)*sin(rad.y)*sin(rad.z),
                  sin(rad.x)*cos(rad.y)*cos(rad.z) + cos(rad.x)*sin(rad.y)*sin(rad.z),
                  cos(rad.x)*sin(rad.y)*cos(rad.z) - sin(rad.x)*cos(rad.y)*sin(rad.z),
                  cos(rad.x)*cos(rad.y)*sin(rad.z) - sin(rad.x)*sin(rad.y)*cos(rad.z));
}