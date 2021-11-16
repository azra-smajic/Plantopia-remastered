
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace planTopia.Generators.Core
{
    public class Generator : MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> Data;
        [SerializeField]
        private Transform parent;
        [SerializeField]
        private List<int> amount;


        public int Size;
        private List<GameObject> lst;
        private GameObject CurrentItem;
        private int CurrentPosition = -1;
        private System.Random RandomGen;
        private int counter = 0;

        private void Start()
        {


            lst = new List<GameObject>();
            for (int i = 0; i < Data.Count; i++)
            {

                Instantiating(Data[i], amount[i]);
                this.CurrentPosition = this.lst.Count - 1;
            }
            this.RandomGen = new System.Random();


        }



        public void Instantiating(GameObject item, int amount)
        {
            for (int i = 0; i < amount; i++)
                this.lst.Add(Instantiate(item));


        }

        public GameObject Next(float x, float y, float z, Vector3 origin = default(Vector3))
        {
            if (this.Size == 0)
                return default(GameObject);

            if (CurrentPosition < 0)
            {
                this.CurrentPosition = this.lst.Count - 1;
                CurrentItem = this.lst[0];
                if (CurrentItem.activeInHierarchy == true)
                    return default(GameObject);
                SpecifiedPosition(ref CurrentItem, x, y, z, origin);

                return CurrentItem; ;
            }

            var Position = this.RandomGen.Next(this.CurrentPosition);
            CurrentItem = this.lst[Position];
            if (CurrentItem.activeInHierarchy == true)
                return default(GameObject);
            this.lst[Position] = lst[CurrentPosition];
            this.lst[CurrentPosition] = CurrentItem;
            CurrentPosition--;

            SpecifiedPosition(ref CurrentItem, x, y, z, origin);


            return CurrentItem;

        }
        //Setting a position of player
        public bool CheckActivity()
        {
            if (counter == 0)
            {
                counter++;
                return false;
               
            }
            
            return lst.Where(x => x.activeInHierarchy == true).ToList().Count ==0;
        }
        public void SpecifiedPosition(ref GameObject currentItem, float x, float y, float z, Vector3 origin = default(Vector3))
        {



            Vector3 Range = new Vector3(x, y, z);

            Vector3 Coordinates = origin + Range;
            currentItem.transform.position = Coordinates;


        }


    }
}
