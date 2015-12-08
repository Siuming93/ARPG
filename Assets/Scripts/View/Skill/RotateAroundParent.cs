using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.View.Skill
{
    public class RotateAroundParent : MonoBehaviour
    {
        public Transform Parent;
        public float Speed;
        public float Distance;
        public float Height;

        public Vector3 DirectionVector3;

        private void Start()
        {
            transform.position = Parent.position + DirectionVector3;
        }

        private Vector2 oldVector2 = new Vector2();

        private void Update()
        {
            //跟随主角移动
            transform.position = RotationAround(transform.position, Parent.position, Parent.up, Speed*Time.deltaTime);
            //1.计算x,z
            var v = new Vector2(Parent.position.x - transform.position.x, Parent.position.z - transform.position.z);
            if (v.magnitude > 1.1*Distance || v.magnitude < 0.8*Distance && v.magnitude > 0.2f*Distance)
            {
                var u = Distance/v.magnitude*v;
                v = Vector2.SmoothDamp(oldVector2, u, ref v, Time.time, Speed);
            }
            else
            {
                oldVector2 = new Vector2(Parent.position.x - transform.position.x,
                    Parent.position.z - transform.position.z);
                v = oldVector2;
            }

            //2.计算高度
            var y = Mathf.Lerp(Parent.position.y - transform.position.y, -Height, Speed/200*Time.deltaTime);

            transform.position = Parent.position - new Vector3(v.x, y, v.y);
        }

        private void LateUpdate()
        {
        }

        private static Vector3 RotationAround(Vector3 curPoint, Vector3 centerPoint, Vector3 axis, float angle)
        {
            var s = Mathf.Cos(angle/360);
            var v = axis*Mathf.Sin(angle/360);
            var p = curPoint - centerPoint;

            var endPoint = Mathf.Pow(s, 2)*p + v*Vector3.Dot(p, v) + 2*s*(Vector3.Cross(v, p)) +
                           Vector3.Cross(v, Vector3.Cross(v, p));
            return endPoint + centerPoint;
        }
    }
}