using System;
using System.Collections.Generic;
using System.Linq;
using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class PhysicalContext {

        // Gravity
        FVector2 gravity;
        public FVector2 Gravity => gravity;

        // Event
        PhysicalEventCenter eventCenter;
        public PhysicalEventCenter EventCenter => eventCenter;

        // Repo
        Dictionary<uint, RigidbodyEntity> rigidbodies;
        RigidbodyEntity[] tempRigidBodyArray;

        // Event Queue
        Queue<(RigidbodyEntity, RigidbodyEntity)> collisionEnterQueue;
        Queue<(RigidbodyEntity, RigidbodyEntity)> collisionStayQueue;
        Queue<(RigidbodyEntity, RigidbodyEntity)> collisionExitQueue;

        Queue<(RigidbodyEntity, RigidbodyEntity)> triggerEnterQueue;
        Queue<(RigidbodyEntity, RigidbodyEntity)> triggerStayQueue;
        Queue<(RigidbodyEntity, RigidbodyEntity)> triggerExitQueue;

        // Contact
        Dictionary<ulong, (ulong, RigidbodyEntity, RigidbodyEntity)> intersectContacts;
        Dictionary<ulong, (ulong, RigidbodyEntity, RigidbodyEntity)> collisionContacts;

        public PhysicalContext() {
            rigidbodies = new Dictionary<uint, RigidbodyEntity>();
            tempRigidBodyArray = new RigidbodyEntity[0];
            eventCenter = new PhysicalEventCenter();
            collisionEnterQueue = new Queue<(RigidbodyEntity, RigidbodyEntity)>();
            collisionStayQueue = new Queue<(RigidbodyEntity, RigidbodyEntity)>();
            collisionExitQueue = new Queue<(RigidbodyEntity, RigidbodyEntity)>();
            triggerEnterQueue = new Queue<(RigidbodyEntity, RigidbodyEntity)>();
            triggerStayQueue = new Queue<(RigidbodyEntity, RigidbodyEntity)>();
            triggerExitQueue = new Queue<(RigidbodyEntity, RigidbodyEntity)>();
            collisionContacts = new Dictionary<ulong, (ulong, RigidbodyEntity, RigidbodyEntity)>();
            intersectContacts = new Dictionary<ulong, (ulong, RigidbodyEntity, RigidbodyEntity)>();
        }

        // Rigidbody
        public void Rigidbody_Add(RigidbodyEntity rb) {
            rigidbodies.Add(rb.ID, rb);
        }

        public bool Rigidbody_TryGetByID(uint id, out RigidbodyEntity res) {
            return rigidbodies.TryGetValue(id, out res);
        }

        public int Rigidbody_TakeAll(out RigidbodyEntity[] res) {
            int count = rigidbodies.Count;
            if (count > tempRigidBodyArray.Length) {
                tempRigidBodyArray = new RigidbodyEntity[(int)(count * 1.5f)];
            }
            rigidbodies.Values.CopyTo(tempRigidBodyArray, 0);
            res = tempRigidBodyArray;
            return count;
        }

        public void Rigidbody_Remove(RigidbodyEntity rb) {
            rigidbodies.Remove(rb.ID);
        }

        public void Rigidbody_ForEach(Action<RigidbodyEntity> action) {
            foreach (var rb in rigidbodies.Values) {
                action.Invoke(rb);
            }
        }

        // Gravity
        public void SetGravity(FVector2 value) {
            gravity = value;
        }

        // Collision
        public void EnqueueCollisionEnter(RigidbodyEntity a, RigidbodyEntity b) {
            collisionEnterQueue.Enqueue((a, b));
        }

        public void EnqueueCollisionStay(RigidbodyEntity a, RigidbodyEntity b) {
            collisionStayQueue.Enqueue((a, b));
        }

        public void EnqueueCollisionExit(RigidbodyEntity a, RigidbodyEntity b) {
            collisionExitQueue.Enqueue((a, b));
        }

        public bool TryDequeueCollisionEnter(out RigidbodyEntity a, out RigidbodyEntity b) {
            if (collisionEnterQueue.TryDequeue(out var pair)) {
                (a, b) = pair;
                return true;
            } else {
                a = null;
                b = null;
                return false;
            }
        }

        public bool TryDequeueCollisionStay(out RigidbodyEntity a, out RigidbodyEntity b) {
            if (collisionStayQueue.TryDequeue(out var pair)) {
                (a, b) = pair;
                return true;
            } else {
                a = null;
                b = null;
                return false;
            }
        }

        public bool TryDequeueCollisionExit(out RigidbodyEntity a, out RigidbodyEntity b) {
            if (collisionExitQueue.TryDequeue(out var pair)) {
                (a, b) = pair;
                return true;
            } else {
                a = null;
                b = null;
                return false;
            }
        }

        // Trigger
        public void EnqueueTriggerEnter(RigidbodyEntity a, RigidbodyEntity b) {
            triggerEnterQueue.Enqueue((a, b));
        }

        public void EnqueueTriggerStay(RigidbodyEntity a, RigidbodyEntity b) {
            triggerStayQueue.Enqueue((a, b));
        }

        public void EnqueueTriggerExit(RigidbodyEntity a, RigidbodyEntity b) {
            triggerExitQueue.Enqueue((a, b));
        }

        public bool TryDequeueTriggerEnter(out RigidbodyEntity a, out RigidbodyEntity b) {
            if (triggerEnterQueue.TryDequeue(out var pair)) {
                (a, b) = pair;
                return true;
            } else {
                a = null;
                b = null;
                return false;
            }
        }

        public bool TryDequeueTriggerStay(out RigidbodyEntity a, out RigidbodyEntity b) {
            if (triggerStayQueue.TryDequeue(out var pair)) {
                (a, b) = pair;
                return true;
            } else {
                a = null;
                b = null;
                return false;
            }
        }

        public bool TryDequeueTriggerExit(out RigidbodyEntity a, out RigidbodyEntity b) {
            if (triggerExitQueue.TryDequeue(out var pair)) {
                (a, b) = pair;
                return true;
            } else {
                a = null;
                b = null;
                return false;
            }
        }

        // Collision Contact
        public void CollisionContact_Add(RigidbodyEntity a, RigidbodyEntity b) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            collisionContacts[key] = (key, a, b);
        }

        public bool CollisionContact_TryGet(RigidbodyEntity a, RigidbodyEntity b, out (ulong, RigidbodyEntity, RigidbodyEntity) value) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            return collisionContacts.TryGetValue(key, out value);
        }

        public bool CollisionContact_Remove(RigidbodyEntity a, RigidbodyEntity b) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            return collisionContacts.Remove(key);
        }

        public bool CollisionContact_Contains(RigidbodyEntity a, RigidbodyEntity b) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            return collisionContacts.ContainsKey(key);
        }

        public void CollisionContact_ForEach(Action<(ulong, RigidbodyEntity, RigidbodyEntity)> action) {
            foreach (var pair in collisionContacts.Values) {
                action.Invoke(pair);
            }
        }

        public KeyValuePair<ulong, (ulong, RigidbodyEntity, RigidbodyEntity)>[] CollisionContact_GetAll() {
            return collisionContacts.ToArray();
        }

        public void CollisionContact_Clear() {
            collisionContacts.Clear();
        }

        // Intersect Contact
        public void IntersectContact_Add(RigidbodyEntity a, RigidbodyEntity b) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            intersectContacts[key] = (key, a, b);
        }

        public bool IntersectContact_TryGet(RigidbodyEntity a, RigidbodyEntity b, out (ulong, RigidbodyEntity, RigidbodyEntity) value) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            return intersectContacts.TryGetValue(key, out value);
        }

        public bool IntersectContact_Remove(RigidbodyEntity a, RigidbodyEntity b) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            return intersectContacts.Remove(key);
        }

        public bool IntersectContact_Contains(RigidbodyEntity a, RigidbodyEntity b) {
            ulong key = IDService.ContactKey(a.ID, b.ID);
            return intersectContacts.ContainsKey(key);
        }

        public void IntersectContact_ForEach(Action<(ulong, RigidbodyEntity, RigidbodyEntity)> action) {
            foreach (var pair in intersectContacts.Values) {
                action.Invoke(pair);
            }
        }

        public KeyValuePair<ulong, (ulong, RigidbodyEntity, RigidbodyEntity)>[] IntersectContact_GetAll() {
            return intersectContacts.ToArray();
        }

        public void IntersectContact_Clear() {
            intersectContacts.Clear();
        }

        public void Clear() {
            rigidbodies.Clear();
            collisionEnterQueue.Clear();
            collisionStayQueue.Clear();
            collisionExitQueue.Clear();
            triggerEnterQueue.Clear();
            triggerStayQueue.Clear();
            triggerExitQueue.Clear();
            collisionContacts.Clear();
            intersectContacts.Clear();
            eventCenter.Clear();
        }

    }

}