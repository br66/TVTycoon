using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vision;

/// <summary>
/// This oversees the entire game.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Allows only a single instance of GameManager to exist.
    /// </summary>
    public static GameManager instance = null;

    /// <summary>
    /// The player.
    /// </summary>
    public GameObject Player { get; private set; }

    /// <summary>
    /// The network the player owns / is currently playing as.
    /// </summary>
    private Vision.Network m_network;

    /// <summary>
    /// Instance of UI Manager
    /// </summary>
    [SerializeField]
    private UIManagerObj m_uiManager;

    #region Unity Functions
    /// <summary>
    /// This runs before start, creates singleton.
    /// </summary>
    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)

            // if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        m_uiManager = transform.Find("UI").GetComponent<UIManagerObj>();
    }

    // Called after Awake()
    void Start ()
    {
        CreateChannel("channel iwh");
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}
    #endregion

    #region Test Functions
    /// <summary>
    /// Creates faux channel for testing
    /// </summary>
    /// <param name="name"></param>
    public void CreateChannel(string name)
    {
        if (!Player)
        {
            Player = new GameObject();
            Player.transform.parent = transform;
            Player.name = "iwh";
            Player.tag = "Player";
            m_network = Player.AddComponent<Vision.Network>();
            m_network.Name = name;
            m_uiManager.OnPlayerInitialze();
        }
        else
        {
            Debug.Log("Channel already exists!");
        }
    }
    #endregion

    #region Channel Actions
    /// <summary>
    /// Adds a show to the network
    /// </summary>
    /// <param name="pro"></param>
    /// <returns>
    /// true if successful, false otherwise
    /// </returns>
    public bool AcquireShow(Program pro)
    {
        return false;
    }

    /// <summary>
    /// Cancels the show.
    /// </summary>
    /// <param name="pro"></param>
    /// <returns>
    /// true if successful in cancellation, false otherwise
    /// </returns>
    public bool CancelShow(Program pro)
    {
        return false;
    }

    /// <summary>
    /// This is for relinguishing an intellectual property for moving it to another network or not.
    /// </summary>
    /// <param name="pro"></param>
    /// <returns>
    /// true if able to remove show, false otherwise
    /// </returns>
    public bool RemoveShow(Program pro)
    {
        return false;
    }
    #endregion

    #region Deprecated Functions
    public void ChannelAcquireShow(Program pro)
    {
        //PlayerOne.CurrentChannel.AcquireShow(pro);
    }

    public void ChannelCancelShow(Program pro)
    {
        //PlayerOne.CurrentChannel.CancelShow(pro);
    }
    #endregion
}
