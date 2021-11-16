using UnityEngine;

namespace planTopia.Boss

{
    public class OnCollide : MonoBehaviour
    {
        [SerializeField]
        private GameObject Boss;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.PLAYER))
            {
               // Boss.GetComponent<Chase>().enabled = true;
            }
        }
    }
}
