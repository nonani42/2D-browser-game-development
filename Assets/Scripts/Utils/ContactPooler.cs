using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class ContactPooler
    {
        private Collider2D _collider;
        private ContactPoint2D[] _contact = new ContactPoint2D[5];
        private float _contactCount = 0;
        private float _threshold = 0.2f;

        public bool IsGrounded { get; private set; }

        public bool LeftContact { get; private set; }

        public bool RightContact { get; private set; }



        public ContactPooler(Collider2D collider)
        {
            _collider = collider;
        }

        public void Update()
        {
            IsGrounded = false;
            LeftContact = false;
            RightContact = false;

            _contactCount = _collider.GetContacts(_contact);

            for (int i = 0; i < _contactCount; i++)
            {
                if (_contact[i].normal.y > _threshold) IsGrounded = true;
                if (_contact[i].normal.x > _threshold) LeftContact = true;
                if (_contact[i].normal.x < -_threshold) RightContact = true;
            }
        }
    }
}
