using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendScoreObj{
    public string UserId;
    public string PersonAlias;
    public int Score;

    public SendScoreObj(string userId, string alias, int score)
    {
        this.UserId = userId;
        this.PersonAlias = alias;
        this.Score = score;
    }


}
