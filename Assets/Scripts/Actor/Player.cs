using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuki.NEnemy;

namespace Yuki.NPlayer
{
    public class Player : Actor
    {
        [SerializeField] private PlayerData _data; public PlayerData Data => _data;
        public Input Input { get; private set; }


        //Core component
        private DamageReceiver _damageReceiver; public DamageReceiver DamageReceiver => _damageReceiver;
        private Stats _stats; public Stats Stats => _stats;
        private CollisionSenses _collisionSenses;
        public CollisionSenses CollisionSenses => _collisionSenses;
        private Movement _movement; public Movement Movement => _movement;
        private RangeAttack _rangeAttack; public RangeAttack RangeAttack => _rangeAttack;

        //State
        public RunState RunState { get; private set; }
        public JumpState JumpState { get; private set; }
        public PlayerFallState FallState { get; private set; }
        public QuickFallState QuickFallState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public AttackState AttackState { get; private set; }
        public DeathState DeathState { get; private set; }
        public HitState HitState { get; private set; }

        public override void Awake()
        {
            base.Awake();

            RunState = new RunState(this, "run");
            JumpState = new JumpState(this, "jump");
            FallState = new PlayerFallState(this, "fall");
            QuickFallState = new QuickFallState(this, "quickFall");
            DashState = new PlayerDashState(this, "dash");
            AttackState = new AttackState(this, "attack");
            DeathState = new DeathState(this, "death");
            HitState = new HitState(this, "hit");
        }

        private void OnTakeDamage()
        {
            FSM.ChangeState(HitState);
        }

        private void OnPlayerDie()
        {
            FSM.ChangeState(DeathState);
        }

        private void OnDisable()
        {
            _damageReceiver.OnTakeDamage -= OnTakeDamage;
            _stats.OnPlayerDie -= OnPlayerDie;
        }

        public override void Start()
        {
            base.Start();

            _damageReceiver = Core.GetCoreComponent<DamageReceiver>();
            _stats = Core.GetCoreComponent<Stats>();
            _collisionSenses = Core.GetCoreComponent<CollisionSenses>();
            _movement = Core.GetCoreComponent<Movement>();
            _rangeAttack = Core.GetCoreComponent<RangeAttack>();

            _damageReceiver.OnTakeDamage += OnTakeDamage;
            _stats.OnPlayerDie += OnPlayerDie;

            Input = GetComponent<Input>();
            FSM.Initialization(RunState);
        }
    }
}