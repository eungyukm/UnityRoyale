using UnityEngine;
using UnityEngine.Events;

namespace UnityRoyale
{
    //재생 영역에 배치할 수 있는 모든 객체의 기본 클래스(단위, 장애물, 구조물 등)
    public class Placeable : MonoBehaviour
    {
        public PlaceableType pType;
		
        [HideInInspector] public Faction faction;
        [HideInInspector] public PlaceableTarget targetType; //TODO: move to ThinkingPlaceable?
		[HideInInspector] public AudioClip dieAudioClip;

        public UnityAction<Placeable> OnDie;

        public enum PlaceableType
        {
            Unit,
            Obstacle,
            Building,
            Spell,
            Castle, //special type of building
        }

        public enum PlaceableTarget
        {
            OnlyBuildings,
            Both,
            None,
        }
        
        // 당파 파벌
        public enum Faction
        {
            Player, //Red
            Opponent, //Blue
            None,
        }
    }
}