using Interface;
using UnityEngine;

namespace NPC
{
    public class NpcMain : MonoBehaviour, IInteractive
    {
        [SerializeField] private InteractiveType interactiveType;
        public InteractiveType GetInteractiveType() => interactiveType;
    }
}