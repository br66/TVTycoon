using System.Collections.Generic;
using UnityEngine;

namespace Vision
{
    // to be or not to be Monobehavior?
    public class Network : MonoBehaviour
    {
        //public static Channel instance = null;
      
        // name of channel
        public string Name { get; set; }

        // channel's slogan
        public string Slogan { get; set; }

        // list of all shows
        public List<Program> AllPrograms;

        private void Awake()
        {
            AllPrograms = new List<Program>();
            //AllPrograms.Add(new Program { CurrentNetwork = this, InitalNetwork = this, Name = "Doctor Who", Rating = 4.5f, Type = Program.ProgramType.Show, name = "Doctor Who" });
            //AllPrograms.Add(new Program { CurrentNetwork = this, InitalNetwork = this, Name = "Planet Earth II", Rating = 4f, Type = Program.ProgramType.Show, name = "Planet Earth II" });
            //AllPrograms.Add(new Program { CurrentNetwork = this, InitalNetwork = this, Name = "The Aviator", Rating = 4f, Type = Program.ProgramType.Movie, name = "The Aviator" });
            //AllPrograms.Add(new Program { CurrentNetwork = this, InitalNetwork = this, Name = "U.S. Football", Rating = 4f, Type = Program.ProgramType.Sports, name = "U.S. Football" });
        }

        /* old code > */
        //public Inventory current;
        //public Inventory former;
        //public Inventory upcoming;

        //public Inventory acquired;
        //public Inventory originals;

        /* acquire a show, list as upcoming */

        /*
         * public void AcquireShow(Program pro)
        {
            // list as upcoming
            for (int i = 0; i < upcomingPrograms.Length; i++)
            {
                if (upcomingPrograms[i] == null)
                {
                    upcomingPrograms[i] = pro;
                }
                return;
            }

            // list as acquired, original out of scope for now
            for (int i = 0; i < acquiredProgramList.Length; i++)
            {
                if (acquiredProgramList[i] == null)
                {
                    acquiredProgramList[i] = pro;
                }
                return;
            }
        }

        // cancel a show
        public void CancelShow(Program pro)
        {
            // take out of list of current roster of shows
            for (int i = 0; i < currentPrograms.Length; i++)
            {
                if (currentPrograms[i] == pro)
                {
                    currentPrograms[i] = null;
                }
            }

            // put in list of former shows
            for (int i = 0; i < formerPrograms.Length; i++)
            {
                if (formerPrograms[i] == pro)
                {
                    formerPrograms[i] = null;
                }
            }
            return;
            //}
        }

            // if the show is not active, but on our roster

            // respectfully end a show that's legitimately finished
            // public void EndShow(Program pro)

            /* < old code */
    }
}