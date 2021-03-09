using System.Collections.Generic;

public static class TutorialPlayerObjectRegistry
{
    // keeps a list of all the players
    public static List<TutorialPlayerObject> players = new List<TutorialPlayerObject>();

    // create a player for a connection
    // note: connection can be null
    public static TutorialPlayerObject CreatePlayer(BoltConnection connection, BoltEntity chara)
    {
        TutorialPlayerObject player;
     
        // create a new player object, assign the connection property
        // of the object to the connection was passed in
        player = new TutorialPlayerObject();
        player.connection = connection;
        player.character = chara;
        // if we have a connection, assign this player
        // as the user data for the connection so that we
        // always have an easy way to get the player object
        // for a connection
        if (player.connection != null)
        {
            player.connection.UserData = player;
        }

        // add to list of all players
        players.Add(player);

        return player;
    }
    public static void AddPlayers(BoltEntity e)
    {
        
    }
    // this simply returns the 'players' list cast to
    // an IEnumerable<T> so that we hide the ability
    // to modify the player list from the outside.
    public static IEnumerable<TutorialPlayerObject> AllPlayers
    {
        get { return players; }
    }
    public static List<bool> GetBool()
    {
        List<bool> playerCheck = new List<bool>();
        playerCheck.Clear();
        for(int i = 0; i< players.Count ;i++)
        {
            playerCheck.Add(players[i].character.GetComponent<NetworkPlayer>().isReady);
        }
        return playerCheck;
    }
    // finds the server player by checking the
    // .IsServer property for every player object.
    public static TutorialPlayerObject ServerPlayer
    {
        get { return players.Find(player => player.IsServer); }
    }

    // utility function which creates a server player
    /*
    public static TutorialPlayerObject CreateServerPlayer()
    {
        return CreatePlayer(null);
    }

    // utility that creates a client player object.
    public static TutorialPlayerObject CreateClientPlayer(BoltConnection connection)
    {
        return CreatePlayer(connection);
    }
     */
    // utility function which lets us pass in a
    // BoltConnection object (even a null) and have
    // it return the proper player object for it.
    public static TutorialPlayerObject GetTutorialPlayer(BoltConnection connection)
    {
        if (connection == null)
        {
            return ServerPlayer;
        }

        return (TutorialPlayerObject)connection.UserData;
    }
}