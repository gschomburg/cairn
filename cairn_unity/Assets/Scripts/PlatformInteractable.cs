namespace VRTK.Examples
{
    using UnityEngine;
    using System.Collections;

    public class PlatformInteractable : VRTK_InteractableObject
    {

        public FixedJoint lockPointJoint;
        //public bool lockedInPlace;
        public GameObject lockPoint;
        private int LOCK_POINT_LAYER = 10;

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            //FireBullet();
        }
        public override void Ungrabbed(GameObject previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);

            //check for collisions with lock Points
            //if there's a collision make a fixed joint
            //gameObject.AddComponent<FixedJoint>();
            //gameObject.GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
            if(lockPoint != null)
            {
                lockPointJoint = gameObject.AddComponent<FixedJoint>();
                gameObject.GetComponent<FixedJoint>().connectedBody = lockPoint.GetComponent<Rigidbody>();
            }
        }
        public override void Grabbed(GameObject currentGrabbingObject)
        {
            base.Grabbed(currentGrabbingObject);
            //break any existing joint

            //Debug.Log("Grabbed");
            if (lockPointJoint!=null)
            {
                Destroy(lockPointJoint);
            }
            lockPointJoint = null;
            lockPoint = null;
        }

        protected override void Start()
        {
            base.Start();
            //bullet = transform.Find("Bullet").gameObject;
            //bullet.SetActive(false);
        }

        //void OnCollisionStay(Collision collisionInfo)
        //{
        //    if (grabbingObject!=null)
        //    {
        //        Debug.Log("collision but currently grabbed");
        //        return;
        //    }

        //    Debug.Log("collision: " + collisionInfo.gameObject);

        //    //foreach (GameObject contact in collisionInfo.gameObject)
        //    //{
        //    //    //lockedInPlace = true;
                
        //    //}
        //}

        void OnTriggerEnter(Collider other)
        {
            //Debug.Log("OnTriggerEnter: " + other.gameObject);
            if (other.gameObject.layer == LOCK_POINT_LAYER)
            {
                lockPoint = other.gameObject;
            }
        }

        void OnTriggerExit(Collider other)
        {
            
            if(other.gameObject == lockPoint)
            {
                lockPoint = null;
            }
        }
    }
}


