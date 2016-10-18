using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class TransformSyncer : MonoBehavior
{
    private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        // Always send transform (depending on reliability of the network view) 
        if (stream.isWriting) {
            Vector3 pos = transform.localPosition;
            Quaternion rot = transform.localRotation;
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
        }
            // When receiving, buffer the information 
        else {
            // Receive latest state information 
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
            transform.localPosition = pos;
            transform.localRotation = rot;
        }
    }
}
