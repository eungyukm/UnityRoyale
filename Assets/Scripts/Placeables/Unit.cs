using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    //휴머노이드 또는 어쨌든 걸어 다닐 수 있는.
    public class Unit : ThinkingPlaceable
    {
        //배치 가능한 데이터에서 오는 데이터
        private float speed;

        private Animator animator;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            pType = Placeable.PlaceableType.Unit;

            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
			audioSource = GetComponent<AudioSource>();
        }

        //이 유닛이 플레이 필드에서 플레이될 때 GameManager에 의해 호출됩니다.
        public void Activate(Faction pFaction, PlaceableData pData)
        {
            faction = pFaction;
            hitPoints = pData.hitPoints;
            targetType = pData.targetType;
            attackRange = pData.attackRange;
            attackRatio = pData.attackRatio;
            speed = pData.speed;
            damage = pData.damagePerAttack;
			attackAudioClip = pData.attackClip;
			dieAudioClip = pData.dieClip;
            //TODO: add more as necessary
            
            navMeshAgent.speed = speed;
            animator.SetFloat("MoveSpeed", speed); //실행 애니메이션 클립의 속도에 대한 승수 역할을 합니다.

            state = States.Idle;
            navMeshAgent.enabled = true;
        }

        public override void SetTarget(ThinkingPlaceable t)
        {
            base.SetTarget(t);
        }

		//유닛이 목표물을 향해 이동합니다.
        public override void Seek()
        {
            if(target == null)
                return;

            base.Seek();

            navMeshAgent.SetDestination(target.transform.position);
            navMeshAgent.isStopped = false;
            animator.SetBool("IsMoving", true);
        }

		//유닛이 목표에 도달했습니다. 이 기능은 "공격 모드"로 설정하지만 데미지를 주지 않습니다(Deal Blow 참조).
        public override void StartAttack()
        {
            base.StartAttack();

            navMeshAgent.isStopped = true;
            animator.SetBool("IsMoving", false);
        }

		//공격 애니메이션을 시작하고 유닛의 공격 비율에 따라 반복됩니다.
        public override void DealBlow()
        {
            base.DealBlow();

            animator.SetTrigger("Attack");
            transform.forward = (target.transform.position - transform.position).normalized; //turn towards the target
        }

		public override void Stop()
		{
			base.Stop();

			navMeshAgent.isStopped = true;
			animator.SetBool("IsMoving", false);
		}

        protected override void Die()
        {
            base.Die();

            navMeshAgent.enabled = false;
            animator.SetTrigger("IsDead");
        }
    }
}
