using Items.Busters;
using UnityEngine;

namespace Player
{
    public class PlayerBusters : MonoBehaviour
    {
        public void AddNewBuster(ActiveBuster buster)
        {
              ActiveBuster newBuster = (ActiveBuster)gameObject.AddComponent(buster.GetType());
              newBuster.Copy(buster);
              newBuster.InitBuster();
        }

        public void RemoveBuster(Component component)
        {
            Destroy(gameObject.GetComponent(component.GetType()));
        }
    }
}