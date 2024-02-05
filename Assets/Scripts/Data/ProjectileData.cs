﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Yuki
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Data/Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxExistTime;

        public float Speed => _speed;
        public float MaxExistTime => _maxExistTime;
    }
}