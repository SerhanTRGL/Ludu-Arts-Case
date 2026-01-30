using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteAlways]
public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform m_ObjectToFollow;
    [SerializeField] private Vector3 m_Offset;

    private Camera m_Camera;
    
    void Start(){
        m_Camera = GetComponent<Camera>();
    }

    private void LateUpdate() {
        m_Camera.transform.position = m_ObjectToFollow.position + m_Offset;
    }
}
